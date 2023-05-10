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
    private void MakeDeck(List<int> list)//�Ӵϵ� ������ִ� �Լ�
    {
        int minMoney = 10000;
        for (int i = 0; i < 9; i++)//9������ ����
        {
            for (int j = 0; j < 6; j++)//6����
            {
                list.Add(minMoney);
            }
            minMoney += 10000;
        }
    }
    private List<T> ShuffleList<T>(List<T> list)//����Ʈ ���� �Լ�
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

    //ī���� �ϳ� �޾Ƽ� ó���ϴ� �ڵ�� �ۼ��߽��ϴ�.
    //casino �ȿ� 6�� ī���밡 �ִ� ������� �����ϽŴٰ� �ϸ� �� ī���� ���� ����� ���� ������ �����Դϴ�.


    public void SendtoBoard()//������ ī���뺸��� ���� �Ű��ִ� �Լ�
    {
        audiosource.Play();
        int moneySum = 0;
        for (int i = 0; i < CasinoManager.instance.casinoList.Count; i++)//6���� ī���� ���� ���鼭 50000�� ���������� �ݺ����� �����ش�.
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