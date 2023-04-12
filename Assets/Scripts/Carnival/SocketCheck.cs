using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
public class SocketCheck : MonoBehaviour
{
    private XRSocketInteractor _socket;
    [SerializeField] private FireBullet _fireBullet;
    [SerializeField] private GameObject _bullet;

    public bool clipExist = false;
    public bool canFireAll = false;

    public bool _fireRateExist = false;
    
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

            if (objName.transform.CompareTag("FireRate"))
            {
                _fireBullet._fireSpeed = 30f;
            }

            if (objName.transform.CompareTag("Impact"))
            {
                _fireBullet._bullet = _bullet;
            }
            if (objName.transform.CompareTag("FireAll"))
            {
                canFireAll = true;
            }
            else
            {
                canFireAll = false;
            }
        }


    }

    private void Update()
    {
        socketCheck();
    }
}
