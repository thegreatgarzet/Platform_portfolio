using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChainedSpin : MonoBehaviour
{
    public float rotation;
    void Update()
    {
        transform.Rotate(0, 0, rotation * Time.deltaTime);
    }
}
