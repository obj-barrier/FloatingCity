using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimLineController : MonoBehaviour
{
    public float lineLength;

    private LineRenderer aimLine;

    private void Start()
    {
        aimLine = GetComponent<LineRenderer>();
    }

    private void Update()
    {
        aimLine.SetPosition(1, new Vector3(0f, 0.5f + lineLength, 0f));
    }
}
