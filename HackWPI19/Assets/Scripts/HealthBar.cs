using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour {

    private const float barSize = 0.75f;

    public void setSize(float sizeNormalized) {
        transform.localScale = new Vector3(sizeNormalized, barSize, barSize);
    }

    public void setColor(Color color) {
        transform.GetChild(0).GetChild(0).GetComponent<SpriteRenderer>().color = color;
    }
}
