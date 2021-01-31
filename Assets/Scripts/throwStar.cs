using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class throwStar : MonoBehaviour
{
    public static bool canThrowStar = true;
    [SerializeField] Transform firePoint;
    [SerializeField] GameObject throwingStarPrefab;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1") && canThrowStar) {
            Shoot();
        }
    }

    void Shoot() {
        Instantiate(throwingStarPrefab, firePoint.position, firePoint.rotation);
        // the star applies a movement force to itself, here we only instantiate it.
    }
}
