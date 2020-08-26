using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectPlayerOnRange : MonoBehaviour
{
    public bool _PlayerOnRange;
    public Vector2 boxSize, boxOffset;
    public LayerMask playerLayer;
    public bool _ShowObject;
    public GameObject _ObjectToShow;
    private void Update()
    {
        Vector2 newpos;
        newpos.x = transform.position.x + boxOffset.x;
        newpos.y = transform.position.y + boxOffset.y;
        _PlayerOnRange = Physics2D.OverlapBox(newpos, boxSize, 0, playerLayer);
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
