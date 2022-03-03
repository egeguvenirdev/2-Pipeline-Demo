using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipelineCreator : MonoBehaviour
{
    #region Pipes
    [SerializeField] private GameObject lastPipe;
    private Transform endPoint;
    //private ArrayList pipeTags = new ArrayList(); //{ "3x3.13", "3.5x1.2", "3x1.5", "2x0.7", "2x0.9", "2.5x0.9" };
    //List<string> pipeTags = new List<string>();
    string[] pipeTags = new string[] { "thic1", "thic2", "thic3", "tiny1", "tiny2", "tiny3" };
    private int randomListNumber;

    #endregion

    private ObjectPooler objPooler;

    private void Start()
    {
        objPooler = FindObjectOfType<ObjectPooler>();
        endPoint = lastPipe.transform.GetChild(1).transform;

        StartCoroutine(InstantiatePipe()); // CHANGE IT TO WHEN PLAYER CLICKS
    }

    /*private void Update()
    {
            StartCoroutine(InstantiatePipe());
    }*/

    IEnumerator InstantiatePipe()
    {
        //creating a random number for random pipe tag
        randomListNumber = Random.Range(0, pipeTags.Length);

        //pulling and repositioning a random pipe from pool
        var poolObj = objPooler.GetPooledObject(pipeTags[randomListNumber]);
        poolObj.transform.position = endPoint.position;
        lastPipe = poolObj;

        //i changed last pipe and its end point
        endPoint.position = lastPipe.transform.GetChild(1).transform.position;
        poolObj.SetActive(true);

        yield return new WaitForSeconds(1.4f);

        StartCoroutine(InstantiatePipe());
        //lastPipe = poolObj;       
    }
}


