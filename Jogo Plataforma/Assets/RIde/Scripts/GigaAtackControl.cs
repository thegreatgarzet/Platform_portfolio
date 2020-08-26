using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GigaAtackControl : MonoBehaviour
{
    BoxCollider2D box;
    public List<GameObject> enemy;
    public GameObject explosionFx;
    public int dano, danobase;
    private void Start()
    {
        box = GetComponent<BoxCollider2D>();
        dano = danobase;
    }
    public void EnableBox()
    {
        box.enabled = true;
        
    }
    public void DisableBox()
    {
        box.enabled = false;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("inimigo"))
        {
            enemy.Add(collision.gameObject);
        }
    }
    public void ApplyDamage()
    {
        foreach (GameObject inimigo in enemy)
        {
            Instantiate(explosionFx, new Vector2(inimigo.transform.position.x, inimigo.transform.position.y+ 1) , Quaternion.identity);
            inimigo.GetComponent<Inimigo_Basico_Hp_Control>().vida -= dano;
        }
    }
    public void ClearList()
    {
        enemy.Clear();
    }
}
