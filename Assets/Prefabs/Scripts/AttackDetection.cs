using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackDetection : MonoBehaviour
{
    [Header("Scripts")]
    public StateMachine stateMachine;

    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            stateMachine.enemyState = StateMachine.EnemyState.attacking;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        stateMachine.enemyState = StateMachine.EnemyState.chaseing;
    }
}
