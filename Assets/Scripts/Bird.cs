using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Bird : MonoBehaviour
{
    public enum BirdState { Idle, Thrown, HitSomething}
    public float delay = 2f;
    protected Rigidbody2D rigidBody2D;
    protected CircleCollider2D circleCollider2D;
    private BirdState _state;
    private float _minVelocity = 0.05f;
    private bool _flagDestroy = false;

    public UnityAction OnBirdDestroyed = delegate { };
    public UnityAction<Bird> OnBirdShot = delegate { };
    public BirdState State
    {
        get { return _state; }
    }

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
    private void OnCollisionEnter2D(Collision2D collision)
    {
        _state = BirdState.HitSomething;
    }

    private void FixedUpdate()
    {
        ThrownBird();
        DestroyBird(delay);
    }

    protected virtual void ThrownBird()
    {
        if (_state == BirdState.Idle && rigidBody2D.velocity.sqrMagnitude >= _minVelocity)
        {
            _state = BirdState.Thrown;
        }
    }

    protected virtual void DestroyBird(float delay)
    {
        if ((_state == BirdState.Thrown || _state == BirdState.HitSomething) && rigidBody2D.velocity.sqrMagnitude < _minVelocity && !_flagDestroy)
        {
            //Hancurkan gameobject setelah 2 detik
            //Jika kecepatannya sudah kurang dari batas minimum
            _flagDestroy = false;
            StartCoroutine(DestroyAfter(delay));
        }
    }

    protected virtual IEnumerator DestroyAfter(float second)
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
        OnBirdShot(this);
    }

    public virtual void OnTap() { }

    private void OnDestroy()
    {
        if(_state == BirdState.Thrown || _state == BirdState.HitSomething) OnBirdDestroyed();
    }

}
