using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn_Ride_Armor : MonoBehaviour
{
    public GameObject ridearmorPrefab, ridearmorInstance;
    public bool visible, spawned;
    public float timer, timerb;
    private void Update()
    {
        if(ridearmorInstance == null && !visible)
        {
            //ridearmorInstance = Instantiate(ridearmorPrefab, new Vector2(transform.position.x, transform.position.y + 1.5f), Quaternion.identity);
        }
        OnScreen();
    }
    public void OnScreen()
    {
        Vector3 visTest = Camera.main.WorldToViewportPoint(transform.position);
        if (visTest.x >= 0 && visTest.x <= 1.3f && visTest.y >= 0 && visTest.y <= 1.3f)
        {
            visible = true;
        }
        else
        {
            visible = false;
        }
        if (!visible && !spawned && ridearmorInstance == null)
        {
            if (timer > 0)
            {
                timer -= Time.deltaTime;
            }
            else
            {
                InstantiateRide();
                timer = timerb;
                spawned = true;
            }
        }
    }
    public void InstantiateRide()
    {
        ridearmorInstance = Instantiate(ridearmorPrefab, new Vector2(transform.position.x, transform.position.y + 1.5f), Quaternion.identity);
    }
}
