using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float speed = 2f; 
    private int direction = -1; 

    void Update()
    {
        
        transform.Translate(new Vector2(direction * speed * Time.deltaTime, 0));
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        
        if (collision.gameObject.CompareTag("Obstaculo"))
        {
            
            direction *= -1;
        }
    }
}
