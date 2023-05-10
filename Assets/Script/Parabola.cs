using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Parabola : MonoBehaviour
{
    public static Parabola instance;
    bool stop = false;
    void Awake()
    {
        instance = this;
    }
    private Vector3 startPos, endPos;
    public float timer;
    private void OnTriggerEnter(Collider col)
    {
        if (col.name == "Dice_Pos")
        {
            transform.GetComponent<BoxCollider>().isTrigger = false;
            transform.GetComponent<Rigidbody>().useGravity = true;
            //col.isTrigger = false;
            stop = true;
        }
    }
    protected static Vector3 parabola(Vector3 start, Vector3 end, float height, float t)
    {
        Func<float, float> f = x => -4 * height * x * x + 4 * height * x;

        var mid = Vector3.Lerp(start, end, t);

        return new Vector3(mid.x, f(t) + Mathf.Lerp(start.y, end.y, t), mid.z);
    }
    public IEnumerator MoveToCasino()
    {
        transform.GetComponent<BoxCollider>().isTrigger = true;
        transform.GetComponent<Rigidbody>().useGravity = false;
        switch(transform.GetComponent<Dice>().number)
        {
            case 1:
                transform.rotation = Quaternion.Euler(new Vector3(-90, 0, 0));
                break;
            case 2:
                transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
                break;
            case 3:
                transform.rotation = Quaternion.Euler(new Vector3(0, 0, -90));
                break;
            case 4:
                transform.rotation = Quaternion.Euler(new Vector3(0, 0, 90));
                break;
            case 5:
                transform.rotation = Quaternion.Euler(new Vector3(0, 0, 180));
                break;
            case 6:
                transform.rotation = Quaternion.Euler(new Vector3(90, 0, 0));
                break;

        }
        
        startPos = transform.position;
        endPos = Dice_Manager.instance.endPos + new Vector3(0, 3, 0);
        timer = 0;

        while (!stop)
        {
            timer += Time.deltaTime * 0.75f;

            Vector3 tempPos = parabola(startPos, endPos, 5, timer);
            transform.position = tempPos;
            yield return new WaitForEndOfFrame();
        }
    }
    public IEnumerator MoveToPlayer()
    {
        transform.GetComponent<BoxCollider>().isTrigger = false;
        transform.GetComponent<Rigidbody>().useGravity = true;
        transform.localScale = Dice_Manager.instance.dice_size;
        transform.position = transform.parent.transform.position;
        stop = false;
        //col.isTrigger = true;
        yield return new WaitForEndOfFrame();
    }
}