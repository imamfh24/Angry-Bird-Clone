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
        slingShooter.InitatieBird(birds[0]);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
