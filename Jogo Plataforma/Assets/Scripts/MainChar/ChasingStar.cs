using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChasingStar : MonoBehaviour
{
    Rigidbody2D rb;
    public float speed, rotatespeed, timer, rotatetimer;
    public int dir;
    public Transform alvo;
    public List<Transform> alvos;
    public GameObject mira;
    public bool perdeuAlvo=false, canAddAlvo = true, addSpeed=false;
    BoxCollider2D box;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        box = GetComponent<BoxCollider2D>();
        Destroy(gameObject, 3f);
    }

    private void Update()
    {
        
        if (canAddAlvo)
        {
            timer -= Time.deltaTime;
            if (timer <= 0 && alvos.Count >0)
            {
                alvo = alvos[Random.Range(0, alvos.Count)];
                GameObject aim = Instantiate(mira, new Vector2(alvo.position.x, alvo.position.y + 0.7f) , Quaternion.identity);
                aim.GetComponent<SpriteRenderer>().sortingOrder = 5;
                box.enabled = false;
                canAddAlvo = false;
                addSpeed = true;
            }
        }
        else
        {
            if (alvo == null)
            {
                Destroy(gameObject);
            }
        }
        if (addSpeed)
        {
            rotatetimer -= Time.deltaTime;
            if (rotatetimer <= 0)
            {
                rotatespeed = rotatespeed * 3;
                addSpeed = false;
            }
            
        }
    }
    void FixedUpdate()
    {
        if (alvo == null)
        {

            rb.velocity = new Vector2(dir * speed, rb.velocity.y);
        }
        else
        {
            Vector2 direction = (Vector2)alvo.position - rb.position;
            direction.Normalize();
            float rotateAmount = Vector3.Cross(direction, transform.up).z;
            rb.angularVelocity = -rotateAmount * rotatespeed;
            rb.velocity = transform.up * speed;
          
        }
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("inimigo"))
        {
            alvos.Add(collision.transform);
        }
    }
}
