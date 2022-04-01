using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Detection : MonoBehaviour
{
    [Header("PlayerObjects")]
    public GameObject player;

    [Header("DetectionObject")]
    public SphereCollider detectionCollider;

    [Header("Scripts")]
    public StateMachine stateMachine;

    // Update is called once per frame
    void Start()
    {
        detectionCollider = GetComponent<SphereCollider>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            stateMachine.enemyState = StateMachine.EnemyState.chaseing;
            detectionCollider.radius += 5f;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            stateMachine.enemyState = StateMachine.EnemyState.retreating;
            detectionCollider.radius -= 5f;
        }
    }
}
