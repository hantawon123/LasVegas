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


    //player가 현재 소지하고 있는 주사위의 개수
    int diceCount = 0;

    // Singleton
    public static CasinoPlayer instance;

    private void Awake()
    {
        instance = this;

        //start에서 실행하면 안된다.

        playerNum = (int)Char.GetNumericValue(this.name[this.name.Length - 1]);

        //플레이어별로 8개의 주사위 생성
        for (int i = 0; i < 8; i++)
        {
            //주사위 오브젝트를 생성하고, 플레이어 하위오브젝트로 둔다.
            GameObject temp = Instantiate(diceObject, transform.position, transform.rotation);
            temp.transform.parent = this.transform;
            temp.transform.name = "Dice" + playerNum;
            //플레이어 명을 태그로 둔다 카지노 보드에서 어느 플레이어의 주사위인지 판별하기 위한 중요한 요소
            temp.tag = "Dice";

        }
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.C))
        {
            //SD플레이어 오브젝트는 제외o
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

    //버튼을 통해 호출해서 플레이어 별로 주사위를 굴리게 선택할 수 있다.
    public void OnClickRoll()
    {
        int nowPlayer = GameManager.instance.turn;

        //현재 플레이어 자식으로 존재하는 주사위의 개수
        diceCount = this.transform.childCount;

        //SDPlayer가 있으므로i=0부터 탐색
        for (int i = 1; i < diceCount; i++)
        {
            GameObject temp = transform.GetChild(i).gameObject;
            if (nowPlayer == int.Parse(this.name))
            {
                //플에이어 오브젝트의 자식 주사위를 한번씩 굴린다.
                temp.GetComponent<Dice>().roll();
            }
            else
            {
                // 주사위 숨기기
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

    //버튼을 통해 플레이어가 특정 카지노(targetNum)로 배팅할 수 있게한다.
    public void OnClickBet(int targetNum)
    {
        bool tf = false;
        int Countdice = transform.childCount;

        //애니메이션 배팅 모션 재생
        this.GetComponent<SDAnimation>().PlayBetting();

        GameObject targetCasino = GameObject.Find("Casino" + targetNum);

        //SDplayer는 보내면 안되므로 Countdice-1까지 실행
        for (int i = 0; i < Countdice - 1; i++)
        {

            if (tf == false)
            {
                //맨 마지막 인덱스부터 주사위 수를 읽는다.
                int temp = transform.GetChild(Countdice - i - 1).gameObject.GetComponent<Dice>().number;
                //카지노 번호와 주사위 눈 이 같으면 그 카지노로 주사위를 넘긴다.
                if (temp == targetNum)
                {
                    //보내고자하는 주사위 오브젝트를 목표 카지노이 자식으로 넘긴다.
                    GameObject tempGO = transform.GetChild(Countdice - i - 1).gameObject;
                    tempGO.transform.parent = targetCasino.transform;
                    //tempGO.transform.position = targetCasino.transform.position;

                    // 각 카지노에 몇번 플레이어가 몇개를 배팅했는지 카지노의 playerdc변수를 증가한다.
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
        //플레이어 자식의 주사위 개수 반환
        diceCount = this.transform.childCount - 1;
        return diceCount;
    }

    public List<int> GetDiceList()
    {

        //초기화
        PlayerDiceList.Clear();

        Number1dc = 0;
        Number2dc = 0;
        Number3dc = 0;
        Number4dc = 0;
        Number5dc = 0;
        Number6dc = 0;

        //SDPlayer는 제외
        int Countdice = transform.childCount - 1;

        //SDplayer는 보내면 안되므로 Countdice-1까지 실행
        for (int i = 0; i < Countdice; i++)
        {
            //맨 마지막 인덱스부터 주사위 수를 읽는다.
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

        //0번 인자부터 차례대로 주사위 1의 개수, 2의 개수, ....... 6의 개수
        PlayerDiceList.Add(Number1dc);
        PlayerDiceList.Add(Number2dc);
        PlayerDiceList.Add(Number3dc);
        PlayerDiceList.Add(Number4dc);
        PlayerDiceList.Add(Number5dc);
        PlayerDiceList.Add(Number6dc);

        return PlayerDiceList;
    }





}
