using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{

    private Player player;
    //���� ���̽� �ϼ��Ǹ� ���̽� ����Ʈ �߰�
    [SerializeField] public GameObject Player1;//�÷��̾� ���ӿ�����Ʈ ����Ʈ
    [SerializeField] public GameObject Player2;
    [SerializeField] public GameObject Player3;
    [SerializeField] public GameObject Player4;
    [SerializeField] public GameObject Player5;


    public Player player1;
    public Player player2;
    public Player player3;
    public Player player4;
    public Player player5;

    int inumsofBot;//Player��ũ��Ʈ�� bot ���� ����� ����

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
        //�÷��̾� ������Ʈ 1~5�� ����Ʈ�� ����
        PlayerList.Add(Player1);
        PlayerList.Add(Player2);
        PlayerList.Add(Player3);
        PlayerList.Add(Player4);
        PlayerList.Add(Player5);
        SetBot();

        //�÷��̾� �Ӵϸ���Ʈ1~5�� �������ִ� ����Ʈ

        //�÷��̾��ΰ� ���ΰ� Ȯ��

    }


    public List<int> PMGetDiceList(int playerIndex)
    {
        var tempPlayerList = PlayerList[playerIndex - 1].GetComponent<CasinoPlayer>().GetDiceList();
        //PlayerManager�� tempPlayerList���

        return tempPlayerList;
    }

    public void SumMoney()//�� �÷��̾� �Ӵϸ���Ʈ ��
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

