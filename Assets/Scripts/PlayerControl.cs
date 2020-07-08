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
    bool canJump = false;
    float horizontalInput = 0;
    float idleAnimationTime = 0;
    float fireTime = 0,gameOverTime = 0;
    public GameObject bulletPrefab;
    bool isOver = false;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        rigidbody_player = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        fireTime += Time.deltaTime;
        if ((Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow)) && canJump && !isOver)
        {
            rigidbody_player.velocity = Vector2.zero; //to avoid long jump when going uphill
            rigidbody_player.AddForce(new Vector2(0, 425));
            canJump = false;
        }
        if (Input.GetButtonDown("Fire1"))
        {
            if(!isOver) Fire();
        }

        if (transform.position.y < -7) isOver = true;
        GameOver();
    }
    private void FixedUpdate()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        if(!isOver) rigidbody_player.velocity = new Vector3(horizontalInput * 5, rigidbody_player.velocity.y, 0);

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
            isOver = true;
        }
        if (collision.gameObject.tag == "Finish")
        {
            if (SceneManager.GetActiveScene().buildIndex == 3) SceneManager.LoadScene("MainMenu");
            else SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
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
        if (isOver)
        {
            rigidbody_player.gravityScale = 0;
            rigidbody_player.velocity = Vector2.zero;
            spriteRenderer.enabled = false;
            for (int i = 0; i < transform.childCount; i++) //crash body parts
            {
                transform.GetChild(i).transform.gameObject.SetActive(true);
                transform.GetChild(i).GetComponent<Rigidbody2D>().AddForce(new Vector2(Random.Range(-10, 10), Random.Range(-10, 10)));
            }

            gameOverTime += Time.deltaTime;
            if (gameOverTime >= 1f)
            {
                gameOverTime = 0;
                SceneManager.LoadScene("MainMenu");
            }
        }
        
    }
}
