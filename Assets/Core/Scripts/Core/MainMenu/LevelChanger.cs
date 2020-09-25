using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace MainMenu.LevelSelection
{
    public class LevelChanger : MonoBehaviour
    {
        /// <summary>
        /// Loads the scene with the given BuildIndex. 
        /// </summary>
        /// <param name="index"></param>
        public void LoadLevelbyBuildIndex(int index)
        {
            StartCoroutine(LoadSceneAsync(index));
        }

        IEnumerator LoadSceneAsync(int index)
        {
            AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(index);

            while (!asyncOperation.isDone)
            {
                //TODO: Open LoadingScreen
                Debug.Log("Loading Scene with index " + index);
                yield return null;
            }
        }
        
    }
}