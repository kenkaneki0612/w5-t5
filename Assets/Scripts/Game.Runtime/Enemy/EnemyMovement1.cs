using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement1 : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float range;
    [SerializeField] private float checkDelay;
    [SerializeField] private LayerMask playerLayer; 

    private bool move;
    private Vector3 destination; 
    private Vector3[] directions = new Vector3[4];


    private void Update()
    {
        if (move) 
            transform.Translate(destination * Time.deltaTime * speed);
    }
}
