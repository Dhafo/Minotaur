using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Collections;

public class GameManager : MonoBehaviour
{
	private static GameManager instance;

	public static GameManager GetInstance()
    {
		return instance;
    }

	public static GameObject __player;

	public static GridGen __gridGen;

	public Queue<Entity> entities;

	public Entity currentEntity;

    public void NextTurn()
    {
		currentEntity.turn = Entity.TurnState.Waiting;
		entities.Enqueue(currentEntity);
		currentEntity = entities.Dequeue();
		currentEntity.turn = Entity.TurnState.OnTurn;
		if (!currentEntity.isPlayer)
		{
			currentEntity.GetComponent<Enemy>().DoSomething();
		}
	}

    private void Awake()
    {
		instance = this;
		__gridGen = GetComponent<GridGen>();
		entities = new Queue<Entity>();
	}

    public void StartGame(int roomWidth, int roomHeight)
    {
		SpawnPlayer(roomHeight/2, roomHeight/2);
		SpawnEnemies();
    }
	public void SpawnPlayer(int x, int y)
	{
		GameObject playerObject = Resources.Load<GameObject>("Prefabs/Player");
		__player = Instantiate(playerObject, Vector3.zero, Quaternion.identity);
		__player.GetComponent<Entity>().reallocateEntity(x, y);
		CameraController cam = GameObject.FindWithTag("MainCamera").GetComponent<CameraController>();
		cam.FollowTarget(__player.transform);
        entities.Enqueue(__player.GetComponent<Entity>());
		currentEntity = entities.Dequeue();
		currentEntity.turn = Entity.TurnState.OnTurn;
	}

	public void SpawnEnemy(int x, int y)
    {
		GameObject enemyObject = Resources.Load<GameObject>("Prefabs/Enemy");
		GameObject enemy = Instantiate(enemyObject, Vector3.zero, Quaternion.identity);
		enemy.GetComponent<Entity>().reallocateEntity(x, y);
		entities.Enqueue(enemy.GetComponent<Entity>());
	}

	void SpawnEnemies()
	{
		int spawnCount = 1;
		while (spawnCount != 0)
		{
			for (int x = 0; x < __gridGen.roomWidth; x++)
			{
				for (int y = 0; y < __gridGen.roomHeight; y++)
				{
					if (__gridGen.grid[x, y] == GridGen.gridSpace.floor && Random.value > 0.99 && spawnCount != 0)
					{
						SpawnEnemy(x, y);
						spawnCount -= 1;
					}
				}
			}
			spawnCount = 0;
		}
	}

}
