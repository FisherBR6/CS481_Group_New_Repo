using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class InputManager : MonoBehaviour
{

    public Camera mainCamera;
    public float dwellTime = .3f; // seconds to trigger a dwell
    public bool useDwell = false;

    //tracks currently gazed at objects 
    private GameObject currentTarget = null;
    private float gazeTimer = 0f;


    //key colors 
    private Color hoverColor = Color.yellow;

    //progress bar 
    [SerializeField] private Image dwellProgressImage;

    private SceneManagerScript sceneManagerScript;

    public void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    public void Start()
    {

    }
    private void Update()
    {
        Ray ray;

        // Determine platform-specific ray
        if (Application.isEditor || Application.platform == RuntimePlatform.WindowsPlayer)
        {
            ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        }
        else
        {
            var reticlePointer = mainCamera.GetComponentInChildren<CardboardReticlePointer>();
            if (reticlePointer == null) return; //exit if no reticle is found

            ray = new Ray(reticlePointer.transform.position, reticlePointer.transform.forward);
        }
        //handle gaze unteractions 
        HandleRaycast(ray);

        //if not in dwell mode allow taps to trigger keys 
        if (!useDwell && Google.XR.Cardboard.Api.IsTriggerPressed && currentTarget != null)
        {
            Debug.Log("Tap deteected! Triggering key: " + currentTarget.name);
            TriggerKey(currentTarget);
        }
       
    }

    //highlights key when hovered 
    void OnHover(GameObject key)
    {
        var renderer = key.GetComponent<KeyButton>();
        if (renderer != null)
        {
            renderer.SetHoverColor(hoverColor);
        }
    }

    //resets color when gaze exits 
    void OnExit(GameObject key)
    {
        var renderer = key.GetComponent<KeyButton>();
        if (renderer != null)
        {
            renderer.ResetColor();
        }
    }

    //handles gaze detection and dwell logic 
    void HandleRaycast(Ray ray)
    {
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            GameObject hitObj = hit.collider.gameObject;

            if (hitObj.CompareTag("KeyButton"))
            {
                //if the target has changed, update hover/exit effects
                if (hitObj != currentTarget)
                {
                    if (currentTarget != null)
                    {
                        OnExit(currentTarget); //exits previous key 
                    }
                    currentTarget = hitObj;//set new object as target 
                    gazeTimer = 0f; 
                    OnHover(currentTarget); //highlight new key
                }
                else if (useDwell)
                {
                    //update dwell progress bar 
                    gazeTimer += Time.deltaTime;
                    float progress = Mathf.Clamp01(gazeTimer / dwellTime);

                    if (dwellProgressImage != null)
                    {
                        dwellProgressImage.fillAmount = progress;
                    }

                    if (gazeTimer >= dwellTime)
                    {
                        //trigger key when time is reached 
                        Debug.Log("Dwell time reached Triggering key: " + currentTarget.name);
                        TriggerKey(currentTarget);
                        gazeTimer = 0f; 

                        if (dwellProgressImage != null)
                            dwellProgressImage.fillAmount = 0f;
                    }
                }
            }
            else { 
            
               ResetGaze();
            }
        }
    }

    void ResetGaze()
    {
        if (currentTarget != null)
        {
            OnExit(currentTarget);
            currentTarget = null;
        }
        gazeTimer = 0f;

        if (dwellProgressImage != null)
        {
            dwellProgressImage.fillAmount = 0f;
        }
    }

    void TriggerKey(GameObject keyObj)
    {
        Debug.Log($"Key triggered: {keyObj.name}");
        keyObj.SendMessage("OnKeyPress", SendMessageOptions.DontRequireReceiver);
    }


    public void ToggleInputMode()
    {
        useDwell = !useDwell;
        Debug.Log("Input mode changed: " + (useDwell ? "Dwell" : "Click"));
    }
}