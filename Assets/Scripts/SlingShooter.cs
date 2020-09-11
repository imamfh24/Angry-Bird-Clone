using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlingShooter : MonoBehaviour
{
    public CircleCollider2D circleCollider2D;
    private Vector2 _startPos;

    [SerializeField]
    private float _radius = 0.75f;

    [SerializeField]
    private float _throwSpeed = 30f;

    private Bird _bird;

    // Start is called before the first frame update
    void Start()
    {
        _startPos = transform.position;
    }

    private void OnMouseUp()
    {
        /*circleCollider2D.enabled = false;*/
        Vector2 velocity = _startPos - (Vector2)transform.position;
        float distance = Vector2.Distance(_startPos, transform.position);

        _bird.Shoot(velocity, distance, _throwSpeed);

        // Kembalikan ketapel ke posisi awal
        transform.position = _startPos;
    }

    public void InitatieBird(Bird bird)
    {
        _bird = bird;
        _bird.MoveTo(transform.position, gameObject);
        circleCollider2D.enabled = true;
    }

    private void OnMouseDrag()
    {
        // Mengubah posisi mouse ke world position
        Vector2 p = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Debug.Log("P: " + p);
        Debug.Log("Start Position" + _startPos);
        // Hitung supaya karet ketapel berada didalam radius  yang ditentukan
        Vector2 dir = p - _startPos;
        Debug.Log("dir old: " + dir);
        Debug.Log("dir.sqrMagnitude: " + dir.sqrMagnitude);
        if (dir.sqrMagnitude > _radius)
        {
            Debug.Log("dir.normalized: " + dir.normalized);
            dir = dir.normalized * _radius;
            Debug.Log("dir.normalized: " + dir.normalized + " * _radius: " + _radius);
            Debug.Log("new dir " + dir);
        }


        transform.position = _startPos + dir;
        Debug.Log("transform.position: " + transform.position); 
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
