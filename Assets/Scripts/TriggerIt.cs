using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerIt : MonoBehaviour
{
   private void OnTriggerEnter(Collider other)
   {
      Debug.Log("Player enter trigger volume!");
   }
}
