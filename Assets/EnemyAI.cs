using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class EnemyAI : MonoBehaviour
{
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
                // goes back to partol
                agent.SetDestination(pointDest.transform.position);
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
