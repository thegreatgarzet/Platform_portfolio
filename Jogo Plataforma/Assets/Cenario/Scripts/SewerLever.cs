using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SewerLever : MonoBehaviour
{
    DetectPlayerOnRange playerOnRange;
    public SewerDoorOpen sewerDoorOpen;
    public bool canAdd = true;
    private void Awake()
    {
        playerOnRange = GetComponent<DetectPlayerOnRange>();

    }
    private void Update()
    {
        if (Input.GetAxisRaw("Vertical") > 0 && playerOnRange._PlayerOnRange && canAdd)
        {
            sewerDoorOpen.doorcount++;
            canAdd=false;
        }
    }
}
