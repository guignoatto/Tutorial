using System;
using System.Collections;
using UnityEngine;

public class EnemyBase : MonoBehaviour
{
    [SerializeField] private float life = 100;
    [SerializeField] private Material flashMaterial;

    private Transform _player;

    private Material standardMaterial;
    private SpriteRenderer spriteRenderer;
    private Color originalColor;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        _player = GameObject.FindGameObjectWithTag("Player").transform;
        standardMaterial = spriteRenderer.material;
        originalColor = spriteRenderer.color;
    }

    private void Update()
    {
        Vector3 lookDir = _player.position - transform.position;
        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, angle - 90f));
    }

    public void TakeDamage(float damage)
    {
        life -= damage;
        StartCoroutine(BlinkWhite(.15f));
        if (life <= 0)
            Destroy(this.gameObject);
    }

    private IEnumerator BlinkWhite(float duration)
    {
        spriteRenderer.material = flashMaterial;
        yield return new WaitForSeconds(duration);
        spriteRenderer.material = standardMaterial;
    }
}
