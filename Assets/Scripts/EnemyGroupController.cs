using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemyGroupController : MonoBehaviour
{
    public float patrolInterval;
    public GameObject terminalGroup;
    public GateController gate;

    private float nextPatrolTime;
    private List<EnemyController> enemies;
    private List<Transform> terminals;
    private bool patrolling = false;

    private void Start()
    {
        nextPatrolTime = patrolInterval;
        enemies = GetComponentsInChildren<EnemyController>().ToList();
        enemies[Random.Range(0, enemies.Count)].ReceiveKey();
        terminals = terminalGroup.GetComponentsInChildren<Transform>().Skip(1).ToList();
    }

    private void Update()
    {
        if (enemies.Count == 0)
        {
            return;
        }

        if (!patrolling && Time.timeSinceLevelLoad > nextPatrolTime)
        {
            enemies[Random.Range(0, enemies.Count)].StartPatrol(terminals[Random.Range(0, terminals.Count)]);
            patrolling = true;
        }
    }

    public void NextPatrol()
    {
        nextPatrolTime = Time.timeSinceLevelLoad + patrolInterval;
        patrolling = false;
    }

    public void Remove(EnemyController enemy)
    {
        enemies.Remove(enemy);
    }
}
