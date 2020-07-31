using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuporterBullet1 : MonoBehaviour
{
    public Transform targetPos;
    public float speed;
    public int dano;
    private void Start()
    {
        
    }
    private void Update()
    {
        if (targetPos == null)
        {
            Destroy(gameObject);
        }
        
        transform.position = Vector2.MoveTowards(transform.position, new Vector2(targetPos.transform.position.x * 2, targetPos.transform.position.y * 2), speed);
        Destroy(gameObject, 2f);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("inimigo"))
        {
            collision.GetComponent<Inimigo_Basico_Hp_Control>().vida-=dano;
            Destroy(gameObject);
        }
    
    }
}
