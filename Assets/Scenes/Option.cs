using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace RampageCars
{
    [RequireComponent(typeof(Button))]
    public class Option : MonoBehaviour
    {

        [SerializeField]
        GameObject HowToMenu;

        [SerializeField]
        GameObject PauseFristButoom;

       [SerializeField]
        GameObject OptionFristButoom;

        // Start is called before the first frame update
        void Start()
        {
            
        }

        // Update is called once per frame
        void Update()
        {
            if (!HowToMenu.activeInHierarchy)
            {
                GetComponent<Button>().onClick.AddListener(Open);
            }
            else
            {
                GetComponent<Button>().onClick.AddListener(Close);
            }
        }

        void Open()
        {
            HowToMenu.SetActive(true);

            //選択しているオブジェクトをクリア
            EventSystem.current.SetSelectedGameObject(null);

            // 新しいオブジェクトを選択してセット
            EventSystem.current.SetSelectedGameObject(OptionFristButoom);
        }
        void Close()
        {
            HowToMenu.SetActive(false);

            //選択しているオブジェクトをクリア
            EventSystem.current.SetSelectedGameObject(null);

            // 新しいオブジェクトを選択してセット
            EventSystem.current.SetSelectedGameObject(PauseFristButoom);
        }
    }
}
