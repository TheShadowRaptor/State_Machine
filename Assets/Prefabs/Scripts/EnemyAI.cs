using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class EnemyAI : MonoBehaviour
{
    private float chaseSpeed;
    private float patrolSpeed;

    [Header("Timers")]
    [SerializeField] private float searchTime = 2.0f;
    [SerializeField] private float attackTime = 2.0f;
    private float timer = 0;
    private float timerReset;

    [Header("Agent")]
    public NavMeshAgent agent;

    [Header("StateColors")]
    public GameObject enemyBody;
    public Material[] stateMaterial;

    [Header("PlayerObjects")]
    public GameObject player;

    [Header("PatrolPointObjects")]
    private int i = 0;
    public GameObject currentPointDest;
    public GameObject[] patrolPoint;

    [Header("Scripts")]
    public StateMachine stateMachine;
    public Respawn respawn;

    [HideInInspector]
    public int state;

    // Start is called before the first frame update
    private void Start()
    {
        stateMachine.GetComponent<StateMachine>();
        currentPointDest.transform.position = patrolPoint[0].transform.position;
        timerReset = timer;
        patrolSpeed = agent.speed;
        chaseSpeed = agent.speed * 2;
    }

    // Update is called once per frame
    void Update()
    {
        
        switch (stateMachine.enemyState)
        {
            case StateMachine.EnemyState.patrolling:
                // patrol 3 points on map
                enemyBody.GetComponent<MeshRenderer>().material = stateMaterial[(int)stateMachine.enemyState];
                agent.SetDestination(currentPointDest.transform.position);                
                break;

            case StateMachine.EnemyState.searching:
                // lost sight of player
                //Time until chase stops
                enemyBody.GetComponent<MeshRenderer>().material = stateMaterial[(int)stateMachine.enemyState];
                timer += Time.deltaTime;
                if (timer > searchTime)
                {
                    timer = timerReset;
                    stateMachine.retreating = true;
                }
                break;

            case StateMachine.EnemyState.chasing:
                // resets search/attack Timer
                timer = timerReset;
                // chases player
                enemyBody.GetComponent<MeshRenderer>().material = stateMaterial[(int)stateMachine.enemyState];
                agent.SetDestination(player.transform.position);
                agent.speed = chaseSpeed;
                break;

            case StateMachine.EnemyState.attacking:
                // attacks the player
                enemyBody.GetComponent<MeshRenderer>().material = stateMaterial[(int)stateMachine.enemyState];
                agent.SetDestination(player.transform.position);
                timer += Time.deltaTime;
                if (timer > attackTime)
                {
                    timer = timerReset;
                    respawn.activated = true;
                    stateMachine.retreating = true;
                }
                break;

            case StateMachine.EnemyState.retreating:
                // retreats from player location
                enemyBody.GetComponent<MeshRenderer>().material = stateMaterial[(int)stateMachine.enemyState];
                agent.SetDestination(currentPointDest.transform.position);               
                if(agent.transform.position.x == currentPointDest.transform.position.x)
                {
                    agent.speed = patrolSpeed;
                    stateMachine.patrolling = true;
                }
                break;
        }
        EnemyPatrol();
    }

    private void EnemyPatrol()
    {
        // Swap dest to patrol points
        if (agent.transform.position.x == patrolPoint[i].transform.position.x
                && currentPointDest.transform.position == patrolPoint[i].transform.position)
        {
            //Swap to next point
            i += 1;
            if (i == patrolPoint.Length)
            {
                i = 0;
            }
            currentPointDest.transform.position = patrolPoint[i].transform.position;
        }
    }
}
