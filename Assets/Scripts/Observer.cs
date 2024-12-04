using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Observer : MonoBehaviour
{
    //Detects the players transform(position)
    public Transform player;
    public GameEnding gameEnding;

    bool m_IsPlayerInRange;

    //Player enter range
    void OnTriggerEnter (Collider other)
    {
       if(other.transform == player)
       {
        m_IsPlayerInRange = true;
       }
        
    }


    //Player exit range
  void OnTriggerExit (Collider other)
    {
        if (other.transform == player)
        {
            m_IsPlayerInRange = false;
        }
    }

   void Update ()
    {
        //Line of sight check
        if (m_IsPlayerInRange)
        {
            Vector3 direction = player.position - transform.position + Vector3.up;
            Ray ray = new Ray(transform.position, direction);
            RaycastHit raycastHit;

            if(Physics.Raycast(ray, out raycastHit))
            {
                if (raycastHit.collider.transform == player)
                {
                    gameEnding.CaughtPlayer ();
                }
            }
        }
    }
}