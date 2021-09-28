using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Mathematics;

public class Enemy : MonoBehaviour
{
    private Entity entity;

    private int2 currentMove;

    private void Start()
    {
        entity = GetComponent<Entity>();
        entity.isPlayer = false;
    }
    public void DoSomething()
    {
        Debug.Log("move");
        int direction = UnityEngine.Random.Range(0, 4);
        if(entity.currentMoveOrder.Count > 0)
        {
            currentMove = entity.currentMoveOrder.Dequeue();
            entity.MoveToPosition(currentMove.x, currentMove.y);
        }
        else
        {
            switch (direction)
            {
                case 0:
                    entity.MoveEntity("up");
                    break;
                case 1:
                    entity.MoveEntity("down");
                    break;
                case 2:
                    entity.MoveEntity("right");
                    break;
                case 3:
                    entity.MoveEntity("left");
                    break;
            }

        }     
    }
}
