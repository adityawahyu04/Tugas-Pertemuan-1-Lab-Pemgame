using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    //variabel
    public GameObject loadingScreen;
    public Slider loadingBar;
    public Text loadingText;
    public static bool loadStatus;

    public void Loadlevel(int sceneIndex)
    {
        loadingScreen.SetActive(true);
        StartCoroutine(LoadAsync(sceneIndex));
        
    }

    IEnumerator LoadAsync(int sceneIndex)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneIndex);
        
        while (!operation.isDone)
        {
            int progress = (int)Mathf.Clamp01(operation.progress / .9f);

            loadingBar.value = progress;
            loadingText.text = progress * 100 + "%";

            yield return null;
        }
}   }
