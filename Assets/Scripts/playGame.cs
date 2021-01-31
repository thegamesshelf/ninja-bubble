using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class playGame : MonoBehaviour
{
    [Header("Audio Settings")]
    [SerializeField] AudioMixer audioMixer;
    [SerializeField] string mixerGroupName;
    [SerializeField] float fadeDuration;
    [SerializeField] float maxVolume;

    [Header("Other Settings")]
    public GameObject parentObject;
    [SerializeField] int loadSceneIndex;
    [SerializeField] GameObject tutorialCanvas;

    private Animator animator;

    // place canvas = 0 and tutorial canvas = 1
    private int currentCanvas;
    // Start is called before the first frame update

    private void Start()
    {
        animator = parentObject.GetComponent<Animator>();
        animator.SetBool("a_startGame", false);
        currentCanvas = 0;
    }

    void Update()
    {
        if (Input.GetButtonDown("Jump") && currentCanvas == 0)
        {
            print("Play the Game.");
            StartGame();
        }
        else if (Input.GetButtonDown("Cancel") && currentCanvas == 0)
        {
            tutorialCanvas.SetActive(true);
            currentCanvas = 1;
        }
        else if (Input.GetButtonDown("Cancel") && currentCanvas == 1)
        {
            tutorialCanvas.SetActive(false);
            currentCanvas = 0;
        }
    }

    public void StartGame()
    {
        StartCoroutine(fadeMixerGroup.StartFade(audioMixer, mixerGroupName, fadeDuration, maxVolume));
        StartCoroutine(fadeOut());
        //print("playerName:" + playerName.text);
        //levelLoader.playerNameSTR = playerName.text;
        //load
        //SceneManager.LoadScene(0);
    }

    IEnumerator fadeOut()
    {
        animator.SetBool("a_startGame", true);
        yield return new WaitForSeconds(2f);
        //load
        SceneManager.LoadScene(loadSceneIndex);
    }

}
