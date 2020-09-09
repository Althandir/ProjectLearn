using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace GameUI
{
    [RequireComponent(typeof(Button))]
    public class UI_SceneChangerButton : MonoBehaviour
    {
        [SerializeField] Scenes _SceneToLoad;

        public void LoadScene()
        {
            SceneManager.LoadScene((int)_SceneToLoad);
        }
    }

    enum Scenes
    {
        MainMenu, Tutorial
    }
}