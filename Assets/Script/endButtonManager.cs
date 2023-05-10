using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class endButtonManager : MonoBehaviour
{
    [SerializeField] private Button rButton;

    
    void Awake()
    {
        rButton.onClick.AddListener(LoadStartScene);
    }

    private void LoadStartScene()
    {
        SceneManager.LoadScene("StartScene");
    }



}

