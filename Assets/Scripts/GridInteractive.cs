using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UIElements;
using Unity.Mathematics;


public class GridInteractive : MonoBehaviour
{
    private Grid grid;
    public Pathfinding pathfinder;
    [SerializeField] private Tilemap interactiveMap = null;
    [SerializeField] private Tile hoverTile = null;

    //[SerializeField] private RuleTile pathTile = null;

    private Vector3Int previousMousePos = new Vector3Int();
    // Start is called before the first frame update
    void Start()
    {
        grid = GetComponent<Grid>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3Int mousePos = GetMousePosition();

        if (!mousePos.Equals(previousMousePos))
        {

            interactiveMap.SetTile(previousMousePos, null); // Remove old hoverTile

            interactiveMap.SetTile(mousePos, hoverTile);

            previousMousePos = mousePos;
        }

        if(GameManager.__player.GetComponent<Entity>().turn == Entity.TurnState.OnTurn)
        {
            if (Input.GetMouseButtonDown(0))
            {
                Vector3Int startPos = new Vector3Int(Mathf.FloorToInt(GameManager.__player.transform.position.x), Mathf.FloorToInt(GameManager.__player.transform.position.y), 0);
                if (GameManager.__gridGen.grid[mousePos.x, mousePos.y] == GridGen.gridSpace.floor)
                {
                    List<int2> path = pathfinder.FindPath(new int2(startPos.x, startPos.y), new int2(mousePos.x, mousePos.y));
                    GameManager.__player.GetComponent<Entity>().AddPath(path);
                }
            }
        }
       


    }

    Vector3Int GetMousePosition()
    {

        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        return grid.WorldToCell(mouseWorldPos);
    }
}
