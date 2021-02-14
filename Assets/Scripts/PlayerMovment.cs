using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof(GunController))]
public class PlayerMovment : MonoBehaviour , IDamageable

{
    static int[] movementSpeedAmaount = new[] { 350 , 375 , 400 , 420 , 440 , 460 ,480 , 500 };
    static int[] turningSpeedAmaount = new[] { 50, 60, 70, 80, 90, 120, 150 ,200};

    public event System.Action OnPlayerDeath;


    public Rigidbody rb;
    public float forwardForce;
    public Vector3 Rotation;
    public float a;
    public Vector3 rotatedVec;
   

    GunController gunController;


    public void SetPlayerMovement(int turnSpeed, int movementSpeed) 
    {
        Rotation.y = turningSpeedAmaount[turnSpeed]; 
        forwardForce = movementSpeedAmaount[movementSpeed];
    }

    // Start is called before the first frame update
    void Start()
    {
        gunController = GetComponent<GunController>();

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        
        a = -transform.localEulerAngles.y*Mathf.Deg2Rad;
        rotatedVec = new Vector3(Mathf.Cos(a), 0, Mathf.Sin(a));

        rb.AddForce(forwardForce * rotatedVec * Time.deltaTime);
    }

    void Update() 
    {
        a = -transform.localEulerAngles.y * Mathf.Deg2Rad;
        rotatedVec = new Vector3(Mathf.Cos(a), 0, Mathf.Sin(a));

        if ((Input.touchCount > 0 && (Input.touches[0].phase == TouchPhase.Began || Input.touches[0].phase == TouchPhase.Moved || Input.touches[0].phase == TouchPhase.Stationary)))
        {
            transform.Rotate(Rotation * Time.deltaTime);
        }
        
        if (Input.touchCount > 0 &&  Input.touches[0].phase == TouchPhase.Ended)
        {
            gunController.shoot();
        }
        

    }

    public void TakeHit(int damage ,RaycastHit hit) 
    {
        if (OnPlayerDeath != null)
            OnPlayerDeath();
        gameObject.SetActive(false);


    }
}
