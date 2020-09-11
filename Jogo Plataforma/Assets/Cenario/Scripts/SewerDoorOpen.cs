using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SewerDoorOpen : MonoBehaviour
{
    public Animator[] doors;
    public int doorcount, limit;
    public string triggername;
    private void Update()
    {
        if (doorcount >= limit)
        {
            foreach (Animator anim in doors)
            {
                anim.SetTrigger(triggername);
            }
        }
    }
}
