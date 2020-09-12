using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{

    public SlingShooter slingShooter;
    public TrailController trailController;
    public List<Bird> birds;
    public List<Enemy> enemies;
    private Bird _shotBird;
    public BoxCollider2D tapCollider;

    private UIController uIController;
    private bool _isGameEnded = false;
    private bool _isGameWin = false;

    // Start is called before the first frame update
    void Start()
    {
        uIController = GameObject.Find("Canvas").GetComponent<UIController>();

        for(int i = 0; i < birds.Count; i++)
        {
            birds[i].OnBirdDestroyed += ChangeBird;
            birds[i].OnBirdShot += AssignTrail;
        }

        for(int i = 0; i < enemies.Count; i++)
        {
            enemies[i].OnEnemyDestroyed += CheckGameEnd;
        }

        tapCollider.enabled = false;
        slingShooter.InitatieBird(birds[0]);
        _shotBird = birds[0];
    }

    public void ChangeBird()
    {
        tapCollider.enabled = false;

        if (_isGameEnded) 
        {
            GameEnded();
            return;
        }

        birds.RemoveAt(0);

        if(birds.Count > 0)
        {
            slingShooter.InitatieBird(birds[0]);
            _shotBird = birds[0];
        } else
        {
            _isGameEnded = true;
            GameEnded();
        }
    }

    public void CheckGameEnd(GameObject destroyedEnemy)
    {
        for(int i = 0; i < enemies.Count; i++)
        {
            if(enemies[i].gameObject == destroyedEnemy)
            {
                enemies.RemoveAt(i);
                break;
            }
        }

        if(enemies.Count <= 0)
        {
            _isGameEnded = true;
            _isGameWin = true;
        }
    }

    public void AssignTrail(Bird bird)
    {
        trailController.SetBird(bird);
        StartCoroutine(trailController.SpawnTrail());
        tapCollider.enabled = true;
    }

    private void OnMouseUp()
    {
        if (_shotBird != null)
        {
            _shotBird.OnTap();
        }
    }

    private void GameEnded()
    {
        if (_isGameEnded && _isGameWin)
        {
            uIController.LevelComplete();
        } else if (_isGameEnded && !_isGameWin)
        {
            uIController.LevelLose();
        }
    }
}
