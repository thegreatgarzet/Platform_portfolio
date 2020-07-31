using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBallRain : MonoBehaviour
{
    public float kickStart;
    public Rigidbody2D rb;
    public GameObject[] fireballs;
    public Transform fireballBase;
    public Vector2 pos, actualPos;
    private void Start()
    {
        rb.velocity = Vector2.up * kickStart;
        pos = transform.position;
    }
    private void Update()
    {
        actualPos = pos;
        pos = transform.position;
     
        PosCheck(pos, actualPos);
    }

    private void PosCheck(Vector2 pos, Vector2 actualPos)
    {
        
        if (actualPos.y > pos.y)
        {

            fireballBase.rotation = Quaternion.Euler(Vector3.forward * -90f);
            fireballBase.transform.parent = null;
            fireballBase.GetComponent<Rigidbody2D>().simulated = true;
            Debug.Log("Caindo");
            foreach (GameObject fireball in fireballs)
            {
                fireball.SetActive(true);
                fireball.GetComponent<Rigidbody2D>().simulated = true;
                fireball.transform.parent = null;
            }
            Destroy(gameObject);
        }
    }
}
