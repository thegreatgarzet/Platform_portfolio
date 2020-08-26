using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnTriggerDamage : MonoBehaviour
{
    public int dano;
    public GameObject hit_fx;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("inimigo"))
        {
            collision.GetComponent<Inimigo_Basico_Hp_Control>().vida -= dano;
        }
        Destroy(gameObject.transform.parent.gameObject);
        Instantiate(hit_fx, transform.position, Quaternion.identity);
    }
}
