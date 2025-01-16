using UnityEngine;

public class CameraOrbit : MonoBehaviour
{
    public Transform target; // Le cube que la caméra orbite
    public float distance = 5f; // Distance entre la caméra et le cube
    public float sensitivityX = 10f; // Sensibilité pour la rotation horizontale
    public float sensitivityY = 10f; // Sensibilité pour la rotation verticale
    public float minY = -20f; // Limite minimale pour l'angle vertical
    public float maxY = 80f;  // Limite maximale pour l'angle vertical

    private float rotationX = 0f; // Rotation accumulée sur l'axe horizontal
    private float rotationY = 0f; // Rotation accumulée sur l'axe vertical
    void Start()
    {
        // Initialise les angles de rotation avec la position actuelle de la caméra
        Vector3 angles = transform.eulerAngles;
        rotationX = angles.y;
        rotationY = angles.x;

        // Verrouille le curseur pour une meilleure expérience utilisateur
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        // Récupère le mouvement de la souris
        float mouseX = Input.GetAxis("Mouse X") * sensitivityX ;
        float mouseY = Input.GetAxis("Mouse Y") * sensitivityY ;

        // Calcule la nouvelle rotation
        rotationX += mouseX;
        rotationY -= mouseY;

        // Limite la rotation verticale
        rotationY = Mathf.Clamp(rotationY, minY, maxY);

        // Calcule la position orbitale
        Quaternion rotation = Quaternion.Euler(rotationY, rotationX, 0f);
        Vector3 offset = rotation * new Vector3(0, 0, -distance);

        // Met à jour la position et l'orientation de la caméra
        transform.position = target.position + offset;
        transform.LookAt(target); // Oriente la caméra vers le cube
    }
}
