using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animation_Controller : MonoBehaviour
{
    public Animator animator;
    MovementController movementController;
    int state;
    private void Start()
    {
        animator = GetComponent<Animator>();
        movementController = GetComponentInParent<MovementController>();
    }
    private void Update()
    {
    }
}
