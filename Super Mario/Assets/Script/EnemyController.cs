using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float speed = 2f; 
    public bool vaIzquierda;
    public float direction = -1;
    void Update()
    {
        
        transform.Translate(new Vector2(direction * speed * Time.deltaTime, 0));
    }

    void OnCollisionEnter ( Collision collision)
    {

        if (collision.gameObject.CompareTag("Obstaculo"))
        {
            direction = 1;
            vaIzquierda = false;

        }
        

    }
}

