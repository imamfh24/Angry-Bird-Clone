using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bird : MonoBehaviour
{
    public enum BirdState { Idle, Thrown}
    private Rigidbody2D rigidBody2D;
    private CircleCollider2D circleCollider2D;
    private BirdState _state;
    private float _minVelocity = 0.05f;
    private bool _flagDestroy = false;

    // Start is called before the first frame update
    void Start()
    {
        rigidBody2D = GetComponent<Rigidbody2D>();
        circleCollider2D = GetComponent<CircleCollider2D>();

        rigidBody2D.bodyType = RigidbodyType2D.Kinematic;
        circleCollider2D.enabled = false;
        _state = BirdState.Idle;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        if(_state == BirdState.Idle && rigidBody2D.velocity.sqrMagnitude >= _minVelocity)
        {
            _state = BirdState.Thrown;
        }

        if(_state == BirdState.Thrown && rigidBody2D.velocity.sqrMagnitude < _minVelocity && !_flagDestroy)
        {
            //Hancurkan gameobject setelah 2 detik
            //Jika kecepatannya sudah kurang dari batas minimum
            _flagDestroy = false;
            StartCoroutine(DestroyAfter(2));
        }
    }

    private IEnumerator DestroyAfter(float second)
    {
        yield return new WaitForSeconds(second);
        Destroy(gameObject);
    }

    public void MoveTo(Vector2 target, GameObject parent)
    {
        gameObject.transform.SetParent(parent.transform);
        gameObject.transform.position = target;
    }

    public void Shoot(Vector2 velocity, float distance, float speed)
    {
        circleCollider2D.enabled = true;
        rigidBody2D.bodyType = RigidbodyType2D.Dynamic;
        rigidBody2D.velocity = velocity * speed * distance;
    }
}
