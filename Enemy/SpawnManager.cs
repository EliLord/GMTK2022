using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
	[SerializeField] private GameObject player;
	[SerializeField] private GameObject[] enemyPool;
	[SerializeField] private Transform[] spawnPointPool;

	private List<Transform> spawnPointPoolCopy;

	public void SpawnWave()
	{
		spawnPointPoolCopy.Clear();
		spawnPointPoolCopy.AddRange(spawnPointPool);
		for (int i = 0; i < 6; i++) SpawnNewEnemy();
	}

	private void SpawnNewEnemy()
	{
		Transform spawnPoint = GetRandomSpawnPoint();

		int randomEnemyIndex = Random.Range(0, enemyPool.Length);
		GameObject curSpawn = Instantiate(enemyPool[randomEnemyIndex], spawnPoint);
		curSpawn.GetComponent<BasicEnemyController>().SetTarget(player.transform);
		curSpawn.transform.SetParent(null);
	}

	private Transform GetRandomSpawnPoint()
	{
		int randomSpawnIndex = Random.Range(0, spawnPointPoolCopy.Count);
		Transform spawnPoint = spawnPointPoolCopy[randomSpawnIndex];
		spawnPointPoolCopy.RemoveAt(randomSpawnIndex);

		return spawnPoint;
	}

	private void Start()
	{
		spawnPointPoolCopy = new List<Transform>(spawnPointPool.Length);

		SpawnWave();
	}
}