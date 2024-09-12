// UNUSED
// Tutorial stage to teach user controls

using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine;
using Unity.Collections;
using Unity.VisualScripting;
using TMPro;

public class Tutorial : MonoBehaviour
{
    [SerializeField] private Button leftButton;
    [SerializeField] private Button rightButton;
    [SerializeField] private TextMeshProUGUI tutorialText;
    [SerializeField] private Canvas tutorialCanvas; 
    [SerializeField] private CloudSpawner cloudSpawner;
    public CountDownTimer timer;
    public static bool isTutorialPicked = false;
    private bool rightButtonClicked = false;
    private bool leftButtonClicked = false;

    void Start()
    {
        tutorialCanvas.gameObject.SetActive(true);
    }

    public void ChooseTutorial(string level)
    {
        isTutorialPicked = true;
        switch(level)
        {
            case "Stage 1-1":
                Start1_1Tutorial();
                break;
            case "Stage 1-2":
                Start1_2Tutorial();
                Debug.Log("Stage 1-2 tutorial not implemented yet.");
                break;
            default:
                Debug.LogWarning("Unknown tutorial level: " + level);
                break;
        }
    }

    public void Start1_1Tutorial()
    {
        if(!leftButton){
            Debug.LogError("Left Button is not referenced!");
            return;
        }
        if(!rightButton)
        {
            Debug.LogError("Right Button is not referenced!");
            return;
        }

        ToggleLeftButton();
        ToggleRightButton();

        StartRightClickTutorial();
    }

    public void Start1_2Tutorial()
    {
        StartJumpUpTutorial();
    }

    /*
     * Coroutine starters for any of the tutorial
     */
    public void StartRightClickTutorial()
    {
        StartCoroutine(RightClickTutorial());
    }
    public void StartLeftClickTutorial()
    {
        StartCoroutine(LeftClickTutorial());
    }
    public void StartRuleAndMistake()
    {
        StartCoroutine(ExplainRuleAndMistake());
    }

    public void StartJumpUpTutorial()
    {
        StartCoroutine(JumpUpTutorial());
    }

    public IEnumerator JumpUpTutorial()
    {
        Debug.Log("Jump Tutorial starts");
        yield return new WaitUntil(() => (cloudSpawner.NextCloudPosition().x == cloudSpawner.CurrentCloudPosition().x));
        tutorialCanvas.gameObject.SetActive(true);
        tutorialText.text = "To jump up, press on both of the buttons at the same time";
        yield return new WaitUntil(() => (PlayerPrefs.GetString("Direction") == NimbusJump.DIRECTION_U));

        tutorialText.text = "Nicely Done! Game will resume in 3 seconds";
        yield return new WaitForSeconds(3);
        timer.StartCountDown();
        EndOfTutorial();

    }

    // Tutorial for right click
    public IEnumerator RightClickTutorial()
    {
        timer.StopCountDown();

        tutorialText.text = "Welcome to the Adventure of Ninja Nimbus!";
        yield return new WaitForSeconds(3);
        tutorialText.text = "You will learn about basic controls and rules.";
        yield return new WaitForSeconds(3);
        tutorialText.text = "Nimbus wants to jump right and catch cloud!";
        yield return new WaitForSeconds(2);

        // Wait until Right click
        if(!rightButton.IsInteractable())
        {
            ToggleRightButton();
        }
        rightButtonClicked = false;
        tutorialText.text = "Click the orange button on the right to jump right.";
        rightButton.onClick.AddListener(OnRightButtonClick);
        yield return new WaitUntil(() => rightButtonClicked);

        rightButton.onClick.RemoveListener(OnRightButtonClick);
        StopCoroutine(RightClickTutorial());

        timer.StartCountDown();
        ToggleRightButton();

        Debug.Log("Successfully triggered right jump!");

        StartLeftClickTutorial();
    }


    // Tutorial for left click
    public IEnumerator LeftClickTutorial()
    {
        timer.StopCountDown();

        tutorialText.text = "Nimbus wants to jump left and catch cloud!";
        yield return new WaitForSeconds(2);

        // Wait until Left Click
        if(!leftButton.IsInteractable())
        {
            ToggleLeftButton();
        }
        leftButtonClicked = false;
        leftButton.onClick.AddListener(OnLeftButtonClick);
        tutorialText.text = "Click the blue button on the left to jump left.";
        yield return new WaitUntil(() => leftButtonClicked);

        leftButton.onClick.RemoveListener(OnLeftButtonClick);

        timer.StartCountDown();
        ToggleLeftButton();
    
        Debug.Log("Successfully triggered left jump!");

        StartRuleAndMistake();
    }

    // Tutorial for what happens when you make a mistake
    private IEnumerator ExplainRuleAndMistake()
    {
        if(!leftButton.IsInteractable())
        {
            ToggleLeftButton();
        }

        leftButtonClicked = false;
        timer.StopCountDown();
        leftButton.onClick.AddListener(OnLeftButtonClick);

        // Prompt User to click left to trigger a mistake
        tutorialText.text = "Make Nimbus jump left to proceed.";
        yield return new WaitUntil(() => leftButtonClicked);

        leftButton.onClick.RemoveListener(OnLeftButtonClick);

        // Explain what happens when you make a mistake
        tutorialText.text = "Mistake count increases everytime you jump to the wrong direction or attempt to jump before Nimbus lands on cloud.";
        yield return new WaitForSeconds(5);
        tutorialText.text = "Your final score is reminaing time - mistake counts. You need 5 points to pass the stage!";
        yield return new WaitForSeconds(4);
        tutorialText.text = "Make it to the top with as little mistakes and as much time left as possible!";
        yield return new WaitForSeconds(4);
        tutorialText.text = "Game will resume in 3 seconds";
        yield return new WaitForSeconds(3);
        timer.StartCountDown();
        EndOfTutorial();
    }

    void EndOfTutorial()
    {
        Debug.Log("Tutorial has finished and game should continue!");
        if(!leftButton.interactable)
        {
            ToggleLeftButton();
        }
        if(!rightButton.interactable)
        {
            ToggleRightButton();
        }
        tutorialCanvas.gameObject.SetActive(false);
    }

    private void OnLeftButtonClick()
    {  
        Debug.Log("On Left Button CLick called!");
        leftButtonClicked = true;
    }

    private void OnRightButtonClick()
    {
        Debug.Log("On Left Button CLick called!");
        rightButtonClicked = true;
    }

    void ToggleLeftButton()
    {
        if(leftButton.IsInteractable())
        {
            leftButton.interactable = false;
        }
        else
        {
            leftButton.interactable = true;
        }
    }
    void ToggleRightButton()
    {
        if(rightButton.IsInteractable())
        {
            rightButton.interactable = false;
        }
        else
        {
            rightButton.interactable = true;
        }
    }
}
