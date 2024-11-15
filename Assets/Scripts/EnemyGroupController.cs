using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemyGroupController : MonoBehaviour
{
    public float patrolInterval;
    public GameObject terminalGroup;

    private float nextPatrolTime;
    private List<EnemyController> enemies;
    private List<Transform> terminals;
    private bool patrolling = false;

    private void Start()
    {
        nextPatrolTime = Time.time + patrolInterval;
        enemies = GetComponentsInChildren<EnemyController>().Skip(1).ToList();
        terminals = terminalGroup.GetComponentsInChildren<Transform>().Skip(1).ToList();
    }

    private void Update()
    {
        if (!patrolling && Time.time > nextPatrolTime)
        {
            enemies[Random.Range(0, enemies.Count)].StartPatrol(terminals[Random.Range(0, terminals.Count)]);
            patrolling = true;
        }
    }

    public void NextPatrol()
    {
        nextPatrolTime = Time.time + patrolInterval;
        patrolling = false;
    }
}
