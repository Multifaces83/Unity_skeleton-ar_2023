using UnityEngine;
using System.Collections.Generic;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using UnityEngine.UI;

public class InterfaceController : MonoBehaviour
{
    [Header("Prefab to place")]
    [SerializeField] private GameObject _prefab;
    private GameObject spawnedObject;

    [Header("Pointer Symbol")]
    [SerializeField] private Image _pointerSymbol;


    [Header("AR Foundation Components")]
    [SerializeField] private ARRaycastManager _aRRaycastManager;
    [SerializeField] private ARPlaneManager _arPlaneManager;
    private List<ARRaycastHit> _hits = new List<ARRaycastHit>(); // List to hold raycast hits

    private Camera _arCamera;

    private void Awake()
    {
        //Singleton pattern
        InterfaceControllerSingleton singleton = InterfaceControllerSingleton.Instance;

        _pointerSymbol.color = Color.white;
        _arCamera = Camera.main;

    }

    private void Update()
    {
        RaycastFromCamera();
    }

    private void RaycastFromCamera()
    {
        Ray ray = _arCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));

        _hits.Clear(); // Clear the list before performing a new raycast

        //trackable type plane within polygon is used to detect planes
        if (_aRRaycastManager.Raycast(ray, _hits, TrackableType.PlaneWithinPolygon))
        {
            _pointerSymbol.color = Color.green; //color to indicate interaction enable

            // Check for touch anywhere on the screen
            if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
            {
                Pose hitPose = _hits[0].pose; // Access the first hit in the list


                if (spawnedObject != null)
                {
                    Destroy(spawnedObject);
                }

                spawnedObject = Instantiate(_prefab, hitPose.position, hitPose.rotation);
            }
        }
        else
        {
            _pointerSymbol.color = Color.white; //color to indicate no interaction
        }
    }

}

