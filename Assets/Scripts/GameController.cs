using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{

    public SlingShooter slingShooter;
    public List<Bird> birds;

    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0; i < birds.Count; i++)
        {
            birds[i].OnBirdDestroyed += ChangeBird;
        }
        slingShooter.InitatieBird(birds[0]);
    }

    public void ChangeBird()
    {
        birds.RemoveAt(0);

        if(birds.Count > 0)
        {
            slingShooter.InitatieBird(birds[0]);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
