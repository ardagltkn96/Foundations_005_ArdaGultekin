using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class FallandRestart : MonoBehaviour
{
    [SerializeField] private List<GameObject> _cans = new List<GameObject>();
    private int _knockedCanCount = 0;


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Can"))
        {
            _knockedCanCount++;
            Debug.Log("KNOCKED CAN: " + _knockedCanCount);
        }
    }

    private void Update()
    {
        if (_knockedCanCount == _cans.Count)
        {
            _knockedCanCount = 0;
            RestartCans();
        }
    }

    private void RestartCans()
    {
        for (int i = 0; i < _cans.Count; i++)
        {
            _cans[i].GetComponent<Rigidbody>().isKinematic = true;
            _cans[i].transform.position = _cans[i].GetComponent<Cans>().spawnPos;
            _cans[i].transform.rotation = _cans[i].GetComponent<Cans>().spawnRot;
            _cans[i].GetComponent<Rigidbody>().isKinematic = false;
        }
    }
}
