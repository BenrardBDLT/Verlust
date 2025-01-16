using UnityEngine;
using UnityEngine.AI;

public class Ennemy_behavior : MonoBehaviour
{
    public float detectionRadius = 10f; // Rayon de détection
    public float stoppingDistance = 1.5f; // Distance minimale pour arrêter de suivre
    private Vector3 initialPosition; // Position initiale de l'ennemi
    private NavMeshAgent agent; // Référence au NavMeshAgent
    private GameObject player; // Référence dynamique au joueur
    private bool isPlayerDetected = false; // Indique si le joueur est détecté

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        initialPosition = transform.position; // Sauvegarde la position initiale

        // Recherche dynamique du joueur avec le tag "Player"
        player = GameObject.FindGameObjectWithTag("Player");
        if (player == null)
        {
            Debug.LogError("Player not found! Make sure your player prefab has the 'Player' tag.");
        }
    }

    void Update()
    {
        if (player == null) return; // Sort si le joueur n'est pas encore trouvé

        float distanceToPlayer = Vector3.Distance(player.transform.position, transform.position);

        if (distanceToPlayer <= detectionRadius)
        {
            isPlayerDetected = true;
            if (distanceToPlayer > stoppingDistance)
            {
                agent.SetDestination(player.transform.position); // Suit le joueur
            }
            else
            {
                agent.ResetPath(); // Arrête le mouvement lorsqu'il est proche
            }
        }
        else
        {
            if (isPlayerDetected)
            {
                isPlayerDetected = false;
                agent.SetDestination(initialPosition); // Retourne à sa position d'origine
            }
            else if (Vector3.Distance(transform.position, initialPosition) < 0.1f)
            {
                agent.ResetPath(); // Arrête le mouvement une fois de retour
            }
        }
    }

    void OnDrawGizmosSelected()
    {
        // Dessine une sphère pour visualiser le rayon de détection
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
    }
}