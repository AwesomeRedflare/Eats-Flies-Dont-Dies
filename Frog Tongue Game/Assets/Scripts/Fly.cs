using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fly : MonoBehaviour
{
    private Rigidbody2D rb;
    private bool start = true;

    public float speed;
    public float moveSpeed;

    public float minSpeed;
    public float maxSpeed;

    public GameObject deathEffect;

    private void Start()
    {
        transform.localScale = new Vector3(0f, 0f, 0f);
        rb = GetComponent<Rigidbody2D>();
        transform.eulerAngles = new Vector3(0f, 0f, Random.Range(0f, 360f));
        moveSpeed = moveSpeed + Random.Range(minSpeed, maxSpeed);

        StartCoroutine("Move");
    }

    private void Update()
    {
        if (start == false)
        {
            rb.velocity = transform.up * moveSpeed * Time.deltaTime;
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            FindObjectOfType<AudioManager>().Play("Fly");
            FindObjectOfType<AudioManager>().Play("Pickup");
            Instantiate(deathEffect, transform.position, Quaternion.identity);
            Destroy(gameObject);
            GameManager.score += 10;
        }

        if (col.CompareTag("Destroy"))
        {
            Destroy(gameObject);
        }
    }

    IEnumerator Move()
    {
        while(transform.localScale.x < 1)
        {
            transform.localScale = Vector3.MoveTowards(transform.localScale, new Vector3(1f, 1f, 1f), speed * Time.deltaTime);
            yield return null;
        }
        start = false;
    }
}
