using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using UnityEditor;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.XR;

public class FieldOfView : MonoBehaviour
{
    public List<Transform> visibleObjects;
    
    [SerializeField] private Color _gizmoColor = Color.red;

    [SerializeField] private float _viewRadius = 6f;

    [SerializeField] private float _viewAngle = 30f;

    [SerializeField] private LayerMask _blockingLayers;

    private Grab grabManager;
    
    public Creature creature;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        visibleObjects.Clear();
        
        Collider[] targetsInViewRadius = Physics.OverlapSphere(transform.position, _viewRadius);
        foreach (Collider target in targetsInViewRadius)
        {
            if (!target.TryGetComponent<Creature>(out Creature targetCreature)) continue;
            
            if(creature.team == targetCreature.team) continue;

            Vector3 directionToTarget = (target.transform.position - transform.position).normalized;

            if (Vector3.Angle(transform.forward, directionToTarget) < _viewAngle)
            {
                Vector3 headPos = creature.head.position;
                Vector3 targetHeadPos = targetCreature.head.position;

                Vector3 dirToTargetHead = (targetHeadPos - headPos).normalized;
                float distToTargetHead = Vector3.Distance(headPos, targetHeadPos);

                if (Physics.Raycast(headPos, directionToTarget, distToTargetHead, _blockingLayers))
                {
                    continue;;
                }
                Debug.DrawLine(headPos, targetHeadPos, Color.green);
                
                Debug.Log(target);
                //If player is inside the box, dont add player to the visible objects list
                if (target.GetComponent<Grab>() != null)
                {
                    grabManager = target.GetComponent<Grab>();
                    if (grabManager.isHiding)
                    {
                        Debug.Log("PLAYER IS HIDING, CANNOT ADD TO LIST");
                        continue;
                    }
                }
                
                visibleObjects.Add(target.transform);
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = _gizmoColor;
        Handles.color = _gizmoColor;

        Handles.DrawWireArc(transform.position, transform.up, transform.forward, _viewAngle, _viewRadius);
        Handles.DrawWireArc(transform.position, transform.up, transform.forward, -_viewAngle, _viewRadius);
        
        Vector3 lineA = Quaternion.AngleAxis(_viewAngle, transform.up) * transform.forward;
        Vector3 lineB = Quaternion.AngleAxis(-_viewAngle, transform.up) * transform.forward;
        Handles.DrawLine(transform.position, transform.position + (lineA * _viewRadius));
        Handles.DrawLine(transform.position, transform.position + (lineB * _viewRadius));
    }
}
