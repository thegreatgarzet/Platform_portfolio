using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableCameraCollider : MonoBehaviour
{
    public CinemachineConfiner cinemachine;
    public Collider2D collider2d;
    private void Awake()
    {
        collider2d = cinemachine.m_BoundingShape2D;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            cinemachine.m_BoundingShape2D = null;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            cinemachine.m_BoundingShape2D = collider2d;
        }
    }
}
