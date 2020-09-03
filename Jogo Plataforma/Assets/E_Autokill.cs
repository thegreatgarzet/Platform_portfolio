using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E_Autokill : MonoBehaviour
{
    public float timer;
    public GameObject destroyfx;
    public bool canfx = true;
    void Update()
    {
        timer -= Time.deltaTime;
        if(timer <= 0)
        {
            if (canfx)
            {
                Instantiate(destroyfx, new Vector2(transform.position.x, transform.position.y + 0.15f), Quaternion.identity);
                Destroy(gameObject);
                canfx = false;
            }
            
        }
    }
}
