using JetBrains.Annotations;
using Mono.Cecil;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SpiralShooting : MonoBehaviour
{
    public Transform player;
    public float fireRate = 0.1f;
    public GameObject bulletPrefab;
    public Transform firePoint;
    public float bulletSpeed = 10f;
    public int bulletCount = 10;
    public float spiralSize = 2f;
    public float spiralSpeed = 2f;
    public float timeBetweenSpirals = 0;
    public float bulletLifetime = 1;
    List<IEnumerator> currentPattern;

    private void Start()
    {
        StartCoroutine(SpiralShoot());
    }

    private IEnumerator SpiralShoot()
    {
        float angleStep = 360f / bulletCount;
        float angle = 0f;

        while (true)
        {
            for (int i = 0; i < bulletCount; i++)
            {
                float radian = angle * Mathf.Deg2Rad;
                Vector3 direction = new Vector3(Mathf.Cos(radian), Mathf.Sin(radian), 0f);
                GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);
                bullet.GetComponent<Rigidbody2D>().velocity = direction * bulletSpeed;
                Destroy(bullet, bulletLifetime);
                angle += angleStep;
                yield return new WaitForSeconds(fireRate);
            }
            angle += spiralSpeed;
            yield return new WaitForSeconds(timeBetweenSpirals);
        }
    }
}
