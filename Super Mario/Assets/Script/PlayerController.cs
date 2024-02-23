using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 5f;
    public float jumpForce = 10f;
    public AudioClip jumpSound;
    public AudioClip takeSound;
    public GameObject crushedEnemyPrefab;
    private Rigidbody2D rb;
    private bool isGrounded;
    private AudioSource audioSource;
    private Animator aPlayer;
    public AnimatorOverrideController marioGrande;
    private RuntimeAnimatorController marioChiquito;
    private bool marioesGrande = false;
    private bool isDead = false;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        rb = GetComponent<Rigidbody2D>();
        aPlayer = GetComponent<Animator>();
        marioChiquito = aPlayer.runtimeAnimatorController;
        
    }

    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(horizontalInput * speed, rb.velocity.y);
        aPlayer.SetFloat("Velocidad X", Mathf.Abs(rb.velocity.x));
        aPlayer.SetFloat("Velocidad Y", rb.velocity.y);
        aPlayer.SetBool("isGrounded", isGrounded);
       

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);

            if (jumpSound != null)
                audioSource.PlayOneShot(jumpSound);

            isGrounded = false;
        }

        if (horizontalInput < 0)
            transform.localScale = new Vector3(-5, 5, 1);
        else if (horizontalInput > 0)
            transform.localScale = new Vector3(5, 5, 1);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }

        if (collision.gameObject.CompareTag("Enemy"))
        {
            ContactPoint2D contact = collision.contacts[0];
            if (contact.normal.y > 0.9f)
            {
                GameObject crushedEnemy = Instantiate(crushedEnemyPrefab, collision.transform.position, Quaternion.identity);
                Destroy(collision.gameObject);
                Destroy(crushedEnemy, 1f);
            }
            if (marioesGrande)
            {
                aPlayer.runtimeAnimatorController = marioChiquito;
            }
            else
            {
                aPlayer.SetBool("isDead",isDead);
                transform.localPosition = new Vector3(0, 3, 1);
            }
        }

        if (collision.gameObject.CompareTag("Obstaculo"))
        {
            isGrounded = true;
        }

        if (collision.gameObject.CompareTag("Seta"))
        {
            if (marioGrande != null)
            {
                audioSource.PlayOneShot(takeSound);
                aPlayer.runtimeAnimatorController = marioGrande as RuntimeAnimatorController;
                Destroy(collision.gameObject);
                marioesGrande = true;
            }
        }
    }
}
