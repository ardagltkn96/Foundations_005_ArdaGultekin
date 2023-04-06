using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Throwable : MonoBehaviour
{
   public Rigidbody _rb;
   private Vector3 _lastPosition;
   private float _speed;
   public XRGrabInteractableToAttach _grabInteractableToAttach;

   // public InputActionProperty leftCancel;
   // public InputActionProperty rightCancel;

   private void Start()
   {
      _rb = this.transform.GetComponent<Rigidbody>();
      _lastPosition = transform.position;
      _grabInteractableToAttach = this.transform.GetComponent<XRGrabInteractableToAttach>();
      _grabInteractableToAttach.OnGrabbed = Unfreeze;
   }

   private void Unfreeze()
   {
      _rb.constraints &= ~RigidbodyConstraints.FreezeAll;
   }


   private void Update()
   {
      _speed = (transform.position - _lastPosition).magnitude / Time.deltaTime;
      _lastPosition = transform.position;
   }
   
   
}
