using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    private const float speed = 500F;

    private float xVel;
    private float yVel;

    void Start() {
        //Movement
        transform.localScale = new Vector3(50F, 50F, 0);

        Vector3 screenPos = Camera.main.WorldToScreenPoint(transform.position);
        float x = Input.mousePosition.x - screenPos.x;
        float y = Input.mousePosition.y - screenPos.y;
        float angle = Mathf.Atan2(y, x);
        xVel = speed * Mathf.Cos(angle);
        yVel = speed * Mathf.Sin(angle);

        transform.localRotation = Quaternion.Euler(0, 0, angle * 180 / Mathf.PI);
    }

    void Update() {

        //Movement
        Vector3 pos = transform.position;
        pos.x += Time.deltaTime * xVel;
        pos.y += Time.deltaTime * yVel;
        transform.position = pos;

    }

    void OnBecameInvisible()
    {
        Destroy(gameObject);
    }

    void OnCollisionEnter()
    {
        Destroy(gameObject);
    }
}
