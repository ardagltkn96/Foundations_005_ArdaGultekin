using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Grenade : MonoBehaviour
{
    [SerializeField] private Rigidbody _rb;
    [SerializeField] private float _implosionRadius = 10f;
    [SerializeField] private float _implosionForce = 100f;
    [SerializeField] private float _teleportDistance = 1f;

    private bool _isExploded = false;

    public void Init(Vector3 velocity)
    {
        _rb.AddForce(velocity, ForceMode.Impulse);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Explodable" && !_isExploded)
        {
            // Trigger implosion effect
            Collider[] colliders = Physics.OverlapSphere(transform.position, _implosionRadius);
            foreach (Collider collider in colliders)
            {
                Rigidbody rb = collider.GetComponent<Rigidbody>();
                if (rb != null)
                {
                    Vector3 forceDirection = transform.position - collider.transform.position;
                    rb.AddForce(forceDirection.normalized * _implosionForce, ForceMode.Impulse);
                }
            }

            _isExploded = true;
            StartCoroutine(TeleportObjects(colliders));
        }
    }
    
    IEnumerator TeleportObjects(Collider[] colliders)
    {
        yield return new WaitForSeconds(1f); // wait for 1 second before teleporting objects

        foreach (Collider collider in colliders)
        {
            Vector3 direction = collider.transform.position - transform.position;
            float distance = direction.magnitude;
            if (distance <= _teleportDistance)
            {
                Vector3 newPosition = transform.position + Random.insideUnitSphere * _teleportDistance;
                collider.transform.position = newPosition;
            }
        }
    }

   
}
