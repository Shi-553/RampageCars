using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace RampageCars
{
    public class SceneLoader : SingletonBase
    {
        public Scene Current { get; private set; }

        public SceneType CurrentType => (SceneType)Current.buildIndex;

        Coroutine changeing;

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

        IEnumerator ChangeSceneAsync(SceneType scene)
        {
            Time.timeScale = 0;

            yield return SceneManager.UnloadSceneAsync(Current);
            var load=SceneManager.LoadSceneAsync(scene.GetBuildIndex(), LoadSceneMode.Additive);

            yield return Resources.UnloadUnusedAssets();

            yield return load;

            Current = SceneManager.GetSceneByBuildIndex(scene.GetBuildIndex());
            SceneManager.SetActiveScene(Current);

            Debug.Log($"Changed to <b>{CurrentType}</b>");

            Time.timeScale = 1;
            changeing = null;
        }

    }
}
