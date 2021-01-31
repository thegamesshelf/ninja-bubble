using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class introController : MonoBehaviour
{
    void Update()
    {
        if (Input.GetButtonDown("Jump"))
        {
            print("Play the Game.");
        }
        else if (Input.GetButtonDown("Cancel"))
        {
            print("Show Tutorial.");
        }
    }
}
