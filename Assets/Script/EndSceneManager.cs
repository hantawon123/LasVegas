using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EndSceneManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI WinnerText;
    [SerializeField] private TextMeshProUGUI player1MoneyText;
    [SerializeField] private TextMeshProUGUI player2MoneyText;
    [SerializeField] private TextMeshProUGUI player3MoneyText;
    [SerializeField] private TextMeshProUGUI player4MoneyText;
    [SerializeField] private TextMeshProUGUI player5MoneyText;



    // Start is called before the first frame update
    void Start()
    {
        PlayerManager pm = PlayerManager.instance;
        GameManager gm = GameManager.instance;
        //CasinoPlayer.instance.PlayVictory();
        WinnerText.text = "WINNER IS PLAYER#" + gm.WinPlayerNum.ToString();
        WinnerText.color = Color.yellow;
        player1MoneyText.text = "PLAYER1 " + pm.player1.sum.ToString() + "$";
        player2MoneyText.text = "PLAYER2 " + pm.player2.sum.ToString() + "$";
        player3MoneyText.text = "PLAYER3 " + pm.player3.sum.ToString() + "$";
        player4MoneyText.text = "PLAYER4 " + pm.player4.sum.ToString() + "$";
        player5MoneyText.text = "PLAYER5 " + pm.player5.sum.ToString() + "$";

        //局聪皋捞记 包访 贸府
        string WinnerTxt = "SDPlayer" + (gm.WinPlayerNum).ToString();

        if (WinnerTxt != "SDPlayer1")
            GameObject.Find("SDPlayer1").GetComponent<SDAnimation>().PlayDefeat();
        if(WinnerTxt != "SDPlayer2")
            GameObject.Find("SDPlayer2").GetComponent<SDAnimation>().PlayDefeat();
        if (WinnerTxt != "SDPlayer3")
            GameObject.Find("SDPlayer3").GetComponent<SDAnimation>().PlayDefeat();
        if (WinnerTxt != "SDPlayer4")
            GameObject.Find("SDPlayer4").GetComponent<SDAnimation>().PlayDefeat();
        if (WinnerTxt != "SDPlayer5")
            GameObject.Find("SDPlayer5").GetComponent<SDAnimation>().PlayDefeat();

        GameObject.Find(WinnerTxt).GetComponent<SDAnimation>().PlayVictory();




    }
}