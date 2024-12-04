using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public float turnSpeed = 20f;
    
    //Creating a vector3 variable called m_Movement
    //Vector3 is what is used to represent a gameobjects position
    //Part 2; Step 5
    Animator m_Animator;
    Rigidbody m_Rigidbody;
    AudioSource m_AudioSource;
    Vector3 m_Movement;
    Quaternion m_Rotation = Quaternion.identity;

    // Start is called before the first frame update
    void Start()
    {
        //Part 2; Step 10
        //Defining m_Animator
        m_Animator = GetComponent<Animator> ();
        m_Rigidbody = GetComponent<Rigidbody> ();
        m_AudioSource = GetComponent<AudioSource> ();
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //Creating horizontal & vertical movement floats
        //Part 2; Step 3
        float horizontal = Input.GetAxis ("Horizontal");
        float vertical = Input.GetAxis ("Vertical");

        //Part 2; Step 6
        //Setting the value for our character's movement
        m_Movement.Set(horizontal, 0f, vertical);
        m_Movement.Normalize ();

        //Part 2; Step 7
        //Bool is either true or false
        //We're setting & checking if horizontal and vertical movement is true or false
        //! inverts bool, making true actually false and vice versa
        //Bottom line is combining horizontal and vertical bools into one
        bool hasHorizontalInput = !Mathf.Approximately (horizontal, 0f);
        bool hasVerticalInput = !Mathf.Approximately (vertical, 0f);
        bool isWalking = hasHorizontalInput || hasVerticalInput;
        m_Animator.SetBool ("IsWalking", isWalking);

        if (isWalking)
        {
            if (!m_AudioSource.isPlaying)
            {
                m_AudioSource.Play();
            }
        }
        else
        {
            m_AudioSource.Stop ();
        }

        //Part 2; Step 11
        //Creates and defines a Vector3 variable called desiredForward
        Vector3 desiredForward = Vector3.RotateTowards (transform.forward, m_Movement, turnSpeed * Time.deltaTime, 0f);
        m_Rotation = Quaternion.LookRotation (desiredForward);

    }

    void OnAnimatorMove ()
    {
        m_Rigidbody.MovePosition (m_Rigidbody.position + m_Movement * m_Animator.deltaPosition.magnitude);
        m_Rigidbody.MoveRotation (m_Rotation);

    }

}
