using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UIElements;


public class GridInteractive : MonoBehaviour
{
    private Grid grid;
    [SerializeField] private Tilemap interactiveMap = null;
    [SerializeField] private Tilemap pathMap = null;
    [SerializeField] private Tile hoverTile = null;

   // [SerializeField] private RuleTile pathTile = null;

    private Vector3Int previousMousePos = new Vector3Int();
    // Start is called before the first frame update
    void Start()
    {
        grid = gameObject.GetComponent<Grid>();
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

        //if (Input.GetMouseButton(0))
        //{

           // pathMap.SetTile(mousePos, pathTile);

        //}


    }

    Vector3Int GetMousePosition()
    {

        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        return grid.WorldToCell(mouseWorldPos);
    }
}
