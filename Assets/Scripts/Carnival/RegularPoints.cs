using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RegularPoints : MonoBehaviour
{
    [SerializeField] private MaxPoint _maxPoint;
    public int score = 0;
    public int centerHits = 0;
    public int normalHits = 0;
    private void OnTriggerEnter(Collider other)
 {
     if (other.CompareTag("Bullet"))
     {
         if (_maxPoint.isMaxPointHit)
         {
             Debug.Log("Max Point Hit");
             _maxPoint.isMaxPointHit = false;
             score += 20;
             centerHits += 1;
         }
         else
         {
             Debug.Log("Regular Point Hit");
             score += 10;
             normalHits += 1;
         }
         
         gameObject.transform.parent.gameObject.SetActive(false);
     }
     
 }

    
}
