using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorTrigger : MonoBehaviour
{
    [SerializeField] private GameObject door;

    private void OnTriggerEnter(Collider other)
    {
        DoorInteractor doorInter = other.GetComponent<DoorInteractor>();
        if (doorInter)
        {
            door.SetActive(false);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        DoorInteractor doorInter = other.GetComponent<DoorInteractor>();
        if (doorInter)
        {
            door.SetActive(true);
        }
    }
}
