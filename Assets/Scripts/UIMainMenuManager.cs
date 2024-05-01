using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIMainMenuManager : MonoBehaviour
{
    [SerializeField] Button playButton;

    public void LoadFirstLevel()
    {
        SceneManager.LoadScene(1);
    }
}
