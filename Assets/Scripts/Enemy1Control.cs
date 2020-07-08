using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy1Control : MonoBehaviour
{
    private int velocity = 5;
    private int hp = 2;
    private SpriteRenderer spriteRenderer;
    private Transform player;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    void Update()
    {
        if(hp <= 0)
        {
            Destroy(transform.gameObject);
            GameControl.score++;
        }
        //transform.position += new Vector3(Time.deltaTime * velocity, 0, 0);
        //PositionCheck();
        if(Vector2.Distance(transform.position,player.position) < 10)
        {
            transform.position = Vector2.MoveTowards(transform.position, player.position, velocity * Time.deltaTime);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "BulletTag")
        {
            Destroy(collision.transform.gameObject);
            TakeDamage();
        }
    }

    /*private void PositionCheck()
    {
        if(transform.position.x < -11 || transform.position.x > 11)
        {
            Destroy(gameObject);
        }
    }*/

    public void TakeDamage()
    {
        hp--;
        spriteRenderer.color = new Color(1, spriteRenderer.color.g-55 / 255f, 1, 1);
    }

}
