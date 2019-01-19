using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gloup : MonoBehaviour
{

    float speed = 20;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update() {
        GameObject player = GameObject.Find("Astronaut");

        Vector3 position = player.transform.position - this.transform.position;
        position.Normalize();
        Debug.Log("Gloup Position:" + position);


        position *= speed * Time.deltaTime;

        this.transform.position += position;
    

        Debug.Log("Gloup:" + this.transform.position + " " + Time.deltaTime);

        /*Vector3 vectorToTarget = player.transform.position - transform.position;
        float angle = Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg;
        Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
        //transform.rotation = Quaternion.Slerp(transform.rotation, q, Time.deltaTime * dir);
        Debug.Log("playerCoords: " + player.transform.position);*/
    }
}