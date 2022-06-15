using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject player;
    private Vector3 offset;

    void Start()
    {
        //Set the offset to the cameras position minus the players position
        offset = transform.position - player.transform.position;
    }

    void LateUpdate()
    {
        //Set the transform of the camera to that of the player
        transform.position = player.transform.position + offset;
    }
}