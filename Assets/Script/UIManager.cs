using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class UIManager : MonoBehaviour
{
    [SerializeField] private Text numsofBot;
    [SerializeField] private Text player1MT;
    [SerializeField] private Text player2MT;
    [SerializeField] private Text player3MT;
    [SerializeField] private Text player4MT;
    [SerializeField] private Text player5MT;

    [SerializeField] private Text player1Name;
    [SerializeField] private Text player2Name;
    [SerializeField] private Text player3Name;
    [SerializeField] private Text player4Name;
    [SerializeField] private Text player5Name;

    [SerializeField] private Transform Casino1parent;
    [SerializeField] private Transform Casino2parent;
    [SerializeField] private Transform Casino3parent;
    [SerializeField] private Transform Casino4parent;
    [SerializeField] private Transform Casino5parent;
    [SerializeField] private Transform Casino6parent;
    
    [SerializeField] private Text RoundText;
    [SerializeField] private Text TurnText;
    // Start is called before the first frame update
    private void Start()
    {
       

    }

    // Update is called once per frame
    void Update()
    {
        //플레이어 보유 금액 UI에 표시
        player1MT.text = PlayerManager.instance.player1.sum.ToString();
        player2MT.text = PlayerManager.instance.player2.sum.ToString();
        player3MT.text = PlayerManager.instance.player3.sum.ToString();
        player4MT.text = PlayerManager.instance.player4.sum.ToString();
        player5MT.text = PlayerManager.instance.player5.sum.ToString();

        if(GameManager.instance.isReset)
        {
            
            for(int i = 0; i < 5; i++)
            {
                Casino1parent.GetChild(i).GetComponent<Text>().text = "";
            }
            for (int i = 0; i < 5; i++)
            {
                Casino2parent.GetChild(i).GetComponent<Text>().text = "";
            }
            for (int i = 0; i < 5; i++)
            {
                Casino3parent.GetChild(i).GetComponent<Text>().text = "";
            }
            for (int i = 0; i < 5; i++)
            {
                Casino4parent.GetChild(i).GetComponent<Text>().text = "";
            }
            for (int i = 0; i < 5; i++)
            {
                Casino5parent.GetChild(i).GetComponent<Text>().text = "";
            }
            for (int i = 0; i < 5; i++)
            {
                Casino6parent.GetChild(i).GetComponent<Text>().text = "";
            }
            GameManager.instance.isReset = false;
        }

        if (PlayerManager.instance.player1.isBot)
            player1Name.text = "BOT";
        else
            player1Name.text = "Player"+PlayerManager.instance.player1.name;
        if (PlayerManager.instance.player2.isBot)
            player2Name.text = "BOT";
        else
            player2Name.text = "Player" + PlayerManager.instance.player2.name;
        if (PlayerManager.instance.player3.isBot)
            player3Name.text = "BOT";
        else
            player3Name.text = "Player" + PlayerManager.instance.player3.name;
        if (PlayerManager.instance.player4.isBot)
            player4Name.text = "BOT";
        else
            player4Name.text = "Player" + PlayerManager.instance.player4.name;
        if (PlayerManager.instance.player5.isBot)
            player5Name.text = "BOT";
        else
            player5Name.text = "Player" + PlayerManager.instance.player5.name;


        RoundText.text = "Round " + (GameManager.instance.round+1).ToString();

        for (int j = 0; j < CasinoManager.instance.casinoList[0].GetComponent<Casino>().casinoMoneyList.Count; j++)
        {
            Casino1parent.GetChild(j).GetComponent<Text>().text = CasinoManager.instance.casinoList[0].GetComponent<Casino>().casinoMoneyList[j].ToString();
        }
        for (int j = 0; j < CasinoManager.instance.casinoList[1].GetComponent<Casino>().casinoMoneyList.Count; j++)
        {
            Casino2parent.GetChild(j).GetComponent<Text>().text = CasinoManager.instance.casinoList[1].GetComponent<Casino>().casinoMoneyList[j].ToString();
        }
        for (int j = 0; j < CasinoManager.instance.casinoList[2].GetComponent<Casino>().casinoMoneyList.Count; j++)
        {
            Casino3parent.GetChild(j).GetComponent<Text>().text = CasinoManager.instance.casinoList[2].GetComponent<Casino>().casinoMoneyList[j].ToString();
        }
        for (int j = 0; j < CasinoManager.instance.casinoList[3].GetComponent<Casino>().casinoMoneyList.Count; j++)
        {
            Casino4parent.GetChild(j).GetComponent<Text>().text = CasinoManager.instance.casinoList[3].GetComponent<Casino>().casinoMoneyList[j].ToString();
        }
        for (int j = 0; j < CasinoManager.instance.casinoList[4].GetComponent<Casino>().casinoMoneyList.Count; j++)
        {
            Casino5parent.GetChild(j).GetComponent<Text>().text = CasinoManager.instance.casinoList[4].GetComponent<Casino>().casinoMoneyList[j].ToString();
        }
        for (int j = 0; j < CasinoManager.instance.casinoList[5].GetComponent<Casino>().casinoMoneyList.Count; j++)
        {
            Casino6parent.GetChild(j).GetComponent<Text>().text = CasinoManager.instance.casinoList[5].GetComponent<Casino>().casinoMoneyList[j].ToString();
        }


    }
}
