using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class mainCameraParallaxChildren : MonoBehaviour
{
    [SerializeField] GameObject[] layers;
    [SerializeField] float parallaxAmount = 1f;
    private Vector3 previousPosition;
    private Vector3 positionDelta;
    private List<float> zDepths = new List<float>();
    private GameObject objToSpawn;
    private List<GameObject> childLayers = new List<GameObject>();
    private int i = 0;
    private bool zZeroIgnored = false;
    // Start is called before the first frame update
    void Start()
    {
        // get start position of camera
        previousPosition = transform.position;

        // group layers
        foreach (GameObject layer in layers) {
            // ignore if layer had z=zero
            if (layer.transform.position.z == 0f)
            {
                zZeroIgnored = true;
            }
            else { 
                zDepths.Add((int)layer.transform.position.z); 
            }
        }
        zDepths = zDepths.Distinct().ToList();
        zDepths.Sort();
        print("mainCameraParallaxChildren: Total z groups found: " + zDepths.Count);
        if (zZeroIgnored) {
            print("mainCameraParallaxChildren: Zero group ignored.");
        }

        i = 0;
        foreach (float zDepth in zDepths) {
            objToSpawn = new GameObject("Parallax z " + zDepth);
            objToSpawn.transform.position = new Vector3(0f,0f,zDepth);
            foreach (GameObject layer in layers) {
                if (zDepth == (int)layer.transform.position.z)
                {
                    layer.transform.parent = objToSpawn.transform;
                }
            }
            childLayers.Add(objToSpawn);
            i++;
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Calculate change in position of camera
        positionDelta = (previousPosition - transform.position) * -1;
        // If there is a change do something
        if (positionDelta != Vector3.zero)
        {
            // for each layer
            foreach (GameObject layer in childLayers)
            {
                // apply the change
                layer.transform.position = Vector3.Lerp(
                    layer.transform.position,
                    layer.transform.position + positionDelta,
                    (layer.transform.position.z * parallaxAmount) * Time.deltaTime
                );
            }
        }
        // zero out the change in position
        previousPosition = transform.position;
    }

}

