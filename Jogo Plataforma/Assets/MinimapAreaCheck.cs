using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinimapAreaCheck : MonoBehaviour
{
    public string areaName;
    public int id;
    public Transform cameraPos;
    public bool canCollide;
    public float timer = 0.2f;
    private void Update()
    {
        if (!canCollide)
        {
            timer -= Time.deltaTime;
            if (timer <=0)
            {
                canCollide = true;
                timer = 0.2f;
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (canCollide)
        {
            Vector3 newPos;
            areaName = collision.GetComponent<MinimapAreaHitBox>().areaName;
            id = collision.GetComponent<MinimapAreaHitBox>().id;
            collision.GetComponent<Animator>().SetInteger("state", 1);
            newPos = collision.transform.position;
            newPos.z = 0.0f;
        }
        /*print(newPos);
        cameraPos.position = Vector2.MoveTowards(transform.position, newPos, 1f);
        transform.position = new Vector3(newPos.x, newPos.y, 0.0f);*/
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        areaName = "";
        //id = 0;
        collision.GetComponent<Animator>().SetInteger("state", 0);
    }
}
