using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.EventSystems;


public class tankManager : MonoBehaviour
{
    // Start is called before the first frame update
    public ARRaycastManager arRaycastManager;
    public Camera arCam;
    public GameObject tigerPrefab;
    private List<ARRaycastHit> arRaycastHits = new List<ARRaycastHit>();
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        var touch = Input.GetTouch(0);
        Ray ray = arCam.ScreenPointToRay(touch.position);
        if (Input.touchCount > 0)
        {
            if (touch.phase == TouchPhase.Ended)
            {
                if (Input.touchCount == 1)
                {
                    if (arRaycastManager.Raycast(touch.position, arRaycastHits))
                    {
                        if (Physics.Raycast(ray, out RaycastHit hit))
                        {
                            if (hit.transform.tag == "tank")
                            {
                                DeleteTank(hit.transform.gameObject);
                                return;
                            }
                        }
                        var pose = arRaycastHits[0].pose;
                        CreateTank(pose.position);
                        return;
                    }
                }
            }

        }
    }
    private void CreateTank(Vector3 position)
    {
        Instantiate(tigerPrefab, position, Quaternion.identity);
    }

    private void DeleteTank(GameObject tankObj)
    {
        Destroy(tankObj);
    }
}
