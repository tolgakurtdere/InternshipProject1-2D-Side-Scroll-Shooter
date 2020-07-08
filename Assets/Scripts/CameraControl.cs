using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    GameObject player;
    Vector3 initialPosition;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        initialPosition = transform.position - player.transform.position;
    }

    void LateUpdate() //suggested for camera events
    {
        transform.position = Vector3.Lerp(transform.position, (initialPosition + player.transform.position), 0.05f);
    }
}
