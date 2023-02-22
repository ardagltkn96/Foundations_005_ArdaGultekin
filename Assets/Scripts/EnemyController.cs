using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    [SerializeField]private NavMeshAgent agent;
    [SerializeField] private float threshold = 0.5f;

    [SerializeField] private Transform[] points;

    private int currentPoint;
    // [SerializeField] private Transform point1;
    // [SerializeField] private Transform point2;
    // [SerializeField] private Transform point3;
   

    private bool moving = false;

    //private Transform currentPoint;
    
    // Start is called before the first frame update
    void Start()
    {
        currentPoint = 0;
        MoveToNextPoint();
    }

    // Update is called once per frame
    void Update()
    {
        if (!agent.pathPending && agent.remainingDistance < threshold)
        {
            MoveToNextPoint();
        }
        // if (!moving)
        // {
        //     if (currentPoint == point1)
        //     {
        //         currentPoint = point2;
        //     }
        //     else
        //     {
        //         currentPoint = point1;
        //     }
        //    
        //     
        //     agent.SetDestination(currentPoint.position);
        //     moving = true;
        // }
        //
        // if (moving && Vector3.Distance(transform.position, currentPoint.position) < threshold)
        // {
        //     moving = false;
        // }
        
    }

    private void MoveToNextPoint()
    {
        if (points.Length != 0)
        {
            agent.destination = points[currentPoint].position;
            currentPoint = (currentPoint + 1) % points.Length;
        }
        else
        {
            Debug.Log("Please set points to patrol!");
            return;
        }
    }


}
