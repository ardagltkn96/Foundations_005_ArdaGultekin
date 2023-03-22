using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
public class SocketCheck : MonoBehaviour
{
    private XRSocketInteractor _socket;
    [SerializeField] private FireBullet _fireBullet;

    public bool clipExist;
    // Start is called before the first frame update
    void Start()
    {
        clipExist = false;
        _socket = GetComponent<XRTagLimitedSocketInteractorForCarnival>();
        Debug.Log("SOCKET: " + _socket.name);
    }

    public void socketCheck()
    {
        IXRSelectInteractable objName = _socket.GetOldestInteractableSelected();

        if (objName != null)
        {
            if (objName.transform.CompareTag("GunClip") && _fireBullet.needToReload && !clipExist)
            {
                _fireBullet.numberOfBullets = 9;
                _fireBullet.needToReload = false;
                clipExist = true;
            }
            else
            {
                clipExist = false;
            }
        }
        
       
    }

    private void Update()
    {
        socketCheck();
    }
}
