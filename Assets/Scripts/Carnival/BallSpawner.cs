using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class BallSpawner : MonoBehaviour
{
    [SerializeField] private GameObject _ball;
    [SerializeField] private GameObject _ballPos;
    //private bool _ballInSpawnerArea = true;
    //public bool _isSpawned = false;
    //private bool _isFirst = true;
    


    // private void Update()
    // {
    //     if (_ball.GetComponent<Collider>().bounds.Contains(_ballPos))
    //     {
    //         Debug.Log("BALL IS WITHIN THE SPAWNER");
    //         _isSpawned = true;
    //     }
    //     else
    //     {
    //         _isSpawned = false;
    //         Debug.Log("BALL IS NOT WITHIN THE SPAWNER");
    //         StartCoroutine(SpawnNewBall());
    //     }
    // }
    
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Grabbable"))
        {
            Debug.Log("BALL IS TAKEN OUT OF THE SPAWNER AREA");
            StartCoroutine(SpawnNewBall());
        }
    }
    private IEnumerator SpawnNewBall()
    {
        Debug.Log("spawningball");
        yield return new WaitForSeconds(1f);
        GameObject newBall = Instantiate(_ball, _ballPos.transform.position, Quaternion.identity);
        //newBall.transform.position = transform.position;
        //_isSpawned = true;
    }

    
}
