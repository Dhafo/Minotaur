using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using Unity.Mathematics;

public class Entity : MonoBehaviour
{
    public float moveSpeed = 4.75f;
    public bool isMoving = false;
    public Vector3 destination;
    int pathIndex = 0;

    public void MoveEntity(string direction, bool isPlayer)
    {
        if(isMoving == false)
        {
            isMoving = true;
            switch (direction)
            {
                case "up":
                    float yPositive = transform.localPosition.y + 1f;
                    destination = new Vector3(transform.localPosition.x, yPositive, 0f);
                    StartCoroutine(MoveIfCan(isPlayer));
                    break;
                case "down":
                    float yNegative = transform.localPosition.y - 1f;
                    destination = new Vector3(transform.localPosition.x, yNegative, 0f);
                    StartCoroutine(MoveIfCan(isPlayer));
                    break;
                case "right":
                    float xPositive = transform.localPosition.x + 1f;
                    destination = new Vector3(xPositive, transform.localPosition.y, 0f);
                    StartCoroutine(MoveIfCan(isPlayer));
                    break;
                case "left":
                    float xNegative = transform.localPosition.x - 1f;
                    destination = new Vector3(xNegative, transform.localPosition.y, 0f);
                    StartCoroutine(MoveIfCan(isPlayer));
                    break;
            }
        }
    }

    public void reallocateEntity(int ax, int ay)
    { 
        transform.localPosition = new Vector3(ax + 0.5f, ay + 0.5f, 0);
    }

    public void MoveByPath(List<int2> path, bool isPlayer)
    {
        Vector3Int startPos = new Vector3Int(Mathf.FloorToInt(transform.position.x), Mathf.FloorToInt(transform.position.y), 0);
        isMoving = true;
        foreach(int2 node in path)
        {
            destination = new Vector3(node.x + .5f, node.y + .5f, 0);
            StartCoroutine(MoveIfCan(isPlayer));
        }
    }
   

    IEnumerator MoveIfCan(bool isPlayer)
    {
        Vector3 startPos = transform.position;
        float t = 0f;
        if (!GridGen.__wallMap.HasTile(new Vector3Int(Mathf.FloorToInt(destination.x), Mathf.FloorToInt(destination.y), 0)))
        {
            while (t < 1f)
            {
                t += Time.deltaTime * moveSpeed;
                transform.position = Vector3.Lerp(startPos, destination, t);
                PlayerFOV.instance.PlayerVisibility(new Vector3Int(Mathf.FloorToInt(transform.position.x), Mathf.FloorToInt(transform.position.y), 0));
                yield return null;
            }
        } 
        isMoving = false;
        yield return 0;
    }

}
