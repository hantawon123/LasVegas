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
    //�̱��� ��

    void Awake()
    {
        instance = this;
        //ī���� ������Ʈ���� ���°���� �����´�.
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



    //ī���뺸�忡�� �÷��̾�� ���� �Ű��ִ� �Լ�
    public void SendtoPlayer()
    {


        //PlayerList�� ù��° ���ڰ� ����
        string temp = "0";
        //list�� �� �÷��̾�� ������ �ֻ��� ������ �ִ´�.
        if (player1dc != 0) casinoPlayerList.Add(player1dc);
        if (player2dc != 0) casinoPlayerList.Add(player2dc);
        if (player3dc != 0) casinoPlayerList.Add(player3dc);
        if (player4dc != 0) casinoPlayerList.Add(player4dc);
        if (player5dc != 0) casinoPlayerList.Add(player5dc);

        //������������ ����
        casinoPlayerList.Sort();
        casinoPlayerList.Reverse();

        //send 
        while (casinoPlayerList.Count > 0)
        {

            //�ߺ� �Ǵ��� Ȯ���Ͽ� �ߺ��Ǵ� �ֻ����� �� �÷��̾�� ��ȯ
            CheckDupAReturn();


            //�ߺ� �ֻ����� �ɷ����� �߿� playerlist�� ���ϹǷ� �ٽ� Ȯ��
            if (casinoPlayerList.Count > 0)
            {
                if (casinoPlayerList[0] == player1dc) temp = "1";
                if (casinoPlayerList[0] == player2dc) temp = "2";
                if (casinoPlayerList[0] == player3dc) temp = "3";
                if (casinoPlayerList[0] == player4dc) temp = "4";
                if (casinoPlayerList[0] == player5dc) temp = "5";

                if (temp != "0")
                {
                    //�ߺ����� ���� ī���뿡 �����ϴ� ���� �ֻ����� �˸��� �÷��̾�� �����ְ� playerlist���� ����
                    ReturnDice(temp);
                    casinoPlayerList.RemoveAt(0);

                    //�ش�Ǵ� �÷��̾� MoneyList�� ���������� �����ϰ� ī���� moneyList�κ��� ����


                    //GameObject.Find(temp).GetComponent<CasinoPlayer>().PlayerMoneyList.Add(casinoMoneyList[0]);
                    int tempToint = Int32.Parse(temp);
                    //�ε����� 0���� �����ϹǷ� -1

                    if (casinoMoneyList.Count > 0)
                    {
                        PlayerManager.instance.PlayerList[tempToint - 1].GetComponent<Player>().moneyList.Add(casinoMoneyList[0]);
                        casinoMoneyList.RemoveAt(0);
                    }

                }
            }
        }
        //playerlist�� �� ����ϸ� ������ ���� �ʱ�ȭ
        casinoPlayerList.Clear();
        casinoMoneyList.Clear();
        GameManager.instance.isRoundEnded = true;
    }


    public void CheckDupAReturn()
    {
        //���� ū ������ ���ϰ� �ߺ��Ǵ°� ������ �� �÷��̾�� ��ȯ, �ߺ����� ���� ���� ū ���� ã�� ������ �ݺ�
        bool tf = true;

        if (tf & casinoPlayerList.Count>=2)
        {
            //���� ū �ֻ��� �ߺ� ���� �Ǻ� �Ǵ��� ���� 

            //ù���� �� ���� ū ���� �ߺ��Ǵ��� countDup�� �Ǻ�
            //https://www.techiedelight.com/ko/get-count-of-number-of-items-in-a-list-in-csharp/ ����
            int countDup = casinoPlayerList.Where(x => x.Equals(casinoPlayerList[0])).Count();
                if (countDup > 1)
                {
                    CheckAReturnDice(casinoPlayerList[0]);

                //��ȯ�ϹǷ� ī������ �ߺ��̵Ǵ� playerdc������ 0���� �ʱ�ȭ
                if (casinoPlayerList[0] == player1dc) player1dc = 0;
                if (casinoPlayerList[0] == player2dc) player2dc = 0;
                if (casinoPlayerList[0] == player3dc) player3dc = 0;
                if (casinoPlayerList[0] == player4dc) player4dc = 0;
                if (casinoPlayerList[0] == player5dc) player5dc = 0;

                for (int i=0; i<countDup; i++)
                    {
                        //�ߺ��Ǵ� ������ŭ playerlist ����
                        casinoPlayerList.RemoveAt(0);
                    }
                }
                else
                {
                //while�� ������
                tf = false;
                }
        }

    }

    public void ReturnDice( string playerNum)
    {
        //ī���뿡�� �÷��̾� ��ȣ�� �´� �ֻ����� �� �÷��̾�� ��ȯ
        GameObject temp; 
        //�ֻ����� �˻����� ���������� ��� �ѱ��.
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
