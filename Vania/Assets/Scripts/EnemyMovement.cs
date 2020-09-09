using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{

    // Config

    [SerializeField] float moveSpeed = 5f;

    // Chached
    Rigidbody2D myRigidBody;

    // Start is called before the first frame update
    void Start()
    {
        myRigidBody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (IsFacingRight()) 
        {
            //transform.Translate(Vector2.right * moveSpeed * Time.deltaTime);
            myRigidBody.velocity = new Vector2(moveSpeed, 0f);
        }
        else
        {
            //transform.Translate(Vector2.left * moveSpeed * Time.deltaTime);
            myRigidBody.velocity = new Vector2(-moveSpeed, 0f);
        }
        
        
    }

    bool IsFacingRight()
    {
        return transform.localScale.x > 0;
    }

    private void OnTriggerExit2D(Collider2D collider)
    {
        bool EnemyHasHorizontalSpeed = Mathf.Abs(myRigidBody.velocity.x) > Mathf.Epsilon;

        transform.localScale = new Vector2(-Mathf.Sign(myRigidBody.velocity.x), 1f);
    }

    
}

