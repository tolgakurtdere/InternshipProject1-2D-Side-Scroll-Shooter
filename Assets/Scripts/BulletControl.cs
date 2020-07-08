using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletControl : MonoBehaviour
{
    /*private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "EnemyTag")
        {
            Destroy(gameObject);
            FindObjectOfType<Enemy1Control>().TakeDamage();
        }
    }*/
    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "BackgroundTag")
        {
            Destroy(gameObject);
        }
    }
}
