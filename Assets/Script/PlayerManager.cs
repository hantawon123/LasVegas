using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{

    private Player player;
    //추후 다이스 완성되면 다이스 리스트 추가
    [SerializeField] public GameObject Player1;//플레이어 게임오브젝트 리스트
    [SerializeField] public GameObject Player2;
    [SerializeField] public GameObject Player3;
    [SerializeField] public GameObject Player4;
    [SerializeField] public GameObject Player5;


    public Player player1;
    public Player player2;
    public Player player3;
    public Player player4;
    public Player player5;

    int inumsofBot;//Player스크립트의 bot 수를 담아줄 변수

    public List<GameObject> PlayerList = new List<GameObject>();
    public static PlayerManager instance;

    // Start is called before the first frame update
    void Awake()
    {
        instance = this;
        player1 = Player1.GetComponent<Player>();
        player2 = Player2.GetComponent<Player>();
        player3 = Player3.GetComponent<Player>();
        player4 = Player4.GetComponent<Player>();
        player5 = Player5.GetComponent<Player>();
    }
    void Start()
    {
        inumsofBot = SelectSceneManager.instance.numsofBot;
        //플레이어 오브젝트 1~5를 리스트로 관리
        PlayerList.Add(Player1);
        PlayerList.Add(Player2);
        PlayerList.Add(Player3);
        PlayerList.Add(Player4);
        PlayerList.Add(Player5);
        SetBot();

        //플레이어 머니리스트1~5를 관리해주는 리스트

        //플레이어인가 봇인가 확인

    }


    public List<int> PMGetDiceList(int playerIndex)
    {
        var tempPlayerList = PlayerList[playerIndex - 1].GetComponent<CasinoPlayer>().GetDiceList();
        //PlayerManager의 tempPlayerList사용

        return tempPlayerList;
    }

    public void SumMoney()//각 플레이어 머니리스트 합
    {
        
        for(int i = 0; i < PlayerManager.instance.PlayerList.Count;i++)
        {
            List<int> list = PlayerManager.instance.PlayerList[i].GetComponent<Player>().moneyList;
            PlayerManager.instance.PlayerList[i].GetComponent<Player>().sum = 0;

            for (int j = 0; j < list.Count;j++)
            {
                PlayerManager.instance.PlayerList[i].GetComponent<Player>().sum += list[j];
            }
        }
    }
    public void SetBot()
    {
        for (int i = 0; i < inumsofBot; i++)
        {
            PlayerList[4 - i].GetComponent<Player>().isBot = true;
        }
    }
}

