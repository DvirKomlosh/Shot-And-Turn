using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(GameManger))]
public class Enemy : MonoBehaviour, IDamageable
{
    public int level = 1 ;
    public int health;
    public float size;
    public float speed;
    public float rotationSpeed;

    Transform target;
    
    GameManger gameManger;

    public Enemy(int Health, float Size, float Speed, float RotationSpeed)
    {
        health = Health;
        size = Size;
        speed = Speed;
        rotationSpeed = RotationSpeed;
    }

    public void setHealth(int Health)
    {
        health = Health;
    }

    public void setLevel(int Level)
    {
        level = Mathf.Max(Level,1);
    }

    public void setSpeed(float Speed)
    {
        speed = Speed;
    }

    public void setSize(float Size)
    {
        size = Size;
    }

    public void setRotationSpeed(float RotationSpeed)
    {
        rotationSpeed = RotationSpeed;
    }
    
    // Start is called before the first frame update
    void Start()
    {

        gameManger = FindObjectOfType<GameManger>();
        if (0 < size && size < 10)
            transform.localScale = new Vector3(size, 1, size);
        
        if(GameObject.FindGameObjectWithTag("GAMER")!=null)
            target = GameObject.FindGameObjectWithTag("GAMER").transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (health <= 0) 
        {
            gameManger.addScore(level);
            GameObject.Destroy(gameObject);
        }
            
        if (target != null)
        {
            Vector3 dir = target.position - transform.position;
            Quaternion lookRotation = Quaternion.LookRotation(dir);
            Vector3 rotation = Quaternion.Lerp(transform.rotation, lookRotation, Time.deltaTime * rotationSpeed).eulerAngles;

            //rotate
            transform.rotation = Quaternion.Euler(0f, rotation.y, 0f);

            //move
            Vector3 localForward = transform.InverseTransformDirection(Vector3.forward);


            dir = transform.position - target.position;
            dir.z = -dir.z;
            
            if (Vector3.Dot(dir.normalized, localForward) > 0.7f)
                transform.Translate(Vector3.forward * Time.deltaTime * speed);
        }
        
        
    }

    public void TakeHit(int demage, RaycastHit hit)
    {
        health -= demage;
    }


}
