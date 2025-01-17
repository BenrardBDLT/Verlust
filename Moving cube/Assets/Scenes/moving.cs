using System.Collections;
using UnityEngine;
using Mirror;

public class moving : NetworkBehaviour
{
    public Transform cameraTransform;
    public float speed = 0.1f;
    public float ratiospeed = 10;
    public float jumpForce = 100f;
    
    private bool isGrounded = true;
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        if (rb == null)
        {
            Debug.LogError("Aucun Rigidbody trouvé sur l'objet !");
        }

        // Disable movement controls for non-local players
        if (!isLocalPlayer)
        {
            // Optionally disable components if they're not needed for remote players
            cameraTransform.gameObject.SetActive(false);
            enabled = false;
        }
    }

    void Update()
    {
        if (!isLocalPlayer) return;

        float xDir = Input.GetAxis("Horizontal");
        float zDir = Input.GetAxis("Vertical");

        if (zDir != 0 || xDir != 0)
        {
            Vector3 cameraForward = cameraTransform.forward;
            cameraForward.y = 0f;
            cameraForward.Normalize();

            Vector3 movementDirection = cameraForward * zDir + cameraTransform.right * xDir;
            movementDirection.Normalize();

            if (movementDirection != Vector3.zero)
            {
                Quaternion targetRotation = Quaternion.LookRotation(movementDirection);
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, 10f * Time.deltaTime);
            }

            transform.position += movementDirection * speed * ratiospeed;
        }

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isGrounded = false;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }
}
