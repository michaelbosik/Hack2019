using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Background_Stars : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < Screen.height; i+=100)
        {
            for (int j = 0; j < Screen.width; j+=100)
            {
                Debug.Log("pixel");
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
