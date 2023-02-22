using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerIt : MonoBehaviour
{
   [SerializeField] private GameObject player;
   [SerializeField] private float waitTime;
   private bool isTriggered = false;
   private void OnTriggerEnter(Collider other)
   {
      //In order to only spawn once, we use a bool
      if (!isTriggered)
      {
         StartCoroutine(fallingDebrisWait(waitTime));
       
      }
     
      Debug.Log("Player enter trigger volume!");
   }

   private IEnumerator fallingDebrisWait(float time)
   {
      //Wait for given number of seconds before cube spawns and falls.
      yield return new WaitForSeconds(time);
      fallingDebris();
   }

   private void fallingDebris()
   {
      var cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
      cube.transform.position = player.transform.position + new Vector3(1.5f, 5, 0);
      cube.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
      cube.AddComponent<Rigidbody>();
      isTriggered = true;
   }
}
