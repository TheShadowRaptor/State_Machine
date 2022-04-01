using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class EnemyAI : MonoBehaviour
{
    [Header("Timers")]
    [SerializeField] private float searchWaitTime = 2.0f;
    [SerializeField] private float retreatWaitTime = 2.0f;
    private float timer = 0;
    private float timerReset;

    [Header("Agent")]
    public NavMeshAgent agent;

    [Header("PlayerObjects")]
    public GameObject player;

    [Header("PatrolPointObjects")]
    public GameObject pointDest;
    public GameObject pointA;
    public GameObject pointB;
    public GameObject pointC;

    [Header("Scripts")]
    public StateMachine stateMachine;

    /*[HideInInspector]*/
    public int state;

    // Start is called before the first frame update
    private void Start()
    {
        stateMachine.GetComponent<StateMachine>();
        pointDest.transform.position = pointA.transform.position;
        float timerReset = timer += timer;
    }

    // Update is called once per frame
    void Update()
    {
       switch (stateMachine.currentState)
       {
            case 1:
                // patrol 3 points on map
                agent.SetDestination(pointDest.transform.position);                
                break;

            case 2:
                // lost sight of player
                agent.SetDestination(agent.transform.position);
                timer += Time.deltaTime;
                if (timer > searchWaitTime)
                {
                    timer = timerReset;
                    stateMachine.enemyState = StateMachine.EnemyState.retreating;
                }
                break;

            case 3:
                // chases player
                agent.SetDestination(player.transform.position);
                break;

            case 4:
                // attacks the player
                agent.SetDestination(player.transform.position);
                break;

            case 5:
                // retreats from player location
                agent.SetDestination(-player.transform.position);
                timer += Time.deltaTime;
                if (timer > retreatWaitTime)
                {
                    timer = timerReset;
                    stateMachine.enemyState = StateMachine.EnemyState.patroling;
                }
                break;
       }

        // Swap dest to patrol points
        if (agent.transform.position.x == pointA.transform.position.x
            && pointDest.transform.position == pointA.transform.position) pointDest.transform.position = pointB.transform.position;

        else if (agent.transform.position.x == pointB.transform.position.x
            && pointDest.transform.position == pointB.transform.position) pointDest.transform.position = pointC.transform.position;

        else if (agent.transform.position.x == pointC.transform.position.x
            && pointDest.transform.position == pointC.transform.position) pointDest.transform.position = pointA.transform.position;
    }
}
