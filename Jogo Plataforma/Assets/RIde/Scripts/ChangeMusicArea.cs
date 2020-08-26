using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeMusicArea : MonoBehaviour
{
    public int music;
    public string namearea;
    public MusicControl musicControl;
    public AreaName areaName;
    private void Awake()
    {
        musicControl = FindObjectOfType<MusicControl>();
        areaName = FindObjectOfType<AreaName>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && areaName.text.text != namearea || collision.CompareTag("RideArmor") && areaName.text.text != namearea)
        {
            areaName.text.text = namearea;
            areaName.anim.SetTrigger("changearea");
            if(musicControl.actualmusic != music)
            {
                musicControl.nextmusic = music;
                musicControl.switchmusic = true;
            }
        }
        if (collision.CompareTag("Player") && musicControl.actualmusic != music || collision.CompareTag("RideArmor") && musicControl.actualmusic != music)
        {
            musicControl.nextmusic = music;
            musicControl.switchmusic = true;
        }
        
    }
}
