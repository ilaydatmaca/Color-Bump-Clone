using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour
{
    [SerializeField] float thrust = 150f;
    [SerializeField] Rigidbody rb;
    [SerializeField] float wallDistance = 5f;
    [SerializeField] float minCamdistance = 3f;

    private Vector2 lastMousePos;

    // Update is called once per frame
    void Update()
    {
        if (GameManager.singleton.GameEnded)
        {
            return;
        }

        Vector2 deltaPos = Vector2.zero;

        if (Input.GetMouseButton(0))
        {
            if (!GameManager.singleton.GameStarted)
                GameManager.singleton.StartGame();

            Vector2 currentMousePos = Input.mousePosition;
            //Debug.Log(currentMousePos+ " " + transform.position);

            if (lastMousePos == Vector2.zero)
            {
                lastMousePos = currentMousePos;
            }

            deltaPos = currentMousePos - lastMousePos;
            lastMousePos = currentMousePos;

            Vector3 force = new Vector3(deltaPos.x, 0, deltaPos.y) * thrust;
            rb.AddForce(force);

        }

        else
        {
            lastMousePos = Vector2.zero;
        }
    }

    private void FixedUpdate()
    {
        if (GameManager.singleton.GameEnded)
        {
            return;
        }
        if (GameManager.singleton.GameStarted)
        {
            rb.MovePosition(transform.position + Vector3.forward * 5 * Time.fixedDeltaTime);
        }
    }

    private void LateUpdate()
    {
        if (GameManager.singleton.GameEnded)
        {
            return;
        }

        Vector3 pos = transform.position;

        if (transform.position.x < -wallDistance)
        {
            pos.x = -wallDistance;
        }
        if (transform.position.x > wallDistance)
        {
            pos.x = wallDistance;
        }

        if (transform.position.z < Camera.main.transform.position.z + minCamdistance)
        {
            pos.z = Camera.main.transform.position.z + minCamdistance;
        }

        transform.position = pos;
    }

    private void OnCollisionEnter(Collision collision)
    {
        //Debug.Log(collision.ToString());
        if (GameManager.singleton.GameEnded)
        {
            return;
        }

        if (collision.gameObject.tag == "Death")
            GameManager.singleton.EndGame(false);
    }
}
