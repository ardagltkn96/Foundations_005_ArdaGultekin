using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class KeyCardHolder : MonoBehaviour
{
    public event EventHandler OnKeyCardsChanged;
    private List<KeyCard.KeyType> _keyList;
    private Grab
        _grabObject;
    private void Awake()
    {
        _keyList = new List<KeyCard.KeyType>();
    }

    public List<KeyCard.KeyType> GetKeyList()
    {
        return _keyList;
    }
    public void AddKeyCard(KeyCard.KeyType keyType)
    {
        Debug.Log("Added Key: " + keyType);
        _keyList.Add(keyType);
        OnKeyCardsChanged?.Invoke(this, EventArgs.Empty);
    }

    public void RemoveKeyCard(KeyCard.KeyType keyType)
    {
        _keyList.Remove(keyType);
        OnKeyCardsChanged?.Invoke(this, EventArgs.Empty);
    }

    public bool ContainsKeyCard(KeyCard.KeyType keyType)
    {
        return _keyList.Contains(keyType);
    }

    private void OnTriggerEnter(Collider collider)
    {
        KeyCard keyCard = collider.GetComponent<KeyCard>();
        if (keyCard != null)
        {
            AddKeyCard((keyCard.GetKeyType()));
            Destroy(keyCard.gameObject);
        }

        DoorTrigger doorTrigger = collider.GetComponent<DoorTrigger>();
        if (doorTrigger != null)
        {
            if (ContainsKeyCard(doorTrigger.GetKeytype()))
            {
                //Player has the key to open this door.
                RemoveKeyCard(doorTrigger.GetKeytype());
                doorTrigger.OpenDoor();
            }
        }
    }

 
}
