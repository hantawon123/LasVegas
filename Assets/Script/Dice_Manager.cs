using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Dice_Manager : MonoBehaviour
{
    public GameObject[] Players;
    public GameObject[] Dice;
    public GameObject CasinoList;
    public int[,] dice_idx = new int[7, 7];
    public Vector3 pos_size;
    public Vector3 dice_size;
    public Vector3 startPos, endPos;
    public float timer;
    protected float timeToFloor;
    public static Dice_Manager instance;
    void Awake()
    {
        instance = this;
    }

    void init()
    {
        dice_idx = new int[7, 7];
        GameObject PlayerList = GameObject.Find("PlayerList");
        Players = new GameObject[PlayerList.transform.childCount];
        for(int i=0; i<PlayerList.transform.childCount; i++)
        {
            Players[i] = PlayerList.transform.GetChild(i).gameObject;
        }
        CasinoList = GameObject.Find("CasinoBoardList");
        Dice = GameObject.FindGameObjectsWithTag("Dice");
    }
    public void locate_dice(int Player_Num, int num)
    {
        int Dice_Count = Players[Player_Num].transform.childCount - 1;
        //SD캐릭터 는 제외
        for (int j = 0; j < Dice_Count; j++)
        {
            GameObject dice = Players[Player_Num].transform.GetChild(Dice_Count - j).gameObject;
            //Dice_Num은 주사위의 눈
            int Dice_Num = dice.transform.GetComponent<Dice>().number;
            if (num == Dice_Num)
            {
                //주사위눈에 해당하는 카지노
                if (Dice_Num != 0)
                {
                    GameObject Casino_num = CasinoList.transform.GetChild(Dice_Num - 1).gameObject.transform.GetChild(2).gameObject;
                    pos_size = Casino_num.transform.GetChild(0).localScale;
                    dice.transform.localScale = pos_size - new Vector3(0.3f, 0.3f, 0.3f);
                    startPos = dice.transform.position;
                    endPos = Casino_num.transform.GetChild(Player_Num * 8 + dice_idx[Player_Num, Dice_Num]++).transform.position;
                    dice.GetComponent<Parabola>().StartCoroutine("MoveToCasino");
                    Players[Player_Num].GetComponent<CasinoPlayer>().OnClickBet(Casino_num.GetComponent<Casino>().casinoNum);
                }
            }

        }
    }
    int count_dice()
    {
        int count = 0;
        foreach(GameObject player in Players)
        {
            count += player.transform.childCount;
        }
        return count;
    }
    void Start()
    {
        init();
        dice_size = GameObject.FindGameObjectWithTag("Dice").transform.localScale;
    }
    void Update()
    {
        timer += Time.deltaTime * 1.5f;
        int num = 0;
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                GameObject temp = hit.transform.gameObject;
                if (temp.tag == "Dice")
                {
                    int Player_Num = int.Parse(temp.transform.parent.name) - 1;
                    if (Players[Player_Num].GetComponent<Player>().isBot) return;
                    //if (GameManager.instance.isBetDone) return;

                    num = temp.transform.GetComponent<Dice>().number;
                    locate_dice(Player_Num, num);
                    GameManager.instance.isBetDone = true;
                }
            }
            //click_count++;
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            foreach (GameObject dice in Dice)
            {
                dice.GetComponent<MeshRenderer>().enabled = false;
                dice.GetComponent<Parabola>().StartCoroutine("MoveToPlayer");
            }
            init();
        }      
    }
}
