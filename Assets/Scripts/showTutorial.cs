using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class showTutorial : MonoBehaviour
{

    [SerializeField] GameObject tutorialCanvas;


    private void Start()
    {
        tutorialCanvas.SetActive(false);
    }

    public void ShowTutorialCanvas() 
    {
        tutorialCanvas.SetActive(true);
    }

    public void HideTutorialCanvas()
    {
        tutorialCanvas.SetActive(false);
    }

}
