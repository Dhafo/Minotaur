using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PlayerFOV : MonoBehaviour
{
    public static PlayerFOV instance;

    private PlayerController player;

    private WorldTile _tile;

    public int fovDistance = 7;

    public List<Vector3Int> visibleTiles;

    public Tile fovPartial;

    void Awake()
    {

        if (instance != null)
        {
            Debug.LogWarning("More than 1 instance of PlayerFov");
            return;
        }
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        player = gameObject.GetComponent<PlayerController>();
        visibleTiles = new List<Vector3Int>();
        StartCoroutine(LateStart(.05f));
    }

    IEnumerator LateStart(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        PlayerVisibility(new Vector3Int(Mathf.FloorToInt(player.transform.position.x), Mathf.FloorToInt(player.transform.position.y), 0));
    }

    public void PlayerVisibility(Vector3Int lPos)
    {
        WorldTile _tile;
        var tiles = GameTiles.instance.tiles;

        //Removes visibleTiles.
        foreach (Vector3Int visibleTile in visibleTiles)
        {
            if (!tiles.TryGetValue(visibleTile, out _tile)) return;
            if (_tile.isVisible)
            {
                _tile.isVisible = false;
                GridGen.__fovMap.SetTileFlags(_tile.localPlace, TileFlags.None);
                GridGen.__fovMap.SetTile(_tile.localPlace, fovPartial);
            }
        }
        visibleTiles = new List<Vector3Int>();
        FOVGenerator.FovCompute(lPos, fovDistance);
    }
}
