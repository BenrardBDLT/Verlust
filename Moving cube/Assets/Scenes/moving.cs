using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moving : MonoBehaviour
{
    //defini la camera pour que le cube suive le momuvement de la camera orbitale
    public Transform cameraTransform;
    public float speed = 0.1f; // init la vitesse du cube
    public float ratiospeed = 10; // ratio vitesse ( a changer en mode dev pour aller plus ou moins vite)
    public float jumpForce = 100f; // Force du saut
    private bool isGrounded = true;//verifie si le cube est sau sol (empeche jump infini)
    private Rigidbody rb;

    void Start()
        {
            // Récupérer le Rigidbody attaché à l'objet
            rb = GetComponent<Rigidbody>();
            if (rb == null)
            {
                Debug.LogError("Aucun Rigidbody trouvé sur l'objet !");
            }
        }
    // Update is called once per frame
    void Update()
    {
        // Récupérer les entrées utilisateur
        float xDir = Input.GetAxis("Horizontal"); // Mouvement latéral
        float zDir = Input.GetAxis("Vertical");   // Mouvement avant/arrière

        // Ne se déplacer que si une touche est pressée
        if (zDir != 0 || xDir != 0)
        {
            // Calculer la direction de la caméra sur le plan horizontal
            Vector3 cameraForward = cameraTransform.forward;
            cameraForward.y = 0f; // Ignorer l'inclinaison verticale
            cameraForward.Normalize();

            // Calculer la direction du mouvement
            Vector3 movementDirection = cameraForward * zDir + cameraTransform.right * xDir;
            movementDirection.Normalize();

            // Faire pivoter le cube pour qu'il fasse face à la direction du mouvement
            if (movementDirection != Vector3.zero)
            {
                Quaternion targetRotation = Quaternion.LookRotation(movementDirection);
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, 10f * Time.deltaTime);
            }

            // Avancer dans la direction calculée
            transform.position += movementDirection * speed * ratiospeed;
        }
        // Gestion du saut
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isGrounded = false; // Empêcher de sauter plusieurs fois
        }
    }
    // Détection des collisions pour savoir si le cube est au sol
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true; // Le cube touche le sol
        }
    }
}
