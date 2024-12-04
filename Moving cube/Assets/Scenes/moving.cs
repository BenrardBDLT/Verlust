using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moving : MonoBehaviour
{
    public float speed = .05f;

    // Update is called once per frame
    void Update()
    {
        float xDir = Input.GetAxis("Horizontal"); 
        float yDir = Input.GetAxis("Vertical");

        Vector3 moving = new Vector3(xDir, 0.0f, yDir);
        transform.position += moving * speed;
    }
}
