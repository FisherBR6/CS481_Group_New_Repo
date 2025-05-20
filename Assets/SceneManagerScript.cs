using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneManagerScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Attempting to load QWERTY script...");
        SceneManager.LoadScene("New_Qwerty", LoadSceneMode.Additive);
    }

    public static void LoadABC()
    {
        SceneManager.UnloadSceneAsync("New_Qwerty");
        SceneManager.LoadScene("New_Abc", LoadSceneMode.Additive);
    }

    public static void LoadQWERTY()
    {
        SceneManager.UnloadSceneAsync("New_Abc");
        SceneManager.LoadScene("New_Qwerty", LoadSceneMode.Additive);
    }
}
