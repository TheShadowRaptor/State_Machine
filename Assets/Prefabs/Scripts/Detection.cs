using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Detection : MonoBehaviour
{
    [Header("PlayerObjects")]
    public GameObject player;

    [Header("DetectionObject")]
    public GameObject detectionObj;
    public float detectionRange;
    public float detectionGrowth;

    [Header("Scripts")]
    public StateMachine stateMachine;

    // Update is called once per frame
    void Start()
    {
        detectionObj.transform.localScale = new Vector3(detectionRange, detectionRange, detectionRange);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            stateMachine.enemyState = StateMachine.EnemyState.chaseing;
            detectionObj.transform.localScale = new Vector3(detectionRange + detectionGrowth, detectionRange + detectionGrowth, detectionRange + detectionGrowth);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            stateMachine.enemyState = StateMachine.EnemyState.searching;
            detectionObj.transform.localScale = new Vector3(detectionRange, detectionRange, detectionRange);
        }
    }
}
