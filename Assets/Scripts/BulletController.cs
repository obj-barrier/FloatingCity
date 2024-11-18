using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    public float speed;

    private void Start()
    {
        GetComponent<Rigidbody2D>().velocity = transform.up * speed;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        PlayerController player = collision.gameObject.GetComponent<PlayerController>();
        EnemyController enemy = collision.gameObject.GetComponent<EnemyController>();
        GoalController goal = collision.gameObject.GetComponent<GoalController>();
        if (player != null)
        {
            player.Die();
        }
        else if (enemy != null)
        {
            enemy.Die();
        }
        else if (goal != null)
        {
            goal.Win();
        }

        Destroy(gameObject);
    }
}
