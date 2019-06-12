using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public abstract class Ship : MonoBehaviour
{
    public float Energy;
    float TimeToNormalColor = 0.05f;

    public abstract void Shoot();
    public abstract void Move();
    public abstract void GetHitted(int damage);

    public virtual void Die()
    {
        Animator animator = GetComponent<Animator>();
        Collider2D collider = GetComponent<Collider2D>();

        animator.SetBool("Dead", true);
        collider.enabled = false;
        Destroy(gameObject, animator.GetCurrentAnimatorStateInfo(0).length);
    }

    protected void ChangeColor()
    {
        SpriteRenderer renderer = GetComponent<SpriteRenderer>();
        renderer.color = Color.red;
        Invoke("ReturnToNormalColor", TimeToNormalColor);
    }

    void ReturnToNormalColor()
    {
        SpriteRenderer renderer = GetComponent<SpriteRenderer>();
        renderer.color = Color.white;
    }
}
