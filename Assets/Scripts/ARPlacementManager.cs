using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using System.Collections.Generic;

public class ARPlacementManager : MonoBehaviour
{
    [Header("AR Components")]
    public ARRaycastManager raycastManager;
    public ARAnchorManager anchorManager;
    public ARPlaneManager planeManager;

    [Header("Placement Settings")]
    public float placementDistance = 0.5f;
    public LayerMask placementLayerMask;

    private GameObject currentToken;
    private EnigmaManager enigmaManager;

    private void Start()
    {
        enigmaManager = FindFirstObjectByType<EnigmaManager>();
    }

    public void SetCurrentToken(GameObject token)
    {
        currentToken = token;
    }

    private void Update()
    {
        if (currentToken == null) return;

        // Check for touch input
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                // Try to place the token
                PlaceToken(touch.position);
            }
        }
    }

    private void PlaceToken(Vector2 touchPosition)
    {
        List<ARRaycastHit> hits = new List<ARRaycastHit>();
        
        if (raycastManager.Raycast(touchPosition, hits, TrackableType.Planes))
        {
            // Get the first hit
            ARRaycastHit hit = hits[0];
            
            // Position the token
            currentToken.transform.position = hit.pose.position;
            currentToken.transform.rotation = hit.pose.rotation;

            // Check if the placement is correct
            CheckPlacementCorrectness(hit.pose.position);
        }
    }

    private void CheckPlacementCorrectness(Vector3 placementPosition)
    {
        // Get the current enigma's target anchor
        Transform targetAnchor = enigmaManager.enigmas[enigmaManager.currentEnigmaIndex].targetAnchor;
        
        if (targetAnchor != null)
        {
            // Check if the token is close enough to the target
            float distance = Vector3.Distance(placementPosition, targetAnchor.position);
            
            if (distance <= placementDistance)
            {
                // Correct placement
                enigmaManager.OnCorrectPlacement();
            }
        }
    }
} 