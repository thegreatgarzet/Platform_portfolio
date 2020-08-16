using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectPlayerOnRange : MonoBehaviour
{
    public bool _PlayerOnRange;
    public Vector2 boxSize;
    public LayerMask playerLayer;
    public bool _ShowObject;
    public GameObject _ObjectToShow;
    private void Update()
    {
        _PlayerOnRange = Physics2D.OverlapBox(transform.position, boxSize, 0, playerLayer);
        if(_PlayerOnRange && _ShowObject)
        {
            _ObjectToShow.SetActive(true);
        }
        else if(_ShowObject && !_PlayerOnRange)
        {
            _ObjectToShow.SetActive(false);
        }
    }
}
