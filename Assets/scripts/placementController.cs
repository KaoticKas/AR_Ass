using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;
using UnityEngine.EventSystems;

[RequireComponent(typeof(ARRaycastManager))]

public class placementController : MonoBehaviour
{
    [SerializeField]
    private Button tiger;
    [SerializeField]
    private Button t34;
    [SerializeField]
    private Button sherman;
    [SerializeField]
    private Button kv1;
    [SerializeField]
    private Button delete;
    [SerializeField]
    private Button stopDelete;


    public Camera arCam;
    private GameObject placedPrefab;
    private ARRaycastManager arRaycastManager;
    private static List<ARRaycastHit> hits = new List<ARRaycastHit>();

    private bool spawnEnabled = false;
    public GameObject SpawnPanel;
    //public Text debugText;
    private bool deleteObj;
    // Start when the instance is loaded
    void Awake()
    {
        arRaycastManager = GetComponent<ARRaycastManager>();
        tiger.onClick.AddListener(() => ChangePrefabTo("InteractableTiger"));
        t34.onClick.AddListener(() => ChangePrefabTo("t34int"));
        sherman.onClick.AddListener(() => ChangePrefabTo("shermanInt"));
        kv1.onClick.AddListener(() => ChangePrefabTo("kv1Int"));
        delete.onClick.AddListener(()=> CanDelete());
        stopDelete.onClick.AddListener(() => CantDelete());
        //buttons that will execute specigic functions
        //ChangePrefabTo("KV-1");
    }

    void Update()
    {
        if (SpawnPanel.activeInHierarchy)
        {
            spawnEnabled = true;
        }
        else
        {
            spawnEnabled = false;
        }
        //tankHit = false;
        //debugLog("tank has been hit :False");
        var touch = Input.GetTouch(0);
        Ray ray = arCam.ScreenPointToRay(touch.position);
        if (Input.touchCount > 0)
        {
            if (touch.phase == TouchPhase.Ended)
            {
                if (Input.touchCount == 1)
                {
                    if (arRaycastManager.Raycast(touch.position, hits))
                    {
                        bool isOverUI = EventSystem.current.IsPointerOverGameObject(touch.fingerId);
                        if (isOverUI)
                        {
                            //debugLog("yes");
                            return;
                        }
                        if (Physics.Raycast(ray, out RaycastHit hit))
                        {
                            if (hit.transform.tag == "tank")
                            {
                                if (deleteObj !=false)
                                {
                                    //bool to check if delete button was pressed to allow for deleting objects
                                    DeleteTank(hit.transform.gameObject);
                                    return;
                                }
                                else
                                {
                                    return;
                                }

                            }
                                //tankHit = true;
                        }
                        if(spawnEnabled == true)
                        {
                            var pose = hits[0].pose;
                            CreateTank(pose.position);
                            return;

                        }
                        else
                        {
                            return;
                        }

                    }
                }
            }
        }
    }


    //public void debugLog(string name)
    //{
    //    debugText.text = name;
    //}

    private void CreateTank(Vector3 position)
    {
        Instantiate(placedPrefab, position, Quaternion.identity);
    }
    //create tank

    void ChangePrefabTo(string prefabName)
    {
        placedPrefab = Resources.Load<GameObject>($"prefabs/{prefabName}");
        if (placedPrefab == null)
        {
            Debug.LogError($"Prefab with name {prefabName} could not be loaded, make sure you check the naming of your prefabs...");
        }
    }
    void CanDelete()
    {
        deleteObj = true;
        //debugLog("true");
    }
    void CantDelete()
    {
        deleteObj = false;
        //debugLog("false");
    }
    //functions to stop and start spawning of vehicles
    private void DeleteTank(GameObject tankObj)
    {
        Destroy(tankObj);
    }
}
