using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;
using Unity.VisualScripting;

public class Casino : MonoBehaviour
{
    public int casinoNum = 0;

    public int tempNum = 0;
    
    public int player1dc = 0;
    public int player2dc = 0;
    public int player3dc = 0;
    public int player4dc = 0;
    public int player5dc = 0;
    private bool Round = false;
    public PlayerManager playermanager;
    //
    public List<int> casinoPlayerList;
    public List<int> casinoMoneyList = new List<int>();
    public static Casino instance;
    //싱글톤 끝

    void Awake()
    {
        instance = this;
        //카지노 오브젝트명에서 몇번째인지 가져온다.
        casinoNum = (int)Char.GetNumericValue(this.name[this.name.Length - 1]);

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.R))
        {
            SendtoPlayer();
            PlayerManager.instance.SumMoney();
            Camera_Manager.instance.MainView();
            Camera_Manager.instance.CameraPos = 0;
            GameManager.instance.isReset = true;
            //Debug.Log("end");
        }
    }



    //카지노보드에서 플레이어게 돈을 옮겨주는 함수
    public void SendtoPlayer()
    {


        //PlayerList의 첫번째 인자가 승자
        string temp = "0";
        //list에 각 플레이어별로 배팅한 주사위 개수를 넣는다.
        if (player1dc != 0) casinoPlayerList.Add(player1dc);
        if (player2dc != 0) casinoPlayerList.Add(player2dc);
        if (player3dc != 0) casinoPlayerList.Add(player3dc);
        if (player4dc != 0) casinoPlayerList.Add(player4dc);
        if (player5dc != 0) casinoPlayerList.Add(player5dc);

        //내림차순으로 정렬
        casinoPlayerList.Sort();
        casinoPlayerList.Reverse();

        //send 
        while (casinoPlayerList.Count > 0)
        {

            //중복 되는지 확인하여 중복되는 주사위를 각 플레이어에게 반환
            CheckDupAReturn();


            //중복 주사위를 걸러내는 중에 playerlist가 변하므로 다시 확인
            if (casinoPlayerList.Count > 0)
            {
                if (casinoPlayerList[0] == player1dc) temp = "1";
                if (casinoPlayerList[0] == player2dc) temp = "2";
                if (casinoPlayerList[0] == player3dc) temp = "3";
                if (casinoPlayerList[0] == player4dc) temp = "4";
                if (casinoPlayerList[0] == player5dc) temp = "5";

                if (temp != "0")
                {
                    //중복되지 않은 카지노에 존재하는 동일 주사위를 알맞은 플레이어에게 돌려주고 playerlist에서 제거
                    ReturnDice(temp);
                    casinoPlayerList.RemoveAt(0);

                    //해당되는 플레이어 MoneyList에 순차적으로 전달하고 카지노 moneyList로부터 제거


                    //GameObject.Find(temp).GetComponent<CasinoPlayer>().PlayerMoneyList.Add(casinoMoneyList[0]);
                    int tempToint = Int32.Parse(temp);
                    //인데스가 0부터 시작하므로 -1

                    if (casinoMoneyList.Count > 0)
                    {
                        PlayerManager.instance.PlayerList[tempToint - 1].GetComponent<Player>().moneyList.Add(casinoMoneyList[0]);
                        casinoMoneyList.RemoveAt(0);
                    }

                }
            }
        }
        //playerlist를 다 사용하면 다음을 위해 초기화
        casinoPlayerList.Clear();
        casinoMoneyList.Clear();
        GameManager.instance.isRoundEnded = true;
    }


    public void CheckDupAReturn()
    {
        //가장 큰 수부터 비교하고 중복되는게 있으면 각 플레이어게 반환, 중복되지 않은 가장 큰 수를 찾을 때까지 반복
        bool tf = true;

        if (tf & casinoPlayerList.Count>=2)
        {
            //가장 큰 주사위 중복 수가 판별 되더라도 이후 

            //첫인자 즉 가장 큰 수가 중복되는지 countDup로 판별
            //https://www.techiedelight.com/ko/get-count-of-number-of-items-in-a-list-in-csharp/ 참조
            int countDup = casinoPlayerList.Where(x => x.Equals(casinoPlayerList[0])).Count();
                if (countDup > 1)
                {
                    CheckAReturnDice(casinoPlayerList[0]);

                //반환하므로 카지노의 중복이되는 playerdc변수를 0으로 초기화
                if (casinoPlayerList[0] == player1dc) player1dc = 0;
                if (casinoPlayerList[0] == player2dc) player2dc = 0;
                if (casinoPlayerList[0] == player3dc) player3dc = 0;
                if (casinoPlayerList[0] == player4dc) player4dc = 0;
                if (casinoPlayerList[0] == player5dc) player5dc = 0;

                for (int i=0; i<countDup; i++)
                    {
                        //중복되는 개수만큼 playerlist 제거
                        casinoPlayerList.RemoveAt(0);
                    }
                }
                else
                {
                //while문 끝내기
                tf = false;
                }
        }

    }

    public void ReturnDice( string playerNum)
    {
        //카지노에서 플레이어 번호에 맞는 주사위를 그 플레이어에게 반환
        GameObject temp; 
        //주사위가 검색되지 않을때까지 계속 넘긴다.
        while (transform.Find("Dice" + playerNum))
        {
            temp = transform.Find("Dice"+playerNum).gameObject;
            GameObject player1 = GameObject.Find(playerNum);
            temp.transform.parent = player1.transform;
        } 
    }

    public void CheckAReturnDice(int manyDice)
    {
        if(manyDice == player1dc) ReturnDice("1");
       
        if (manyDice == player2dc) ReturnDice("2");
       
        if (manyDice == player3dc) ReturnDice("3");
       
        if (manyDice == player4dc) ReturnDice("4");
       
        if (manyDice == player5dc) ReturnDice("5");
    
    }
}
