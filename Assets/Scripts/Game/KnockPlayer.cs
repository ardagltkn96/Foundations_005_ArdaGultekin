using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnockPlayer : MonoBehaviour
{
   private void OnTriggerEnter(Collider other)
   {
      if (other.transform.name == "XR Rig")
      {
         other.transform.GetComponent<Rigidbody>().isKinematic = false;
      }
      else
      {
         Debug.Log("No player detected");
      }
   }
}
