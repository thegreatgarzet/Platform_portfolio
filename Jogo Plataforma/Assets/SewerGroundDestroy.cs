using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SewerGroundDestroy : MonoBehaviour
{
    public bool destroy, spawnexplosions;
    public List<GameObject> explosions;
    public GameObject explosionobj, tilemap;
    private void Update()
    {
        if (destroy)
        {
            if (spawnexplosions)
            {
                explosions.Add(Instantiate(explosionobj, transform.position, Quaternion.identity));
                explosions.Add(Instantiate(explosionobj, new Vector2(transform.position.x - 1.5f, transform.position.y), Quaternion.identity));
                explosions.Add(Instantiate(explosionobj, new Vector2(transform.position.x - 3f, transform.position.y), Quaternion.identity));
                explosions.Add(Instantiate(explosionobj, new Vector2(transform.position.x + 3f, transform.position.y), Quaternion.identity));
                explosions.Add(Instantiate(explosionobj, new Vector2(transform.position.x + 1.5f, transform.position.y), Quaternion.identity));
                spawnexplosions = false;
            }
            else
            {
                if(explosions[0]==null && explosions[1] == null && explosions[2] == null)
                {
                    Destroy(tilemap);
                }
            }
        }
    }
}
