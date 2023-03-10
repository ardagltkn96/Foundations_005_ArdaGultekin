using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProximityMine : MonoBehaviour
{
    [Header("Particles")] 
    [SerializeField] private GameObject explosiveParticle = null;

    [SerializeField] private Vector3 explosionOffset = new Vector3(0, 1, 0);

    [SerializeField] private Transform rayCastPos;

    [SerializeField] private float explosionRange = 5f;

    [SerializeField] private GameObject _explodedRobot;
    
    [SerializeField] private Transform _explodedPartsArea;
    
    private string StuckObjectTag = "Stickable";
   
    
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            GetComponent<Rigidbody>().isKinematic = false;
        }
        else
        {
            GetComponent<Rigidbody>().isKinematic = true;
            
            transform.rotation = Quaternion.Euler(0, 0, 90);
            
            
        }
       
    }
    private void Update()
    {
        if (Physics.Raycast(rayCastPos.position, (rayCastPos.position - transform.position).normalized, out RaycastHit hit, explosionRange))
        {
            if (hit.transform.gameObject.CompareTag("Enemy"))
            {
                hit.transform.GetComponent<EnemyController>()._animator.enabled = false;
                GameObject particle = Instantiate(explosiveParticle, transform.position + explosionOffset, Quaternion.identity);
                Destroy(particle, 2);
                gameObject.SetActive(false);
                Debug.Log("EXPLODED: " + hit.transform.name);
                Instantiate(_explodedRobot, _explodedPartsArea.position, Quaternion.identity);
                Destroy(hit.transform.gameObject);
                //hit.transform.GetComponent<RagdollController>().ActivateRagdoll();
            }

        }
    }
    
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.magenta;
        Gizmos.DrawWireSphere(transform.position, explosionRange);
    }
}
