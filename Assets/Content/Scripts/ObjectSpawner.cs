using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ObjectSpawner : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private NavMeshRandom navMeshRandom;
    [SerializeField] private Enemy enemyPrefab;
    [SerializeField] private GameObject container;
    [SerializeField] private int count;
    [SerializeField] private int spawnDelayMiliseconds = 0;


    private List<Enemy> objects = new List<Enemy>();
    private NavMeshTriangulation triangulation;


    private Enemy spawnRandowwm()
    {
        var enemy = Instantiate(enemyPrefab, container.transform);
        navMeshRandom.Recalculate();
        enemy.transform.position = NavMeshRandom.GetRandomPointOnNavMesh();
        enemy.SetTarget(player);
        enemy.gameObject.SetActive(true);
        return enemy;
    }

    [ContextMenu("spawwwwn")]
    private async void SpawwnALOT()
    {
        DestroyALOT();
        for (int i = 0; i < count; i++)
        {
            objects.Add(spawnRandowwm());
            await System.Threading.Tasks.Task.Delay(spawnDelayMiliseconds);
        }
    }

    [ContextMenu("Destory")]
    private void DestroyALOT()
    {
        foreach (var item in objects)
        {
            Destroy(item);
        }
        objects.Clear();
    }
}
