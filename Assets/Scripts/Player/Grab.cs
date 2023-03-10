using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grab : MonoBehaviour
{
    [SerializeField] private Transform _cameraPosition;
    [SerializeField] private Transform _holdPosition;
    [SerializeField] private float _grabRange = 2f;
    [SerializeField] private float _throwForce = 20f;
    [SerializeField] private float _snapSpeed = 40f;
    [SerializeField] private Transform _hidingPlace;

    private Rigidbody _grabbedObject;

    private bool _grabPressed = false;
    public bool isHiding = false;
  
    // Update is called once per frame
    void FixedUpdate()
    {
        if (_grabbedObject)
        {
            _grabbedObject.velocity = (_holdPosition.position - _grabbedObject.transform.position) * _snapSpeed;
            if (!_grabbedObject.CompareTag("Box"))
            {
                _grabbedObject.GetComponent<Rigidbody>().isKinematic = false;
            }
            
        }
    }

    private void OnGrab()
    {
        if (_grabPressed)
        {
            _grabPressed = false;
            Debug.Log("Grab Released");
            
            if(!_grabbedObject) return;

            if (_grabbedObject.CompareTag("Box"))
            {
                isHiding = false;
            }
            
            DropGrabbedObject();
        }
        else
        {
            _grabPressed = true;
            Debug.Log("Grab Pressed");

            if (Physics.Raycast(_cameraPosition.position, _cameraPosition.forward, out RaycastHit hit, _grabRange))
            {
                if (hit.transform.gameObject.CompareTag("Box"))
                {
                    _grabbedObject = hit.transform.GetComponent<Rigidbody>();
                    transform.position = _hidingPlace.position;
                    _grabbedObject.transform.parent = _holdPosition;
                    isHiding = true;
                }
                
                if (!hit.transform.gameObject.CompareTag("Grabbable")) return;

                _grabbedObject = hit.transform.GetComponent<Rigidbody>();
                _grabbedObject.transform.parent = _holdPosition;
                Debug.Log("Grabbed: "  + _grabbedObject.name);

            }
        }
    }

    private void DropGrabbedObject()
    {
        _grabbedObject.transform.parent = null;
        _grabbedObject = null;
    }

    private void OnThrow()
    {
        if (!_grabbedObject) return;
        
        
        _grabbedObject.AddForce(_cameraPosition.forward *_throwForce, ForceMode.Impulse);
        
        DropGrabbedObject();

    }

   
}
