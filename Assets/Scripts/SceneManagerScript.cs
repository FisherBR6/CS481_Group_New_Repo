using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneManagerScript : MonoBehaviour
{
    //Need to make it an instance because I need to call in keybutton script. Can't use statics on enumerator.
    public static SceneManagerScript Instance { get; private set; }
    public CardboardReticlePointer reticlePointer;
 

    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }
    // Start is called before the first frame update
    // Assets/Scenes/QWERTY_Scene.unity
    // Assets/Scenes/New_Qwerty.unity
    void Start()
    {
        Debug.Log("Attempting to load QWERTY script...");
        SceneManager.LoadScene("New_Qwerty", LoadSceneMode.Additive);
    }

    public void LoadABC()
    {
        StartCoroutine(SwitchScenes("New_Qwerty", "New_Abc"));
    }

    public void LoadQWERTY()
    {
        StartCoroutine(SwitchScenes("New_Abc", "New_Qwerty"));
    }

    private IEnumerator SwitchScenes(string sceneUnload, string sceneLoad)
    {
        yield return new WaitForSeconds(0.1f);
        
        if (reticlePointer != null)
        {
            reticlePointer.ClearCurrentTarget();
            reticlePointer.enabled = false;
        }

        yield return SceneManager.UnloadSceneAsync(sceneUnload);

        yield return null;

        yield return SceneManager.LoadSceneAsync(sceneLoad, LoadSceneMode.Additive);

        yield return null;

        if (reticlePointer != null)
        {
            reticlePointer.enabled = true;
        }

        EventSystem.current.SetSelectedGameObject(null);

        RaycastHit hit;
        if (Physics.Raycast(reticlePointer.transform.position, reticlePointer.transform.forward, out hit))
        {
            Debug.Log("Reticle is pointing at: " + hit.collider.name);
        }
    }
}
