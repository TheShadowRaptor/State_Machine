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
                break;
            case EnemyState.searching:
                // is searching
                break;
            case EnemyState.chaseing:
                // is chasing
                break;
            case EnemyState.attacking:
                // is attacking
                break;
            case EnemyState.retreating:
                // is retreating
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
