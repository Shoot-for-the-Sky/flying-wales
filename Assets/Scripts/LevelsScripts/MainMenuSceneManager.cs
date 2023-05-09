using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuSceneManager : MonoBehaviour
{
    public void PlayLevel(string sceneName)
    {
        Debug.Log("PlayLevel: " + sceneName);
        SceneManager.LoadScene(sceneName);
    }
}
