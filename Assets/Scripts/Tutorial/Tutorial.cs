using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine;
using Unity.Collections;
using Unity.VisualScripting;

public class Tutorial : MonoBehaviour
{
    [SerializeField] private Button leftButton;
    [SerializeField] private Button rightButton;
    public CountDownTimer timer;
    private bool isTutorialPicked = false;
    private bool rightButtonClicked = false;
    private bool leftButtonClicked = false;


    public void ChooseTutorial(string level)
    {
        switch(level)
        {
            case "Stage 1-1":
                Start1_1Tutorial();
                break;
            case "Stage 1-2":
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

    public void StartRightClickTutorial()
    {
        StartCoroutine(RightClickTutorial());
    }

    public IEnumerator RightClickTutorial()
    {
        if(!rightButton.IsInteractable())
        {
            ToggleRightButton();
        }

        // Wait until click
        rightButtonClicked = false;
        timer.StopCountDown();
        rightButton.onClick.AddListener(OnRightButtonClick);

        Debug.Log("Start Waiting for user right click");
        yield return new WaitUntil(() => rightButtonClicked);

        rightButton.onClick.RemoveListener(OnRightButtonClick);
        StopCoroutine(RightClickTutorial());

        timer.StartCountDown();
        ToggleRightButton();

        Debug.Log("Successfully triggered right jump!");

        StartLeftClickTutorial();
    }

    public void StartLeftClickTutorial()
    {
        StartCoroutine(LeftClickTutorial());
    }
    public IEnumerator LeftClickTutorial()
    {
        if(!leftButton.IsInteractable())
        {
            ToggleLeftButton();
        }

        // Wait until click
        leftButtonClicked = false;
        timer.StopCountDown();
        leftButton.onClick.AddListener(OnLeftButtonClick);

        Debug.Log("Start Waiting for user left click");
        yield return new WaitUntil(() => leftButtonClicked);

        leftButton.onClick.RemoveListener(OnLeftButtonClick);

        timer.StartCountDown();
        ToggleLeftButton();
    
        Debug.Log("Successfully triggered left jump!");

        StartGameRuleExplanation();
    }

    public void StartGameRuleExplanation()
    {
        StartCoroutine(ExplainRule());
    }

    private IEnumerator ExplainRule()
    {
        if(!rightButton.IsInteractable())
        {
            ToggleRightButton();
        }

        rightButtonClicked = false;
        timer.StopCountDown();
        rightButton.onClick.AddListener(OnRightButtonClick);

        Debug.Log("Your goal is to get at least 5 points.");
        yield return new WaitUntil(() => rightButtonClicked);

        rightButton.onClick.RemoveListener(OnRightButtonClick);

        timer.StartCountDown();
        ContinueGame();
    }

    void ContinueGame()
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
