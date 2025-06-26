using System.Collections.Generic;
using UnityEngine;

public class ShootController : MonoBehaviour
{
    [SerializeField] Bullet bulletPref;
    [SerializeField] int maxPool;
    List<Bullet> bulletPool = new List<Bullet>();
    int bulletCount;
    [HideInInspector] public bool isOnCooldown;
    float cooldown;
    float startTime;
    private void Update()
    {
        if(isOnCooldown)
        {
            float time = Time.time - startTime;
            if(time >= cooldown)
            {
                isOnCooldown = false;
            }
        }
    }

    public void Shoot(Transform spawnPosRef, float cooldown)
    {
        if (isOnCooldown) { return; }
        this.cooldown = cooldown;

        if(bulletPool.Count < maxPool)
        {
            bulletPool.Add(Instantiate(bulletPref, spawnPosRef.position, spawnPosRef.rotation));
        }
        else
        {
            bulletPool[bulletCount].ResetBullet(spawnPosRef.position, spawnPosRef.rotation);
            bulletCount++;
            if (bulletCount >= maxPool) bulletCount = 0;
        }
        startTime = Time.time;
        isOnCooldown = true;
    }
}
