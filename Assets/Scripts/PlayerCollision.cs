using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    public event System.Action OnPlayerDeath ;
    void OnCollisionEnter(Collision collisionInfo) 
    {
        if (collisionInfo.collider.tag == "Obstacle") 
        {
            if (OnPlayerDeath != null)
                OnPlayerDeath();
            gameObject.SetActive(false);
        }
    }
}
