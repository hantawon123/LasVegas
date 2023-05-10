using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CasinoPlayer : MonoBehaviour
{

    public GameObject playerObject;
    public GameObject diceObject;

    public List<int> PlayerMoneyList = new List<int>();

    int playerNum;

    public Animator SDanimation;

    public List<int> PlayerDiceList = new List<int>();
    public int Number1dc;
    public int Number2dc;
    public int Number3dc;
    public int Number4dc;
    public int Number5dc;
    public int Number6dc;

    public bool isBetDone = true;


    //player�� ���� �����ϰ� �ִ� �ֻ����� ����
    int diceCount = 0;

    // Singleton
    public static CasinoPlayer instance;

    private void Awake()
    {
        instance = this;

        //start���� �����ϸ� �ȵȴ�.

        playerNum = (int)Char.GetNumericValue(this.name[this.name.Length - 1]);

        //�÷��̾�� 8���� �ֻ��� ����
        for (int i = 0; i < 8; i++)
        {
            //�ֻ��� ������Ʈ�� �����ϰ�, �÷��̾� ����������Ʈ�� �д�.
            GameObject temp = Instantiate(diceObject, transform.position, transform.rotation);
            temp.transform.parent = this.transform;
            temp.transform.name = "Dice" + playerNum;
            //�÷��̾� ���� �±׷� �д� ī���� ���忡�� ��� �÷��̾��� �ֻ������� �Ǻ��ϱ� ���� �߿��� ���
            temp.tag = "Dice";

        }
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.C))
        {
            //SD�÷��̾� ������Ʈ�� ����o
            diceCount = this.transform.childCount - 1;
        }

        if (Input.GetKeyUp(KeyCode.M))
        {

            for (int i = 0; i < PlayerMoneyList.Count; i++)
            {
                Debug.Log(PlayerMoneyList[i]);
            }
        }

        if (Input.GetKeyUp(KeyCode.P))
        {
            PlayDefeat();
        }

    }
    void runBot()
    {
        GameObject ML_Player = GameObject.Find("MLPlayer");
        int select = ML_Player.GetComponent<MLPlayer>().ExecuteML(int.Parse(this.name));
        Debug.Log("ML select: " + this.name + " " + select);  // TODO: Temp code
        Dice_Manager.instance.locate_dice(int.Parse(this.name) - 1, select);
    }

    //��ư�� ���� ȣ���ؼ� �÷��̾� ���� �ֻ����� ������ ������ �� �ִ�.
    public void OnClickRoll()
    {
        int nowPlayer = GameManager.instance.turn;

        //���� �÷��̾� �ڽ����� �����ϴ� �ֻ����� ����
        diceCount = this.transform.childCount;

        //SDPlayer�� �����Ƿ�i=0���� Ž��
        for (int i = 1; i < diceCount; i++)
        {
            GameObject temp = transform.GetChild(i).gameObject;
            if (nowPlayer == int.Parse(this.name))
            {
                //�ÿ��̾� ������Ʈ�� �ڽ� �ֻ����� �ѹ��� ������.
                temp.GetComponent<Dice>().roll();
            }
            else
            {
                // �ֻ��� �����
                //temp.GetComponent<Dice>().hide();
            }

        }

        if (nowPlayer != int.Parse(this.name)) return;
        if (playerObject.GetComponent<Player>().isBot)
        {
            if (GameManager.instance.isBetDone) return;
            GameManager.instance.isBetDone = true;
            if (playerObject.GetComponent<Player>().numsofDice <= 0) return;
            Invoke("runBot", 3.0f);
        }
    }

    //��ư�� ���� �÷��̾ Ư�� ī����(targetNum)�� ������ �� �ְ��Ѵ�.
    public void OnClickBet(int targetNum)
    {
        bool tf = false;
        int Countdice = transform.childCount;

        //�ִϸ��̼� ���� ��� ���
        this.GetComponent<SDAnimation>().PlayBetting();

        GameObject targetCasino = GameObject.Find("Casino" + targetNum);

        //SDplayer�� ������ �ȵǹǷ� Countdice-1���� ����
        for (int i = 0; i < Countdice - 1; i++)
        {

            if (tf == false)
            {
                //�� ������ �ε������� �ֻ��� ���� �д´�.
                int temp = transform.GetChild(Countdice - i - 1).gameObject.GetComponent<Dice>().number;
                //ī���� ��ȣ�� �ֻ��� �� �� ������ �� ī����� �ֻ����� �ѱ��.
                if (temp == targetNum)
                {
                    //���������ϴ� �ֻ��� ������Ʈ�� ��ǥ ī������ �ڽ����� �ѱ��.
                    GameObject tempGO = transform.GetChild(Countdice - i - 1).gameObject;
                    tempGO.transform.parent = targetCasino.transform;
                    //tempGO.transform.position = targetCasino.transform.position;

                    // �� ī���뿡 ��� �÷��̾ ��� �����ߴ��� ī������ playerdc������ �����Ѵ�.
                    switch (playerNum)
                    {
                        case 1:
                            targetCasino.GetComponent<Casino>().player1dc += 1;
                            tf = true;
                            break;
                        case 2:
                            targetCasino.GetComponent<Casino>().player2dc += 1;
                            tf = true;
                            break;
                        case 3:
                            targetCasino.GetComponent<Casino>().player3dc += 1;
                            tf = true;
                            break;
                        case 4:
                            targetCasino.GetComponent<Casino>().player4dc += 1;
                            tf = true;
                            break;
                        case 5:
                            targetCasino.GetComponent<Casino>().player5dc += 1;
                            tf = true;
                            break;

                    }

                }
            }   
        }
    }

    public void PlayVictory()
    {
        SDanimation.SetTrigger("Win");
    }

    public void PlayBetting()
    {
        SDanimation.SetTrigger("Bet");
    }

    public void PlayDefeat()
    {
        SDanimation.SetTrigger("Defeat");
    }

    public int GetDiceNum()
    {
        //�÷��̾� �ڽ��� �ֻ��� ���� ��ȯ
        diceCount = this.transform.childCount - 1;
        return diceCount;
    }

    public List<int> GetDiceList()
    {

        //�ʱ�ȭ
        PlayerDiceList.Clear();

        Number1dc = 0;
        Number2dc = 0;
        Number3dc = 0;
        Number4dc = 0;
        Number5dc = 0;
        Number6dc = 0;

        //SDPlayer�� ����
        int Countdice = transform.childCount - 1;

        //SDplayer�� ������ �ȵǹǷ� Countdice-1���� ����
        for (int i = 0; i < Countdice; i++)
        {
            //�� ������ �ε������� �ֻ��� ���� �д´�.
            int temp = transform.GetChild(Countdice - i).gameObject.GetComponent<Dice>().number;

            switch (temp)
            {
                case 1:
                    Number1dc += 1;
                    break;
                case 2:
                    Number2dc += 1;
                    break;
                case 3:
                    Number3dc += 1;
                    break;
                case 4:
                    Number4dc += 1;
                    break;
                case 5:
                    Number5dc += 1;
                    break;
                case 6:
                    Number6dc += 1;
                    break;
                default:
                    Debug.Log("Inavalid Value: " + temp);
                    break;
            }
        }

        //0�� ���ں��� ���ʴ�� �ֻ��� 1�� ����, 2�� ����, ....... 6�� ����
        PlayerDiceList.Add(Number1dc);
        PlayerDiceList.Add(Number2dc);
        PlayerDiceList.Add(Number3dc);
        PlayerDiceList.Add(Number4dc);
        PlayerDiceList.Add(Number5dc);
        PlayerDiceList.Add(Number6dc);

        return PlayerDiceList;
    }





}
