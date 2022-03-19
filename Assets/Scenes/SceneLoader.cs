using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace RampageCars
{
    public class SceneLoader : SingletonBase
    {
        public Scene Current { get; private set; }

        Coroutine changeing;

        protected override void Init()
        {
            Current = SceneManager.GetActiveScene();
        }

        public void LoadScene(SceneType scene)
        {
            if (changeing == null)
            {
                changeing = StartCoroutine(SceneChangeAsync(scene));
            }
        }

        IEnumerator SceneChangeAsync(SceneType scene)
        {
            var load = SceneManager.LoadSceneAsync(((int)scene), LoadSceneMode.Additive);
            load.allowSceneActivation = false;

            yield return SceneManager.UnloadSceneAsync(Current);
            load.allowSceneActivation = true;

            changeing = null;
        }
    }
}
