using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SDCameraManager : MonoBehaviour
{
    public static SDCameraManager instance;
    // Start is called before the first frame update

    public Camera SDCamera;

    public GameObject sdplayer1;
    public GameObject sdplayer2;
    public GameObject sdplayer3;
    public GameObject sdplayer4;
    public GameObject sdplayer5;


    int SDCamPos;

    public List<Vector3> SDCameraList;
    public List<GameObject> SDPlayers;

    private void Awake()
    {
        instance = this;

        SDCameraList.Add(GameObject.Find("SDPlayer1").transform.position + new Vector3(0, 7f, -8f));
        SDCameraList.Add(GameObject.Find("SDPlayer2").transform.position + new Vector3(0, 7f, -8f));
        SDCameraList.Add(GameObject.Find("SDPlayer3").transform.position + new Vector3(0, 7f, -8f));
        SDCameraList.Add(GameObject.Find("SDPlayer4").transform.position + new Vector3(0, 7f, -8f));
        SDCameraList.Add(GameObject.Find("SDPlayer5").transform.position + new Vector3(0, 7f, -8f));

        SDPlayers.Add(sdplayer1);
        SDPlayers.Add(sdplayer2);
        SDPlayers.Add(sdplayer3);
        SDPlayers.Add(sdplayer4);
        SDPlayers.Add(sdplayer5);


    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //인자는 0부터 4까지
    public void SetSDCamPos()
    {
        SDCamera.transform.position = SDCameraList[SDCamPos];
       
        SDCamPos = (SDCamPos + 1) % 5;
        
    }
}
