using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    public GameObject arrow;

    void Update()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0f;
        transform.right = mousePos - transform.position;

        if (Input.GetMouseButtonUp(0))
        {
            Instantiate(arrow, transform.position + transform.right * 12f, transform.rotation);
        }
    }
}
