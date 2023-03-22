using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaxPoint : MonoBehaviour
{
   public bool isMaxPointHit;

   private void Start()
   {
      isMaxPointHit = false;
   }

   private void OnTriggerEnter(Collider other)
   {
      if (other.CompareTag("Bullet"))
      {
         isMaxPointHit = true;
      }
     
   }
}
