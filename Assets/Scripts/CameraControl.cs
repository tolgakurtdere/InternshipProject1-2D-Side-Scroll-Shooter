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

    public IEnumerator Shake(float duration, float magnitude)
    {
        Vector3 initPos = transform.localPosition;
        float tempTime = 0;
        while (tempTime < duration)
        {
            float x = Random.Range(-1f, 1f) * magnitude;
            float y = Random.Range(-1f, 1f) * magnitude;

            transform.localPosition = new Vector3(x + initPos.x, y + initPos.y, initPos.z);
            tempTime += Time.deltaTime;
            yield return null;
        }
        //transform.localPosition = initPos;
    }
}
