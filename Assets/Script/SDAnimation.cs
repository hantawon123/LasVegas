using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SDAnimation : MonoBehaviour
{
    // Start is called before the first frame update


    public Animator SDanimation;

    public static SDAnimation instance;

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
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
}
