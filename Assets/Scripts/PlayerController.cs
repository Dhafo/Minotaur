using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using Unity.Mathematics;

public class PlayerController : MonoBehaviour
{
    Entity player;
    int2 currentMove;
    private void Start()
    {
        player = GameManager.__player.GetComponent<Entity>();
        player.isPlayer = true;
    }
    private void Update()
    {
        if(player.currentMoveOrder.Count > 0 && player.turn == Entity.TurnState.OnTurn)
        {
            currentMove = player.currentMoveOrder.Dequeue();
            player.MoveToPosition(currentMove.x, currentMove.y);
        }

        if (Input.GetKeyDown(KeyCode.W))
        {
            player.MoveEntity("up");
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            player.MoveEntity("down");
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            player.MoveEntity("right");

        }
        else if (Input.GetKeyDown(KeyCode.A))
        {
            player.MoveEntity("left");
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            // TODO: Will implement game menu screen here in the future.
        }
 
    }
}
