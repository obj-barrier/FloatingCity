using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour
{
    Animator animator;
    BoxCollider2D doorCollider;

    void Start()
    {
        Transform parent = transform.parent;
        animator = parent.GetComponent<Animator>();
        doorCollider = parent.GetComponent<BoxCollider2D>();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        animator.SetBool("isOpening", true);
        doorCollider.enabled = false;
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        animator.SetBool("isOpening", false);
        doorCollider.enabled = true;
    }
}
