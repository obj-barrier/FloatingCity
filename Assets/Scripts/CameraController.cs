using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private void Update()
    {
        Camera.main.orthographicSize *= Mathf.Pow(2f, -Input.mouseScrollDelta.y);
    }
}
