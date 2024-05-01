using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] Canvas gameOverCanvas;
    [SerializeField] Canvas GameWinCanvas;

    [SerializeField] Button mainMenuButton;
    [SerializeField] Button playAgainButton;
    [SerializeField] Button retryButton;

    //Retry and PlayAgain buttons use same function "RetryButtonClicked()"

    public static UIManager Instance;

    private void Awake()
    {
        if((Instance == null))
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        //Start off
        gameOverCanvas.gameObject.SetActive(false); 
        GameWinCanvas.gameObject.SetActive(false); 
    }

    public void ShowRetryScreen()
    {
        gameOverCanvas.gameObject.SetActive(true);
        //Retry button OnClick() handled in editor
    }

    public void RetryButtonClicked()
    {
        SceneManager.LoadScene(1); //Reload game scene
    }

    public void ShowWinScreen()
    {
        GameWinCanvas.gameObject.SetActive(true);
    }

    public void MainMenuButtonClicked()
    {
        SceneManager.LoadScene(0); //Load initial screen
    }

    
}
