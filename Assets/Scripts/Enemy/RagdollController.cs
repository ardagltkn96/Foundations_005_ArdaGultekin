using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RagdollController : MonoBehaviour
{
   public Rigidbody enemyRB;
   public CapsuleCollider capsuleCollider;
   private Rigidbody[] _rigidbodies;
   private Collider[] _colliders;
   
   private void Awake()
   {
      _rigidbodies = GetComponentsInChildren<Rigidbody>();
      _colliders = GetComponentsInChildren<Collider>();

      
      SetRigidBodiesKinematic(true);
      SetCollidersEnabled(false);
      
      capsuleCollider.enabled = true;
      
   }

   private void SetCollidersEnabled(bool enabled)
   {
      foreach (Collider col in _colliders)
      {
         col.enabled = enabled;
      }
   }

   private void SetRigidBodiesKinematic(bool kinematic)
   {
      foreach (Rigidbody rb in _rigidbodies)
      {
         rb.isKinematic = kinematic;
      }
   }

   public void ActivateRagdoll()
   {
      capsuleCollider.enabled = false;
      enemyRB.isKinematic = true;

      SetCollidersEnabled(true);
      SetRigidBodiesKinematic(false);
      enemyRB.constraints = RigidbodyConstraints.FreezePosition;
      enemyRB.velocity = Vector3.zero;
      enemyRB.GetComponent<FieldOfView>().enabled = false;
      enemyRB.GetComponent<EnemyController>().enabled = false;
   }
}
