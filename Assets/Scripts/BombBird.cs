using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombBird : Bird
{
    public GameObject bombObject;
    public float radius = 2f;

    private bool hasExplode = false;
    private CircleCollider2D bombCircleCollider;

    public void Explode()
    {
        bombCircleCollider = bombObject.GetComponent<CircleCollider2D>();
        bombCircleCollider.enabled = true;
        if(bombCircleCollider.radius <= radius && !hasExplode)
        {
            bombCircleCollider.radius = bombCircleCollider.radius + radius;
            hasExplode = true;
            StartCoroutine(ExplodeFinish(0.1f));
        }
    }

    IEnumerator ExplodeFinish(float delay)
    {
        yield return new WaitForSeconds(delay);
        bombCircleCollider.radius = circleCollider2D.radius;
    }

    protected override void DestroyBird(float delay)
    {
        if(State == BirdState.HitSomething)
        {
            Explode();
        }
        base.DestroyBird(delay);
    }
}
