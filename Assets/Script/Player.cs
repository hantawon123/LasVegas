using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player instance;
    private void Awake()
    {
        instance = this;
    }
    public List<int> moneyList = new List<int>();//�÷��̾ ������ �ִ� ���� ����Ʈ
    public int sum = 0;//�÷��̾ ������ �ִ� ���� ��
    public int numsofDice = 8;//�÷��̾ �����ϰ� �ִ� ���̽� ����
    public bool isBot = false;//�ش� �÷��̾ ������ �÷��̾����� Ȯ��

}
