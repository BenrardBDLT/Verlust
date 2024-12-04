using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moving : MonoBehaviour
{
    public float speed = .05f; // init la vitesse du cube
    public float sensitivityX = 0.1f;  // Sensibilité pour la rotation autour de l'axe X (horizontal)
    public float sensitivityY = 0.1f;  // Sensibilité pour la rotation autour de l'axe Y (vertical)

    private float rotationX = 0f;     // Stocke la rotation autour de l'axe X
    private float rotationY = 0f;     // Stocke la rotation autour de l'axe Y

    // Update is called once per frame
    void Update()
    {
        // Déplacement en fonction de la direction où le cube fait face
        float xDir = Input.GetAxis("Horizontal"); 
        float yDir = Input.GetAxis("Vertical");

        Vector3 moving = transform.forward * yDir + transform.right * xDir;
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
