using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Camera_Manager : MonoBehaviour
{

    public Camera MainCamera;

    public List<Vector3>  CameraPosition ;
    public int CameraPos;


    public static Camera_Manager instance;
    private void Awake()
    {
        instance = this;
        CameraPosition.Add(new Vector3(24.7f, 35, -17.9f));
        CameraPosition.Add(new Vector3(-28.4f, 35, -17.9f));
        CameraPosition.Add(new Vector3(45, 35, -50.1f));
        CameraPosition.Add(new Vector3(0, 35, -51.7f));
        CameraPosition.Add(new Vector3(-43.4f, 35, -51.7f));
        CameraPosition.Add(new Vector3(24.7f, 35, -17.9f));
    }
    void Start()
    {
        MainView();
    }
    // Call this function to disable FPS camera,
    // and enable overhead camera.
    public void DiceView( int targetNum)
    {
        
        Vector3 target = CameraPosition[targetNum];
        transform.position = target;
    }

    // Call this function to enable FPS camera,
    // and disable overhead camera.
    public void MainView()
    {
        Vector3 target = new Vector3(0, 120, 0);
        transform.position = target;
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F1))
            MainView();
        else if (Input.GetKeyDown(KeyCode.F2))
            DiceView(CameraPos - 1 % 5);
    }
    

    public void switchCamera()
    {
        DiceView(CameraPos);
        CameraPos = (CameraPos + 1) % 5;
    }



}
