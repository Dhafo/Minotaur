using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using Unity.Mathematics;

public class Entity : MonoBehaviour
{
    public bool isPlayer = false;
    public enum TurnState
    {
        OnTurn,
        Waiting
    }

    public Queue<int2> currentMoveOrder;

    public TurnState turn = TurnState.Waiting;

    public float moveSpeed = 4.75f;
    public bool isMoving = false;
    public Vector3 destination;

    private void Start()
    {
        currentMoveOrder = new Queue<int2>();
    }

    public void MoveEntity(string direction)
    {
        if(turn == TurnState.OnTurn)
        {
            if (isMoving == false)
            {
                isMoving = true;
                switch (direction)
                {
                    case "up":
                        float yPositive = transform.localPosition.y + 1f;
                        destination = new Vector3(transform.localPosition.x, yPositive, 0f);
                        StartCoroutine(MoveIfCan());
                        break;
                    case "down":
                        float yNegative = transform.localPosition.y - 1f;
                        destination = new Vector3(transform.localPosition.x, yNegative, 0f);
                        StartCoroutine(MoveIfCan());
                        break;
                    case "right":
                        float xPositive = transform.localPosition.x + 1f;
                        destination = new Vector3(xPositive, transform.localPosition.y, 0f);
                        StartCoroutine(MoveIfCan());
                        break;
                    case "left":
                        float xNegative = transform.localPosition.x - 1f;
                        destination = new Vector3(xNegative, transform.localPosition.y, 0f);
                        StartCoroutine(MoveIfCan());
                        break;
                }
            }
        }
    }

    public void MoveToPosition(int x, int y)
    {
        if (turn == TurnState.OnTurn)
        {
            if (isMoving == false)
            {
                isMoving = true;
                destination = new Vector3(x + 0.5f, y + 0.5f, 0f);
                StartCoroutine(MoveIfCan());
            }
        }
        isMoving = false;
    }

    public void reallocateEntity(int ax, int ay)
    { 
        transform.localPosition = new Vector3(ax + 0.5f, ay + 0.5f, 0);
    }

    public void AddPath(List<int2> path)
    {
        currentMoveOrder.Clear();
        foreach(int2 node in path)
        {
            currentMoveOrder.Enqueue(node);
        }
    }
   

    IEnumerator MoveIfCan()
    {
        //Vector3 startPos = transform.position;
        //float t = 0f;
        if (!GridGen.__wallMap.HasTile(new Vector3Int(Mathf.FloorToInt(destination.x), Mathf.FloorToInt(destination.y), 0)))
        {
            //while (t < 1f)
            // {
            //  t += Time.deltaTime * moveSpeed;
            //transform.position = Vector3.Lerp(startPos, destination, t);
            transform.position = destination;
                if (isPlayer)
                {
                    PlayerFOV.instance.PlayerVisibility(new Vector3Int(Mathf.FloorToInt(transform.position.x), Mathf.FloorToInt(transform.position.y), 0));
                }
                yield return null;
           // }
        } 
        isMoving = false;
        GameManager.GetInstance().NextTurn();
        yield return 0;
    }

}
