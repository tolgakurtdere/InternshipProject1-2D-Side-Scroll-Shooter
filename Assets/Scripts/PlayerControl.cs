using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;
using UnityEngine.Experimental.UIElements;
using UnityEngine.SceneManagement;

public class PlayerControl : MonoBehaviour
{
    public Sprite[] idleSprites, runSprites, jumpSprites;
    int idleSpriteIndex = 0, runSpritesIndex = 0;
    SpriteRenderer spriteRenderer;
    Rigidbody2D rigidbody_player;
    bool canJump = true;
    float horizontalInput = 0;
    float idleAnimationTime = 0;
    float fireTime = 0;
    public GameObject bulletPrefab;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        rigidbody_player = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        fireTime += Time.deltaTime;
        if ((Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow)) && canJump)
        {
            rigidbody_player.AddForce(new Vector2(0, 425));
            canJump = false;
        }
        if (Input.GetButtonDown("Fire1"))
        {
            Fire();
        }
    }
    private void FixedUpdate()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        rigidbody_player.velocity = new Vector3(horizontalInput * 5, rigidbody_player.velocity.y, 0);

        characterAnimation();
    }

    void characterAnimation()
    {
        if (canJump) //if player is not jumping now
        {
            if (horizontalInput == 0) //idle
            {
                idleAnimationTime += Time.deltaTime;
                if (idleAnimationTime > 0.05f)
                {
                    idleAnimationTime = 0;

                    spriteRenderer.sprite = idleSprites[idleSpriteIndex++];
                    if (idleSpriteIndex == idleSprites.Length) idleSpriteIndex = 0;
                }

            }
            else if (horizontalInput < 0) //move left
            {
                transform.localScale = new Vector3(-1, 1, 1);
                spriteRenderer.sprite = runSprites[runSpritesIndex++];
                if (runSpritesIndex == runSprites.Length) runSpritesIndex = 0;
            }
            else if (horizontalInput > 0) //move right
            {
                transform.localScale = new Vector3(1, 1, 1);
                spriteRenderer.sprite = runSprites[runSpritesIndex++];
                if (runSpritesIndex == runSprites.Length) runSpritesIndex = 0;
            }
        }
        else //if player is jumping now
        {
            if (rigidbody_player.velocity.y > 0) //if going up
            {
                spriteRenderer.sprite = jumpSprites[0];
            }
            else //if going down
            {
                spriteRenderer.sprite = jumpSprites[1];
            }
            if (horizontalInput < 0) transform.localScale = new Vector3(-1, 1, 1);
            else if (horizontalInput > 0) transform.localScale = new Vector3(1, 1, 1);
        }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        canJump = true;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "EnemyTag")
        {
            Destroy(transform.gameObject);
            GameOver();
        }
    }

    private void Fire() //create bullet according to mousePosition
    {
        if (fireTime > 0.25f)
        {
            fireTime = 0;
            Vector3 worldMousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 direction = (Vector2)((worldMousePos - transform.position));
            direction.Normalize();

            GameObject bullet = (GameObject)Instantiate(bulletPrefab, transform.position, Quaternion.identity);
            bullet.GetComponent<Rigidbody2D>().velocity = direction * 15;
        }
    }

    private void GameOver()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
