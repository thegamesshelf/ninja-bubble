using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mainCameraParallax : MonoBehaviour
{
    [SerializeField] GameObject[] layers;
    [SerializeField] float parallaxAmount = 1f;
    private Vector3 previousPosition;
    private Vector3 positionDelta;
    // Start is called before the first frame update
    void Start()
    {
        previousPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        // Calculate change in position of camera
        positionDelta = (previousPosition - transform.position)*-1;
        // If there is a change do something
        if (positionDelta != Vector3.zero) {
            // for each layer
            foreach (GameObject layer in layers) {
                // apply the change
                layer.transform.position = Vector3.Lerp(
                    layer.transform.position,
                    layer.transform.position + positionDelta,
                    (layer.transform.position.z* parallaxAmount) * Time.deltaTime
                );
            }
        }
        // zero out the change in position
        previousPosition = transform.position;
    }
}
