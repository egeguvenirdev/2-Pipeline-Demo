using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipelineCreator : MonoBehaviour
{
    #region Pipes

    //last pipes information
    [SerializeField]
    private GameObject lastPipe;

    //pipe tags & random number
    string[] pipeTags = new string[] { "thic1", "thic2", "thic3", "tiny1", "tiny2", "tiny3" };
    private int randomListNumber;

    #endregion

    private ObjectPooler objPooler;

    private void Start()
    {
        objPooler = FindObjectOfType<ObjectPooler>();

        StartCoroutine(InstantiatePipe()); // CHANGE IT TO WHEN PLAYER CLICKS
    }

    IEnumerator InstantiatePipe()
    {
        //creating a random number for random pipe tag
        randomListNumber = Random.Range(0, pipeTags.Length);

        //pulling and repositioning a random pipe from pool
        var poolObj = objPooler.GetPooledObject(pipeTags[randomListNumber]);
        poolObj.transform.position = lastPipe.transform.GetChild(1).transform.position; ;
        lastPipe = poolObj;

        //i changed last pipe and its end point
        poolObj.SetActive(true);

        yield return new WaitForSeconds(1.4f);

        StartCoroutine(InstantiatePipe());
        //lastPipe = poolObj;       
    }
}


