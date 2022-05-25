using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace RampageCars
{
    //[RequireComponent(typeof(Button))]
    public class Option : MonoBehaviour
    {

        [SerializeField]
        GameObject HowToMenu;

        [SerializeField]
        GameObject PauseFristButoom;

       [SerializeField]
        GameObject OptionFristButoom;
        public void OpenHowTo()
        {
            HowToMenu.SetActive(true);

            //選択しているオブジェクトをクリア
            EventSystem.current.SetSelectedGameObject(null);

            // 新しいオブジェクトを選択してセット
            EventSystem.current.SetSelectedGameObject(OptionFristButoom);
        }

        public void CloseHowTo()
        {
            HowToMenu.SetActive(false);

            //選択しているオブジェクトをクリア
            EventSystem.current.SetSelectedGameObject(null);

            // 新しいオブジェクトを選択してセット
            EventSystem.current.SetSelectedGameObject(PauseFristButoom);
        }
    }
}
