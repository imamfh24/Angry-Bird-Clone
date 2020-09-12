using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public GameObject panelWin;
    public GameObject panelLose;

    public float delayWin = 1f;

    public SceneController sceneController;
    private void Start()
    {
        /*sceneController = GameObject.Find("SceneController").GetComponent<SceneController>();*/
    }

    public void LevelComplete()
    {
        StartCoroutine(DelayWin(panelWin));
    }

    private IEnumerator DelayWin(GameObject panelWin)
    {
        panelWin.SetActive(true);
        yield return new WaitForSeconds(delayWin);
        sceneController.NextLevel();
    }

    public void LevelLose()
    {
        panelLose.SetActive(true);
    }
}
