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
        SceneManager.LoadScene("QWERTY_Scene", LoadSceneMode.Additive);
    }

    public void LoadABC()
    {
        SceneManager.LoadScene("ABC_Scene", LoadSceneMode.Additive);
    }
}
