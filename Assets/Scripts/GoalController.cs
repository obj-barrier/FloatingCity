using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalController : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<BulletController>())
        {
            Time.timeScale = 0f;
            Destroy(gameObject);
        }
    }
}
