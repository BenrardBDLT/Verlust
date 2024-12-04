using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moving : MonoBehaviour
{
    public float speed = .05f;// init la vitesse du cube
    public float sensitivityX = 10f;  // Sensibilité pour la rotation autour de l'axe X (horizontal)
    public float sensitivityY = 10f;  // Sensibilité pour la rotation autour de l'axe Y (vertical)

    private float rotationX = 0f;     // Stocke la rotation autour de l'axe X
    private float rotationY = 0f;     // Stocke la rotation autour de l'axe Y

    // Update is called once per frame
    void Update()
    {
        float xDir = Input.GetAxis("Horizontal"); 
        float yDir = Input.GetAxis("Vertical");

        Vector3 moving = new Vector3(xDir, 0.0f, yDir);
        transform.position += moving * speed;


        // Récupère le mouvement de la souris
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");

        // Calcule la nouvelle rotation
        rotationX += mouseX * sensitivityX;
        rotationY -= mouseY * sensitivityY;

        // Limite la rotation sur l'axe Y pour éviter une rotation trop extrême
        rotationY = Mathf.Clamp(rotationY, -90f, 90f);

        // Applique la rotation au cube
        transform.rotation = Quaternion.Euler(rotationY, rotationX, 0f);
    }
}
