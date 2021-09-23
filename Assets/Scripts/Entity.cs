using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Entity : MonoBehaviour
{
    public float moveSpeed = 4.75f;
    public bool isMoving = false;
    public Vector3 destination;

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
        t = 0f;
        yield return new WaitForSeconds(.05f);
        isMoving = false;
        yield return 0;
    }

}
