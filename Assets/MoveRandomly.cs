using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveRandomly : MonoBehaviour
{
    public float moveSpeed;
    public Rigidbody2D rb;
    public float timeLeft;
    Vector2 movement;
    public float accelerationTime = 4f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timeLeft -= Time.deltaTime;
        if (timeLeft <= 0)
        {
            movement = new Vector2(UnityEngine.Random.Range(-1f, 1f), UnityEngine.Random.Range(-1f, 1f));
            timeLeft += accelerationTime;
        }

    }

    private void FixedUpdate()
    {
        if (GameData.gameRunning)
            rb.AddForce(movement * moveSpeed, ForceMode2D.Impulse);
        else
            GetComponent<Rigidbody2D>().velocity = Vector2.zero;
    }
}
