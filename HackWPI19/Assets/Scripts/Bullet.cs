using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    private const float speed = 20F;

    private float xVel;
    private float yVel;

    //Animation
    private Texture2D spr;
    private Texture2D[] sprites; //Array of sprites
    private int frame = 0;
    private float waitTime = 0.5f;

    public Rect rect;

    void Start() {
        //Movement
        transform.localScale = new Vector3(3F, 3F, 0);

        Vector3 screenPos = Camera.main.WorldToScreenPoint(transform.position);
        float x = Input.mousePosition.x - screenPos.x;
        float y = Input.mousePosition.y - screenPos.y;
        float angle = Mathf.Atan2(y, x);
        xVel = speed * Mathf.Cos(angle);
        yVel = speed * Mathf.Sin(angle);

        transform.localRotation = Quaternion.Euler(0, 0, angle * 180 / Mathf.PI);

        //Sprite Frames
        //sprites[0] = Resources.Load<Texture2D>("../Sprites/bullet_1.png");
        //sprites[1] = Resources.Load<Texture2D>("../Sprites/bullet_2.png");
        //StartCoroutine("SwitchSprite");
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
    /*
    void OnGUI()
    {
        OnGUI.DrawTexture(new Rect(10, 10, 60, 60), spr, ScaleMode.ScaleToFit, true, 10.0F);
    }

    private IEnumerator SwitchSprite()
    {
        spr = sprites[frame];
        frame++;
        if (frame > sprites.Length)
            frame = 0;

        yield return new WaitForSeconds(waitTime);
        StartCoroutine("SwitchSprite");
    }
    */
}
