using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Data
{
    public int highScore;
    public int gems;

    public int bulletNum;
    public int bulletSpeed;
    public int turnSpeed;
    public int movementSpeed;


    public Data(int HighScore, int Gems, int BulletNum, int BulletSpeed, int TurnSpeed, int MovementSpeed) 
    {
        highScore = HighScore;
        gems = Gems;
        bulletNum = BulletNum;
        bulletSpeed = BulletSpeed;
        turnSpeed = TurnSpeed;
        movementSpeed = MovementSpeed;
    }



}
