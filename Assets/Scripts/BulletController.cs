using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    public float speed;
    public float lifetime;

    private float time;

    private void Start()
    {
        GetComponent<Rigidbody2D>().velocity = transform.up * speed;
        time = Time.time;
    }

    private void Update()
    {
        if (Time.time - time > lifetime)
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(gameObject);
    }
}
