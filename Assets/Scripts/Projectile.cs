using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float speed;
    private float projectileDirection;
    private bool hit;
    private BoxCollider2D boxCollider;
    private Animator anim;
    private float lifeTime;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();
    }

    private void Update()
    {
        if (hit) return;
        {
            float movementSpeed = speed * Time.deltaTime * projectileDirection;
            transform.Translate(movementSpeed, 0, 0);
        }

        lifeTime += Time.deltaTime;
        if (lifeTime > 5)
        {
            this.gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        hit = true;
        boxCollider.enabled = false;
        anim.SetTrigger("Explode");
    }

    public void SetDirection(float direction)
    {
        lifeTime = 0;
        projectileDirection = direction;
        this.gameObject.SetActive(true);
        hit = false;
        boxCollider.enabled = true;

        float localScaleX = transform.localPosition.x;
        if (Mathf.Sign(localScaleX) != direction)
        {
            localScaleX = -localScaleX;
        }

        transform.localScale = new Vector3(localScaleX, transform.localScale.y, transform.localScale.z);
    }

    private void Deactivate()
    {
        this.gameObject.SetActive(false);
    }
}
