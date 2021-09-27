using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Engine : MonoBehaviour
{

	public static GameObject __player;
	public void SpawnPlayer(int x, int y)
	{
		GameObject playerObject = Resources.Load<GameObject>("Prefabs/Player");
		__player = Instantiate(playerObject, Vector3.zero, Quaternion.identity);
		__player.GetComponent<Entity>().reallocateEntity(x, y);
		CameraController cam = GameObject.FindWithTag("MainCamera").GetComponent<CameraController>();
		cam.FollowTarget(__player.transform);
	}

	public void SpawnEnemy(int x, int y)
    {
		GameObject enemyObject = Resources.Load<GameObject>("Prefabs/Enemy");
		GameObject enemy = Instantiate(enemyObject, Vector3.zero, Quaternion.identity);
		enemy.GetComponent<Entity>().reallocateEntity(x, y);
	}


}
