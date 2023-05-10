using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoneyManager : MonoBehaviour
{
    private List<int> moneyDeck = new List<int>();
    private List<List<int>> moneyList = new List<List<int>>();

    AudioSource audiosource;

    public static MoneyManager instance;
    private void Awake()
    {
        instance = this;

    }
    // Start is called before the first frame update
    void Start()
    {
        audiosource = gameObject.AddComponent<AudioSource>();
        audiosource.loop = false;
        audiosource.playOnAwake = false;
        audiosource.mute = false;

        MakeDeck(moneyDeck);
        ShuffleList(moneyDeck);


    }

    // Update is called once per frame
    void Update()
    {
        if(GameManager.instance.isRoundStart)
        {
            SetBoardClean();
            SendtoBoard();
        }
        

    }
    private void MakeDeck(List<int> list)//머니덱 만들어주는 함수
    {
        int minMoney = 10000;
        for (int i = 0; i < 9; i++)//9종류의 지폐
        {
            for (int j = 0; j < 6; j++)//6개씩
            {
                list.Add(minMoney);
            }
            minMoney += 10000;
        }
    }
    private List<T> ShuffleList<T>(List<T> list)//리스트 셔플 함수
    {
        int random1, random2;
        T temp;

        for (int i = 0; i < list.Count; ++i)
        {
            random1 = Random.Range(0, list.Count);
            random2 = Random.Range(0, list.Count);

            temp = list[random1];
            list[random1] = list[random2];
            list[random2] = temp;

        }

        return list;
    }

    //카지노 하나 받아서 처리하는 코드로 작성했습니다.
    //casino 안에 6개 카지노가 있는 방식으로 구현하신다고 하면 각 카지노 접근 방법에 따라 수정할 예정입니다.


    public void SendtoBoard()//덱에서 카지노보드로 돈을 옮겨주는 함수
    {
        audiosource.Play();
        int moneySum = 0;
        for (int i = 0; i < CasinoManager.instance.casinoList.Count; i++)//6개의 카지노 보드 돌면서 50000이 넘을때까지 반복문을 돌려준다.
        {
            while (moneySum < 50000)
            {
                CasinoManager.instance.casinoList[i].GetComponent<Casino>().casinoMoneyList.Add(moneyDeck[0]);
                moneySum += this.moneyDeck[0];
                this.moneyDeck.RemoveAt(0);
            }
            moneySum = 0;
        }
        GameManager.instance.isRoundStart = false;

    }
    public void SetBoardClean()
    {
        for (int i = 0; i < CasinoManager.instance.casinoList.Count; i++)
        {
            CasinoManager.instance.casinoList[i].GetComponent<Casino>().casinoMoneyList.Clear();
        }
    }
}