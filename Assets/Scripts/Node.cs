using UnityEngine;
using System.Collections;

public class Node
{

	public GridGen.gridSpace space;
	public Vector3Int worldPosition;
	public int gridX;
	public int gridY;

	public int gCost;
	public int hCost;
	public Node parent;

	public Node(Vector3Int _worldPos, GridGen.gridSpace _space)
	{
		space = _space;
		worldPosition = _worldPos;
		gridX = worldPosition.x;
		gridY = worldPosition.y;
	}

	public int fCost
	{
		get
		{
			return gCost + hCost;
		}
	}
}
