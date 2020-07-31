using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBallHiper : MonoBehaviour
{
    public bool destroyOnGround, touchedGround;
    public int damage;
    public float timer;
    private void Update()
    {

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (destroyOnGround)
        {
            Destroy(gameObject);

        }
        else
        {
            if (collision.transform.tag == "inimigo")
            {
                collision.gameObject.GetComponent<Inimigo_Basico_Hp_Control>().vida -= damage;
            }
            
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("ground"))
        {
            Debug.Log("bateu");
            Destroy(gameObject, timer);
        }
    }

}
