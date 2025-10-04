using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinPropellerX : MonoBehaviour
{
    public float speed = 1000f;

    void Update()
    {
        // Spin the propeller around its Z-axis
        transform.Rotate(Vector3.forward * speed * Time.deltaTime);
    }
}

