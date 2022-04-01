using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine : MonoBehaviour
{
    public enum EnemyState
    {
        patroling,
        chaseing,
        searching,
        attacking,
        retreating
    }

    public EnemyState enemyState;

    [HideInInspector]
    public int currentState;

    // Start is called before the first frame update
    void Start()
    {
        enemyState = EnemyState.patroling;
    }

    // Update is called once per frame
    void Update()
    {
        switch (enemyState)
        {
            case EnemyState.patroling:
                // is patroling
                currentState = 1;
                break;
            case EnemyState.searching:
                // is searching
                currentState = 2;
                break;
            case EnemyState.chaseing:
                // is chasing
                currentState = 3;
                break;
            case EnemyState.attacking:
                // is attacking
                currentState = 4;
                break;
            case EnemyState.retreating:
                // is retreating
                currentState = 5;
                break;
        }
    }
}
