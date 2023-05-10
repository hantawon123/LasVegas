using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SelectSceneManager : MonoBehaviour
{
    public int numsofBot = 0;
    [SerializeField] private Text numberText;
    [SerializeField] private Button minusButton;
    [SerializeField] private Button plusButton;
    [SerializeField] private Button SelectButton;

    public static SelectSceneManager instance;
    private void Awake()
    {
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        minusButton.onClick.AddListener(MinusonClick);
        plusButton.onClick.AddListener(PlusonClick);
        SelectButton.onClick.AddListener(LoadGameScene);
    }

    // Update is called once per frame
    void Update()
    {
        numberText.text = numsofBot.ToString();
    }

    void PlusonClick()
    {
        if (numsofBot >= 5)
            numsofBot = 5;
        else
            numsofBot++;
    }
    void MinusonClick()
    {
        if (numsofBot <= 0)
            numsofBot = 0;
        else
            numsofBot--;
    }
    void LoadGameScene()
    {
        SceneManager.LoadScene("GameScene");
    }

}
