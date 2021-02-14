using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public LayerMask collisionMask;
    public float speed;
    public Vector3 size;
    public int damage;
    
    //int piercing;


    public void SetSpeed(int Speed) 
    {
        speed = Speed;
    }
    public void SetActive() 
    {
        gameObject.SetActive(true);
    }

    void Update() 
    {
        if(transform.position.x<-16 || transform.position.x > 16 || transform.position.z < -16 || transform.position.z > 16)
            GameObject.Destroy(gameObject);
    }

    void FixedUpdate ()
    {
        float distance = speed * Time.deltaTime;
        CollisionCheck(distance);
        transform.Translate(Vector3.right * distance);
    }

    void CollisionCheck(float moveDistance) 
    {
        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, moveDistance, collisionMask, QueryTriggerInteraction.Collide)) 
        {
            onHitObject(hit);
        }
    }

    void onHitObject(RaycastHit hit) 
    {
        IDamageable damageableObject = hit.collider.GetComponent<IDamageable>();
        if (damageableObject != null) 
        {
            damageableObject.TakeHit(damage , hit);
        }
        GameObject.Destroy(gameObject);
    }
    

    
}
