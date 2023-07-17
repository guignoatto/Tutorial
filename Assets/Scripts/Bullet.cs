using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEditor.Rendering;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public GameObject bulletParticleImpact;
    private float _damage = 10;
    private float _speed;
    private Vector2 _direction;
    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void Initialize(Vector2 direction, float speed)
    {
        this._direction = direction;
        this._speed = speed;

        LaunchProjectile();
    }

    private void LaunchProjectile()
    {
        rb.velocity = _direction.normalized * _speed * BuffStats.bulletSpeedBuff;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.TryGetComponent(out EnemyBase enemy))
        {
            enemy.TakeDamage(Damage);

            Destroy(Instantiate(bulletParticleImpact, transform.position, Quaternion.identity), 3);
            Destroy(this.gameObject);
        }
    }

    public float Damage { get => _damage; set => _damage = value; }
}
