using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public GameObject bulletPrefab;
    public float bulletLifeTime = 3f;
    public Transform firePoint;
    public float bulletSpeed = 10f;

    public float moveSpeed = 5f; 
    private Rigidbody2D rb; 

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>(); 
    }

    private void Update()
    {

        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 lookDir = mousePos - transform.position;
        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, angle - 90f));

        if (Input.GetMouseButtonDown(0))
        {
            var bullet = Instantiate(bulletPrefab, firePoint.position, transform.rotation).GetComponent<Bullet>();
            bullet.Initialize(lookDir, bulletSpeed);
            Destroy(bullet.gameObject, bulletLifeTime);
        }
    }

    private void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxisRaw("Horizontal");
        float moveVertical = Input.GetAxisRaw("Vertical");
        Vector2 movement = new Vector2(moveHorizontal, moveVertical);
        movement = ((movement.normalized * moveSpeed) * BuffStats.playerSpeedBuff) * Time.fixedDeltaTime;
        rb.MovePosition(rb.position + movement);
    }
}
