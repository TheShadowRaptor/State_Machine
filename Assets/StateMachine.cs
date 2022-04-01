using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine : MonoBehaviour
{
    enum EnemyState
    {
        patroling,
        chaseing,
        searching,
        attacking,
        retreating
    }

    [HideInInspector]
    public int currentState;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        EnemyState enemyState = EnemyState.patroling;

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
        /*if (enemyState == EnemyState.patrol)
        {
            // do patrol stuff...
        }
        else if (enemyState == EnemyState.chase)
        {
            // do chase stuff...
        }
        else if (enemyState == EnemyState.search)
        {
            // do search stuff...
        }
        else if (enemyState == EnemyState.attack)
        {
            // do attacking stuff...
        }
        else if (enemyState == EnemyState.runaway)
        {
            // do runaway things
        }*/
    }
}
