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
    public List<int> moneyList = new List<int>();//플레이어가 가지고 있는 돈의 리스트
    public int sum = 0;//플레이어가 가지고 있는 돈의 합
    public int numsofDice = 8;//플레이어가 보유하고 있는 다이스 갯수
    public bool isBot = false;//해당 플레이어가 봇인지 플레이어인지 확인

}
