using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class bubbleManager : MonoBehaviour
{
    [SerializeField] GameObject bubbleCountImage;
    [SerializeField] Text bubbleCountText;
    [SerializeField] GameObject levelCompleteText;
    [SerializeField] GameObject levelCompleteBubbles;
    [SerializeField] private Animator animatorCurtain;

    [Header("Audio Settings")]
    [SerializeField] AudioMixer audioMixer;
    [SerializeField] string mixerGroupName;
    [SerializeField] float fadeDuration;
    [SerializeField] float maxVolume;
    [SerializeField] GameObject levelCompleteAudio;

    [Header("Scene Manager")]
    [SerializeField] int sceneToLoadIndex;
    [SerializeField] bool lastLevel = false;
    [SerializeField] GameObject playAgainText;
    [SerializeField] int startSceneIndex;
    private bool canPlayAgain = false;

    public static int currentBubbleCount;

    private void Start()
    {
        //currentBubbleCount = transform.childCount;
        currentBubbleCount = 0;
        foreach (Transform child in transform)
        {
            if (child.gameObject.activeSelf)
                currentBubbleCount++;
        }
        playerMovement.playerCanMove = true;
        throwStar.canThrowStar = true;
        StartCoroutine(fadeMixerGroup.StartFade(audioMixer, mixerGroupName, fadeDuration, maxVolume));
        print("bubbleManager: Initial active bubble count "+ currentBubbleCount);
        bubbleCountImage.SetActive(true);
        levelCompleteText.SetActive(false);
        levelCompleteBubbles.SetActive(false);
        levelCompleteAudio.SetActive(false);
        playAgainText.SetActive(false);
    }

    private void Update()
    {
        if (currentBubbleCount == 0) {
            playerMovement.playerCanMove = false;
            throwStar.canThrowStar = false;
            levelCompleteText.SetActive(true);
            bubbleCountImage.SetActive(false);
            bubbleCountText.text = "";
            levelCompleteAudio.SetActive(true);
            StartCoroutine(fadeMixerGroup.StartFade(audioMixer, mixerGroupName, fadeDuration, 0f));

            if (sceneToLoadIndex >= 0)
            {
                StartCoroutine(LoadLevelAfterDelay(fadeDuration, sceneToLoadIndex));
            }
            if (lastLevel)
            {
                StartCoroutine(ShowPlayAgain(fadeDuration, playAgainText));
            }
            levelCompleteBubbles.SetActive(true);
            animatorCurtain.SetBool("levelComplete", true);
        }

        if (Input.GetButtonDown("Jump") && canPlayAgain)
        {
            SceneManager.LoadScene(startSceneIndex);
        }
    }
    void LateUpdate()
    {
        if (currentBubbleCount == 0)
        {
            bubbleCountText.text = "";
        }
        else
        {
            bubbleCountText.text = currentBubbleCount.ToString();
        }
    }
    IEnumerator LoadLevelAfterDelay(float delay, int sceneIndex)
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene(sceneIndex);
    }
    IEnumerator ShowPlayAgain(float delay, GameObject playAgainText)
    {
        yield return new WaitForSeconds(delay);
        playAgainText.SetActive(true);
        canPlayAgain = true;
    }
}
