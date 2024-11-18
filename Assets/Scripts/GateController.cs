using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GateController : MonoBehaviour
{
    public void Unlock()
    {
        Destroy(gameObject);
    }
}
