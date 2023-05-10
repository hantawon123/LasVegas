using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class StartSceneManager : MonoBehaviour
{
    [SerializeField] private Button sButton;
    [SerializeField] private Button qButton;

    void Awake()
    {
        sButton.onClick.AddListener(LoadGameScene);
        qButton.onClick.AddListener(QuitGame);
    }

    private void LoadGameScene()
    {
        SceneManager.LoadScene("SelectScene");
    }

    private void QuitGame()
    {
        Application.Quit();
    }


}