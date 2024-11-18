using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    public GameObject aimLine;
    public float weaponCld;
    public GameObject bullet;

    private float nextShot = 0f;
    private float bulletOffset;

    private void Start()
    {
        bulletOffset = 1.1f * transform.parent.localScale.y;
    }

    private void Update()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0f;
        transform.up = mousePos - transform.position;

        if (Time.timeSinceLevelLoad > nextShot)
        {
            if (Input.GetMouseButtonDown(0))
            {
                aimLine.SetActive(true);
            }
            else if (aimLine.activeSelf && Input.GetMouseButtonUp(0))
            {
                aimLine.SetActive(false);
                Instantiate(bullet, transform.position + transform.up * bulletOffset, transform.rotation);
                GetComponent<AudioSource>().Play();
                nextShot = Time.timeSinceLevelLoad + weaponCld;
            }
        }
    }
}
