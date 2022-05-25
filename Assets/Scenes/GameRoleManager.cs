using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RampageCars
{
    public class GameRoleManager : SingletonBase
    {
        public void GameClear()
        {
            Singleton.Get<SceneLoader>().OverrideScene(SceneType.GameClear);
        }
        public void GameOver()
        {
            Singleton.Get<SceneLoader>().OverrideScene(SceneType.GameOver);
        }
    }
}
