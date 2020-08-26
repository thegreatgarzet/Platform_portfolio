using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotProjectileTeste : MonoBehaviour
{
    public GameObject projetil;
    public Vector2 force;
    private void FixedUpdate()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            GameObject faca = Instantiate(projetil, transform.position, Quaternion.identity);
            faca.GetComponent<Rigidbody2D>().AddForce(force, ForceMode2D.Impulse);
        }
    }

}
