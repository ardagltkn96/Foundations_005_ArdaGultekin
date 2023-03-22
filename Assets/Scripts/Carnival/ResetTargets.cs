using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetTargets : MonoBehaviour
{
    [SerializeField] private GameObject _target1;
    [SerializeField] private GameObject _target2;
    public bool isResetClicked = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Bullet"))
        {
            _target1.SetActive(true);
            _target2.SetActive(true);
            isResetClicked = true;
        }
    }
}
