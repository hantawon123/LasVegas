using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CasinoManager : MonoBehaviour
{

    [SerializeField] private GameObject casino1;
    [SerializeField] private GameObject casino2;
    [SerializeField] private GameObject casino3;
    [SerializeField] private GameObject casino4;
    [SerializeField] private GameObject casino5;
    [SerializeField] private GameObject casino6;

    public List<GameObject> casinoList = new List<GameObject>();

    public static CasinoManager instance;
    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        casinoList.Add(casino1);
        casinoList.Add(casino2);
        casinoList.Add(casino3);
        casinoList.Add(casino4);
        casinoList.Add(casino5);
        casinoList.Add(casino6);
    }

    // Update is called once per frame
    void Update()
    {
        
    }


}
