using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour
{
    RaycastHit hit;
    float distance = 100f;
    LayerMask mask;
    private void Start()
    {
        mask = LayerMask.GetMask("Dice");
    }

    private void Update()
    {
        if (Physics.Raycast(transform.position, Vector3.up, out hit, distance, mask))
        {
            Debug.Log(hit.collider.name);
        }
        else
            Debug.Log("empty");
    }
}
