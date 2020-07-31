using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniMapControl : MonoBehaviour
{
    Vector2 directions;
    Rigidbody2D rb;
    public float speed;
    Vector3 player;
    bool speedUp=false;
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.Find("MainChar").GetComponent<Transform>().position;
    }
    private void FixedUpdate()
    {
        rb.velocity = new Vector2(directions.x * speed, directions.y * speed);
    }
    private void Update()
    {
        directions.x = Input.GetAxisRaw("Horizontal");
        directions.y = Input.GetAxisRaw("Vertical");
        directions.Normalize();
        if (Input.GetButtonDown("Jump"))
        {
            speed *= 2;
        }
        if (Input.GetButtonUp("Jump"))
        {
            speed /= 2;
        }
        if (Input.GetButtonDown("Atirar"))
        {
            player.z = -10.0f;
            transform.position = player;
        }
    }
}
