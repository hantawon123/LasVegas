using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;
using System.Linq;

public class GameManager : MonoBehaviour
{

    public int round = 0;
    public int turn = 0;

    public bool isRoundEnded = false;
    public bool isReset = false;
    bool isGameEnded = false;
    public bool isBet = false;
    bool isProgress = false;
    public bool isRoundStart = true;


    public int WinPlayerNum = 0;

    public string nowPlayer;

    List<int> sumlist = new List<int>();

    [SerializeField] private Canvas Ui;
    [SerializeField] private Button Rollbtn;
    
    public bool isBetDone = true;

    public static GameManager instance;

    IEnumerator Wait()
    {
        yield return new WaitUntil(() => isProgress == true);

    }
    IEnumerator WaitEnd()
    {
        yield return new WaitUntil(() => isGameEnded == false);

    }
    private void Awake()
    {
        instance = this;
    }
    void Update()
    {

        if (isGameEnded)
        {
            sumlist.Add(PlayerManager.instance.player1.sum);
            sumlist.Add(PlayerManager.instance.player2.sum);
            sumlist.Add(PlayerManager.instance.player3.sum);
            sumlist.Add(PlayerManager.instance.player4.sum);
            sumlist.Add(PlayerManager.instance.player5.sum);
            isGameEnded = false;
        }
        if (isRoundEnded)
        {
            if (round == 4)
            {
                isGameEnded = true;
                StartCoroutine(WaitEnd());
                WinPlayerNum = SearchWinner(sumlist);
                SceneManager.LoadScene("EndScene");
            }
            else
            {
                round++;
                isRoundEnded = false;
                isRoundStart = true;
                Rule();
            }

        }
        if (isProgress)
        {
            Ui.enabled = false;
            //if (Input.GetKeyDown(KeyCode.A))//A키 누르면 주사위 굴림
            //{
                for (int i = 0; i < PlayerManager.instance.PlayerList.Count; i++)
                {
                    PlayerManager.instance.PlayerList[i].GetComponent<CasinoPlayer>().OnClickRoll();
                    isBet = true;
                    isProgress = false;
                    StartCoroutine(Wait());
                }
           // }
        }
        else
        {
            Ui.enabled = true;
        }


    }
    void Start()
    {

        Rollbtn.onClick.AddListener(SetProgress);

        
    }

    // Update is called once per frame


    private void RefreshList(List<int> list)
    {
        for (int i = 0; i < list.Count; i++)
            list[i] = 0;
    }
    private void Rule()
    {
        StartCoroutine(Wait());
    }


    public void SetProgress()
    {
        isProgress = true;
    }

    private int SearchWinner(List<int> list)//가장 돈이 많은 플레이어의 넘버를 반환해주는 함수
    {
        int maxNum = list.Max();
        int index = list.IndexOf(maxNum);
        list.RemoveAt(index);
        return index + 1;
    }

    public void nextTurn()
    {
        turn += 1;
        if (turn > 5) turn = 1;
        isBetDone = false;
    }
}