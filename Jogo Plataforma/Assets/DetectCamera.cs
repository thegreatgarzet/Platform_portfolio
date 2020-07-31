using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectCamera : MonoBehaviour
{
    public List<GameObject> inimigosLista;
    public GameObject inimigoSpawnado;
    public bool spawned=false, visible;
    public float timer, timerB;
    public int inimigo;
    private void Start()
    {
        timer = timerB;
        if (!spawned)
        {
            InstantiateEnemy();
            spawned = true;
        }
    }
    void Update()
    {
        Vector3 visTest = Camera.main.WorldToViewportPoint(transform.position);
        if (visTest.x>=0 &&visTest.x<=1&&visTest.y>=0&&visTest.y<=1)
        {
            visible = true;
        }
        else
        {
            visible = false;
        }
        if (!visible && !spawned)
        {
            if (timer > 0)
            {
                timer -= Time.deltaTime;
            }
            else
            {
                InstantiateEnemy();
                timer = timerB;
                spawned = true;
            }
        }
        if (inimigoSpawnado == null)
        {
            spawned = false;
        }
    }
    private void InstantiateEnemy()
    {
        inimigoSpawnado = Instantiate(inimigosLista[inimigo], transform.position, Quaternion.identity);
        Debug.Log("spawned");
    }
}
