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
            if (_SceneToLoad == Scenes.ActiveLevel)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
            else
            {
                SceneManager.LoadScene((int)_SceneToLoad);
            }
        }
    }

    enum Scenes
    {
        MainMenu, ActiveLevel
    }
}