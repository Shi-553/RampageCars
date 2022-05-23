using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace RampageCars
{
    public class SceneLoader : SingletonBase
    {
        public Scene Current { get; private set; }
        public bool IsPause { get; private set; }

        public SceneType CurrentType => (SceneType)Current.buildIndex;

        Coroutine changeing;

        [SerializeField, EditorScripts.NotNull]
        GameObject loading;
        [SerializeField, EditorScripts.NotNull]
        AudioListener audioListener;

        protected override void Init()
        {
            Current = SceneManager.GetActiveScene();
        }

        public void ChangeScene(SceneType scene)
        {
            if (changeing == null)
            {
                changeing = StartCoroutine(ChangeSceneAsync(scene));
            }
        }

        IEnumerator ChangeSceneAsync(SceneType type)
        {
            Time.timeScale = 0;
            loading.SetActive(true);
            audioListener.enabled = false;

            Singleton.Get<BGMManager>().StopAll();
            Singleton.Get<SEManager>().StopAll();

            SceneManager.SetActiveScene(gameObject.scene);

            yield return SceneManager.UnloadSceneAsync(Current);
            if (IsPause)
            {
                yield return SceneManager.UnloadSceneAsync(SceneType.Pause.GetBuildIndex());
                IsPause = false;
            }
            audioListener.enabled = true;

            yield return Resources.UnloadUnusedAssets();

            yield return SceneManager.LoadSceneAsync(type.GetBuildIndex(), LoadSceneMode.Additive);

            audioListener.enabled = false;



            Current = SceneManager.GetSceneByBuildIndex(type.GetBuildIndex());
            SceneManager.SetActiveScene(Current);

            FindObjectOfType<FirstSelect>()?.Select();

            Debug.Log($"Changed to <b>{CurrentType}</b>");

            loading.SetActive(false);
            Time.timeScale = 1;

            changeing = null;
        }
        IEnumerator AddPauseAsync()
        {
            Time.timeScale = 0;

            yield return SceneManager.LoadSceneAsync(SceneType.Pause.GetBuildIndex(), LoadSceneMode.Additive);
            SceneManager.SetActiveScene(SceneManager.GetSceneByBuildIndex(SceneType.Pause.GetBuildIndex()));

            Debug.Log($"Add to <b>{CurrentType}</b>");

            FindObjectOfType<FirstSelect>()?.Select();
            IsPause = true;

            changeing = null;
        }
        IEnumerator RemovePauseAsync()
        {
            Time.timeScale = 0;

            yield return SceneManager.UnloadSceneAsync(SceneType.Pause.GetBuildIndex());
            SceneManager.SetActiveScene(Current);

            Debug.Log($"Remove to <b>{CurrentType}</b>");

            FindObjectOfType<FirstSelect>()?.Select();
            Time.timeScale = 1;
            IsPause = false;

            changeing = null;
        }

        public void Pause()
        {
            if (changeing == null && !IsPause)
            {
                changeing = StartCoroutine(AddPauseAsync());
            }
        }
        public void Resume()
        {
            if (changeing == null && IsPause)
            {
                changeing = StartCoroutine(RemovePauseAsync());
            }
        }
    }
}
