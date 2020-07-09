using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy1Control : MonoBehaviour
{
    private int velocity = 5;
    private int hp = 2;
    private SpriteRenderer spriteRenderer;
    private Transform player;
    public Sprite deadSprite;
    private CameraControl cameraControl;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        cameraControl = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraControl>();
    }

    void Update()
    {
        if(hp <= 0)
        {
            this.enabled = false;
            StartCoroutine(Die());
        }
        //transform.position += new Vector3(Time.deltaTime * velocity, 0, 0);
        //PositionCheck();
        if(Vector2.Distance(transform.position,player.position) < 10 && hp > 0)
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
        StartCoroutine(cameraControl.Shake(0.1f,0.15f));
    }

    IEnumerator Die()
    {
        GetComponent<BoxCollider2D>().enabled = false;
        GameControl.score++;
        spriteRenderer.color = new Color(1, 1, 1, 150/255f);
        transform.localScale = new Vector2(0.15f, 0.15f);
        yield return new WaitForSeconds(0.25f);
        spriteRenderer.color = new Color(1, 1, 1, 1);
        transform.localScale = new Vector2(0.1f, 0.1f);
        yield return new WaitForSeconds(0.25f);
        spriteRenderer.color = new Color(1, 1, 1, 150 / 255f);
        transform.localScale = new Vector2(0.15f, 0.15f);
        yield return new WaitForSeconds(0.25f);
        spriteRenderer.color = new Color(1, 1, 1, 1);
        transform.localScale = new Vector2(0.1f, 0.1f);
        yield return new WaitForSeconds(0.25f);
        spriteRenderer.sprite = deadSprite;
        yield return new WaitForSeconds(0.35f);
        Destroy(transform.gameObject);
    }

}
