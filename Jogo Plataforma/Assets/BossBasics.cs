using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBasics : MonoBehaviour
{
    GameManager gameManager;
    FollowPlayer camerafollow;
    private void Awake()
    {
        gameManager = FindObjectOfType<GameManager>();
        camerafollow = GameObject.Find("CameraFollow").GetComponent<FollowPlayer>();
    }
    public void ReleasePlayerControls()
    {
        gameManager.ReturnPlayerControls();
    }
    public void ReturnCamera()
    {
        //camerafollow.ReturnCameraToPlayer();
        gameManager.ChangeCameraToPlayer();
    }
}
