using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundLoof : MonoBehaviour
{
    private float width;

    // Start is called before the first frame update
    void Awake()
    {
        width = GetComponent<BoxCollider2D>().size.x * transform.parent.localScale.y;
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.x <= -width)
        {
            Vector2 offset = new Vector2(width * 2f, 0);
            transform.position = (Vector2)transform.position + offset;
        }
    }
}
