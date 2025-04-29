using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagerScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        SceneManager.LoadScene("QWERTY_Scene", LoadSceneMode.Additive);
    }

    public void LoadABC()
    {
        SceneManager.LoadScene("ABC_Scene", LoadSceneMode.Additive);
    }
}
