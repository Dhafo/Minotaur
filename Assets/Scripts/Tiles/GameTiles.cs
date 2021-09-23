using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GameTiles : MonoBehaviour
{
	public static GameTiles instance;
	public Tilemap fovMap;
	public Dictionary<Vector3, WorldTile> tiles;

	private void Awake()
	{
		if (instance == null)
		{
			instance = this;
		}
		else if (instance != this)
		{
			Destroy(gameObject);
		}

		Invoke("GetWorldTiles", 0.1f);
	}

	// Use this for initialization
	private void GetWorldTiles()
	{
		tiles = new Dictionary<Vector3, WorldTile>();

		foreach (Vector3Int pos in fovMap.cellBounds.allPositionsWithin)
		{
			var lPos = new Vector3Int(pos.x, pos.y, pos.z);

			if (!fovMap.HasTile(lPos)) continue;
			var tile = new WorldTile
			{
				localPlace = lPos,
				gridLocation = fovMap.CellToWorld(lPos),
				tileBase = fovMap.GetTile(lPos),
				isVisible = false,
				isExplored = false,
			};
			tiles.Add(tile.gridLocation, tile);
		}
	}
}