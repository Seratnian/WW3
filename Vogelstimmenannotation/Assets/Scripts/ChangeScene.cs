using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class ChangeScene : MonoBehaviour
{
    private string nextScene;
    private bool asynchronous = false;
    private AsyncOperation sceneLoading;

    void Start()
    {
        switch (SceneManager.GetActiveScene().name)
        {
            case "RootScene":
                nextScene = "LoadScene";
                EventManager.StartListening("useProfile", ChangeToScene);
                break;
            case "LoadScene":
                nextScene = "MainScene";
                asynchronous = true;
                break;
        }
        if (asynchronous)
        {
            StartCoroutine(Load());
        }
    }

    private void ChangeToScene(object data)
    {
        SceneManager.LoadScene(nextScene);
    }

    IEnumerator Load()
    {
        yield return new WaitForSeconds(5);
        sceneLoading = SceneManager.LoadSceneAsync(nextScene, LoadSceneMode.Single);
        sceneLoading.allowSceneActivation = true;
        while (!sceneLoading.isDone)
        {
            yield return new WaitForEndOfFrame();
        }
    }
}
