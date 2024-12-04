using UnityEngine;

public class Chaser : MonoBehaviour
{
    public Transform player;
    public float moveSpeed = .5f; 
    public float detectionAngle = 45f;
    public float moveCooldown = 0f; 
    public float turnSpeed = 20f;
    public GameEnding gameEnding;

    Animator m_Animator;
    Rigidbody m_Rigidbody;
    AudioSource m_AudioSource;
    Vector3 m_Movement;
    Quaternion m_Rotation = Quaternion.identity;

    private float lastMoveTime = 0f; 
    private bool isPlayerLooking = false; 
    void Start()
    {
        m_Animator = GetComponent<Animator> ();
        m_Rigidbody = GetComponent<Rigidbody> ();
        m_AudioSource = GetComponent<AudioSource> ();

    }



    void Update()
    {
        isPlayerLooking = IsPlayerLookingAtEnemy();

        if (!isPlayerLooking && Time.time - lastMoveTime > moveCooldown)
        {
            MoveEnemyTowardsPlayer();

        }
        
        if (Vector3.Distance(transform.position, player.position) < 1f)
        {
            gameEnding.CaughtPlayer ();

        }
    }


    bool IsPlayerLookingAtEnemy()
    {
        Vector3 directionToEnemy = transform.position - player.position;
        float angle = Vector3.Angle(player.forward, directionToEnemy);
        
        return angle < detectionAngle; 
    }

    void MoveEnemyTowardsPlayer()
    {
        Vector3 directionToPlayer = (player.position - transform.position).normalized;

        transform.position += directionToPlayer * moveSpeed * Time.deltaTime;

        lastMoveTime = Time.time;
    }

    void PlayerDeath()
    {
        gameEnding.CaughtPlayer ();
    }
}
