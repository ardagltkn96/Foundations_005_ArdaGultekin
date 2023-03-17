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
            OpenDoor();
        }
    }
    
    protected virtual void OnTriggerExit(Collider other)
    {
        DoorInteractor doorInter = other.GetComponent<DoorInteractor>();
        if (doorInter)
        {
            CloseDoor();
        }
        
    }
    
    protected void OpenDoor()
    {
            door.SetActive(false);
    }
    protected void CloseDoor()
    {
            door.SetActive(true);
    }
    
    // [SerializeField] private GameObject door;
    // [SerializeField] private KeyCard.KeyType _keyType;
    //
    // public KeyCard.KeyType GetKeytype()
    // {
    //     return _keyType;
    // }
    //
    // public void OpenDoor()
    // {
    //     door.SetActive(false);
    // }
}
