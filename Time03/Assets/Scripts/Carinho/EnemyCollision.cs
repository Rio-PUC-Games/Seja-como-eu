﻿﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCollision : MonoBehaviour
{
    public float knockbackStrenght = 20.0f;
    public float knockbackHeight = 1.0f;

    public GameObject deathScreen;

    // Start is called before the first frame update
    void Start()
    {
        deathScreen.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        Rigidbody rb = collision.collider.GetComponent<Rigidbody>();

        if (rb != null && collision.collider.CompareTag("enemy"))
        {
            if (gameObject.GetComponent<MovimentPlayer>().dashing)
            {
                Vector3 dir = collision.transform.position - transform.position;
                dir.y = knockbackHeight;
                rb.AddForce(dir.normalized * knockbackStrenght, ForceMode.Impulse);
            }
            else
            {
                deathScreen.SetActive(true);
            }
        }
        if(collision.collider.CompareTag("rock"))
        {
            if(gameObject.GetComponent<MovimentPlayer>().dashing)
            {
                Vector3 dir = collision.transform.position - transform.position;
                rb.AddForce(dir.normalized * knockbackStrenght, ForceMode.Impulse);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("orb"))
        {
            deathScreen.SetActive(true);
        }
    }
}