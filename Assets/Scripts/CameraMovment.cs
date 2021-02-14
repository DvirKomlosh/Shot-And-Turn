using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovment : MonoBehaviour
{
    public Transform Player;
    public float distance;
    public float yOffset;
    public Vector3 CameraAngle;
    float a;
    Vector3 rotatedVec;
    // Update is called once per frame
    void Update()
    {
        a = -Player.localEulerAngles.y * Mathf.Deg2Rad;
        rotatedVec = new Vector3(Mathf.Cos(a), 0 , Mathf.Sin(a));
        Vector3 heightVec = new Vector3(0, yOffset, 0);

        transform.position = Player.position - distance*rotatedVec+heightVec;
        transform.rotation = Player.rotation * Quaternion.Euler(CameraAngle);
    }
}
