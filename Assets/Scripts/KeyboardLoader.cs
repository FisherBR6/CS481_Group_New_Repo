using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class KeyboardLoader : MonoBehaviour
{
    public string currentKeyboardScene = "New_Qwerty";
    public string abcKeyboardScene = "New_Abc";

    public void SwitchToABCKeyboard()
    {
        StartCoroutine(SwitchKeyboard());
    }

    private IEnumerator SwitchKeyboard()
    {
        // Unload current keyboard scene
        AsyncOperation unload = SceneManager.UnloadSceneAsync(currentKeyboardScene);
        yield return unload;

        // Load new keyboard scene
        AsyncOperation load = SceneManager.LoadSceneAsync(abcKeyboardScene, LoadSceneMode.Additive);
        yield return load;

        // Update current keybaord to abc
        currentKeyboardScene = abcKeyboardScene;
    }

}
