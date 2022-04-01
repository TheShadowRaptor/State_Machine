using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Respawn : MonoBehaviour
{
    public GameObject player;
    public GameObject spawn;
    public CharacterController controller;

    public bool activated = false;

    private void Start()
    {
        controller.GetComponent<CharacterController>();
    }
    // Update is called once per frame
    void Update()
    { 
        while (activated == true)
        {
            controller.enabled = false;
            player.transform.position = spawn.transform.position;
            controller.enabled = true;
            activated = false;
        }
    }
}
