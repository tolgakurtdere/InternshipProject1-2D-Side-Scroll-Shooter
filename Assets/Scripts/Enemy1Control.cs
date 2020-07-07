using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy1Control : MonoBehaviour
{
    private int velocity = 5;

    void Update()
    {
        transform.position += new Vector3(Time.deltaTime * velocity, 0, 0);
        PositionCheck();
    }

    private void PositionCheck()
    {
        if(transform.position.x < -11 || transform.position.x > 11)
        {
            Destroy(gameObject);
        }
    }

}
