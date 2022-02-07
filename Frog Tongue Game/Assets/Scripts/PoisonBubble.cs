using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoisonBubble : MonoBehaviour
{
    private Rigidbody2D rb;
    public float speed;
    public float moveSpeed;

    public GameObject deathEffect;

    private void Start()
    {
        GetComponent<CircleCollider2D>().enabled = false;
        transform.localScale = new Vector3(0f, 0f, 0f);
        rb = GetComponent<Rigidbody2D>();
        transform.eulerAngles = new Vector3(0f, 0f, Random.Range(0f, 360f));

        StartCoroutine("Move");
    }

    private void Update()
    {

            rb.velocity = transform.up * moveSpeed * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            FindObjectOfType<AudioManager>().Play("Hurt");

            Instantiate(deathEffect, transform.position, Quaternion.identity);

            Destroy(gameObject);

            GameManager.health -= 1;

            FindObjectOfType<GameManager>().CheckHealth();
        }

        if (col.CompareTag("Destroy"))
        {
            Destroy(gameObject);
        }
    }

    IEnumerator Move()
    {
        while (transform.localScale.x < 1)
        {
            transform.localScale = Vector3.MoveTowards(transform.localScale, new Vector3(1f, 1f, 1f), speed * Time.deltaTime);
            yield return null;
        }
        GetComponent<CircleCollider2D>().enabled = true;
    }
}
