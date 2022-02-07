using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Transform toungePoint;
    public float toungeLength;
    public LayerMask canHit;

    public float speed;
    public bool isMoving;

    public LineRenderer lr;
    public float toungeSpeed;

    public GameManager gameManager;

    private void Update()
    {
        RaycastHit2D tongue = Physics2D.Raycast(toungePoint.position, transform.up, toungeLength, canHit);

        Debug.DrawLine(toungePoint.position, tongue.point, Color.red);

        if ((Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space)) && isMoving == false && tongue.point != Vector2.zero)
        {
            StartCoroutine("Move", tongue.point);
            
            if (tongue.collider.CompareTag("Fly"))
            {
                tongue.collider.GetComponent<Fly>().moveSpeed = 0f;
            }
        }

        lr.SetPosition(0, toungePoint.position);
    }

    private void FixedUpdate()
    {
        if (isMoving == false)
        {
            Vector3 difference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;

            difference.Normalize();

            float rotation = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg - 90f;

            transform.rotation = Quaternion.Euler(0f, 0f, rotation);
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.layer == 10)
        {
            gameManager.Win();
        }
    }

    IEnumerator Move(Vector2 destination)
    {
        isMoving = true;

        if (Vector2.Distance(transform.position, destination) > .5f)
        {
            while (lr.GetPosition(1).y != destination.y)
            {
                lr.SetPosition(1, Vector2.MoveTowards(lr.GetPosition(1), destination, toungeSpeed * Time.deltaTime));

                yield return null;
            }

            FindObjectOfType<AudioManager>().Play("Tongue");

            while ((Vector2)transform.position != destination)
            {
                transform.position = Vector2.MoveTowards(transform.position, destination, speed * Time.deltaTime);

                yield return null;
            }
        }

        isMoving = false;
    }
}
