using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectPlayerOnRange : MonoBehaviour
{
    public bool _PlayerOnRange;
    public Vector2 boxSize;
    public LayerMask playerLayer;
    private void Update()
    {
        _PlayerOnRange = Physics2D.OverlapBox(transform.position, boxSize, 0, playerLayer);
    }
}
