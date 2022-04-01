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
    public GameObject pointDest;
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
        pointDest.transform.position = patrolPoint[0].transform.position;
        timerReset = timer;
        patrolSpeed = agent.speed;
        chaseSpeed = agent.speed * 2;
    }

    // Update is called once per frame
    void Update()
    {
        
        switch (stateMachine.currentState)
        {
            case 1:
                // patrol 3 points on map
                enemyBody.GetComponent<MeshRenderer>().material = stateMaterial[(int)stateMachine.enemyState];
                agent.SetDestination(pointDest.transform.position);                
                break;

            case 2:
                // lost sight of player
                //Time until chase stops
                enemyBody.GetComponent<MeshRenderer>().material = stateMaterial[(int)stateMachine.enemyState];
                timer += Time.deltaTime;
                if (timer > searchTime)
                {
                    timer = timerReset;
                    stateMachine.enemyState = StateMachine.EnemyState.retreating;
                }
                break;

            case 3:
                // resets search/attack Timer
                timer = timerReset;
                // chases player
                enemyBody.GetComponent<MeshRenderer>().material = stateMaterial[(int)stateMachine.enemyState];
                agent.SetDestination(player.transform.position);
                agent.speed = chaseSpeed;
                break;

            case 4:
                // attacks the player
                enemyBody.GetComponent<MeshRenderer>().material = stateMaterial[(int)stateMachine.enemyState];
                agent.SetDestination(player.transform.position);
                timer += Time.deltaTime;
                if (timer > attackTime)
                {
                    timer = timerReset;
                    respawn.activated = true;
                    stateMachine.enemyState = StateMachine.EnemyState.retreating;
                }
                break;

            case 5:
                // retreats from player location
                enemyBody.GetComponent<MeshRenderer>().material = stateMaterial[(int)stateMachine.enemyState];
                agent.SetDestination(pointDest.transform.position);               
                if(agent.transform.position.x == pointDest.transform.position.x)
                {
                    agent.speed = patrolSpeed;
                    stateMachine.enemyState = StateMachine.EnemyState.patroling;
                }
                break;
        }

        // Swap dest to patrol points
        if (agent.transform.position.x == patrolPoint[i].transform.position.x
                && pointDest.transform.position == patrolPoint[i].transform.position)
        {
            //Swap to next point
            i += 1;
            if(i == patrolPoint.Length)
            {
                i = 0;
            }
            pointDest.transform.position = patrolPoint[i].transform.position;
        }
        
    }
}
