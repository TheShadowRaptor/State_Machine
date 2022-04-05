using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine : MonoBehaviour
{
    public enum EnemyState
    {
        patrolling,
        chasing,
        searching,
        attacking,
        retreating
    }

    public EnemyState enemyState;


    [HideInInspector]
    public int currentState;
    public bool patrolling;
    public bool chasing;
    public bool searching;
    public bool attacking;
    public bool retreating;

    // Start is called before the first frame update
    void Start()
    {
        enemyState = EnemyState.patrolling;
    }

    // Update is called once per frame
    void Update()
    {
        checkState();
    }

    private void checkState()
    {
        if (patrolling)
        {
            enemyState = EnemyState.patrolling;
        }

        if (chasing)
        {
            enemyState = EnemyState.chasing;
        }

        if (searching)
        {
            enemyState = EnemyState.searching;
        }

        if (attacking)
        {
            enemyState = EnemyState.attacking;
        }

        if (retreating)
        {
            enemyState = EnemyState.retreating;
        }

        patrolling = false;
        chasing = false;
        searching = false;
        attacking = false;
        retreating = false;

    }
}
