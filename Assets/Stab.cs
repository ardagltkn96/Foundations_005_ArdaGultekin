using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stab : MonoBehaviour
{
   public Throwable _throwable;
     private void OnCollisionEnter(Collision collision)
       {
          Debug.Log("COLLIDED WITH : " + collision.transform.name, collision.transform);
          if (collision.transform.CompareTag("Stickable"))
          {
             _throwable._rb.constraints = RigidbodyConstraints.FreezeAll;
            
          }
       }

     private void OnTriggerEnter(Collider other)
     {
        Debug.Log("COLLIDED WITH : " + other.transform.name, other.transform);
        if (other.transform.CompareTag("Stickable"))
        {
           _throwable._rb.constraints = RigidbodyConstraints.FreezeAll;
        }
     }
}
