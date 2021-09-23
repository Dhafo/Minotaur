using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PlayerController : MonoBehaviour
{
    Entity player;
    private void Start()
    {
        player = Engine.__player.GetComponent<Entity>();
    }
    private void Update()
    {
        if (Input.GetKey(KeyCode.W))
        {
            player.MoveEntity("up", true);
        }
        else if (Input.GetKey(KeyCode.S))
        {
            player.MoveEntity("down", true);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            player.MoveEntity("right", true);
            
        }
        else if (Input.GetKey(KeyCode.A))
        {
            player.MoveEntity("left", true);
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            // TODO: Will implement game menu screen here in the future.
        }
    }
}
