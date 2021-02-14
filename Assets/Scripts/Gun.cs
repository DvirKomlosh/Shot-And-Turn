using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    static int[] bulletNumAmaount = new[] { 1, 2, 3, 4, 5, 6, 7, 8 };
    static int[] bulletSpeedAmaount = new[] { 50, 70, 90, 110, 130, 150, 175, 200 };

    public Transform muzzle;
    public float rechargeTimeMS;
    float nextShotTime;

    public Bullet bullet;
    public int bulletNum;
    public int bulletSpeed;

    public void SetBulletNum(int BulletNum, int BulletSpeed)
    {
        bulletNum = bulletNumAmaount[BulletNum];
        bulletSpeed = bulletSpeedAmaount[BulletSpeed];
    }


    public void shoot()
    {
        if (nextShotTime < Time.time)
        {
            nextShotTime += (rechargeTimeMS/1000) * bulletNum * 1.5f;
            StartCoroutine(shooter());
        }
    }

    IEnumerator shooter() 
    {
        for (int i = 0; i < bulletNum; i++) 
        {
            Bullet newBullet = Instantiate(bullet, muzzle.position, muzzle.rotation);
            newBullet.SetSpeed(bulletSpeed);
            newBullet.SetActive();
            yield return new WaitForSeconds(rechargeTimeMS / 1000);
        }
    }

    public void SetActive()
    {
        gameObject.SetActive(true);
    }
}
