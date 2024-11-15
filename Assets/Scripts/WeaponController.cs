using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    public GameObject arrow;
    public float offset;

    private void Update()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0f;
        transform.up = mousePos - transform.position;

        if (Input.GetMouseButtonDown(0))
        {
            Instantiate(arrow, transform.position + transform.up * offset, transform.rotation);
        }
    }
}
