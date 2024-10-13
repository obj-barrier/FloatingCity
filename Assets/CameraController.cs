using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    void Update()
    {
        Camera.main.orthographicSize *= Mathf.Pow(2f, -Input.mouseScrollDelta.y);
    }
}
