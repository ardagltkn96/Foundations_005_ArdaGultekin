using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    public enum EnemyState
    {
        Patrol = 0,
        Investigate = 1,
        GrabFriend = 2,
        InvestigateTogether = 3
    }
    
    [SerializeField]private NavMeshAgent _agent;
    public float _threshold = 0.5f;
    [SerializeField] private float _waitTime = 2f;
    [SerializeField] private PatrolRoute _patrolRoute;
    [SerializeField] private FieldOfView _fov;
    public EnemyState _state = EnemyState.Patrol;
    [SerializeField] private Transform _friendPoint;
    // [SerializeField] private NavMeshAgent _friendAgent;
    // [SerializeField] private GameObject _friend;

    [SerializeField] private Transform[] points;

    private Transform _currentPoint;
    private bool _moving = false;
    private int _routeIndex = 0;
    private bool _forwardsAlongPath = true;
    private Vector3 _investigationPoint;
    private Vector3 _investigationTogetherPoint;
    private float _waitTimer = 0f;

    // Start is called before the first frame update
    void Start()
    {
        _currentPoint = _patrolRoute.route[_routeIndex];
    }

    // Update is called once per frame
    void Update()
    {
        if (_fov.visibleObjects.Count > 0)
        {
            InvestigatePoint(_fov.visibleObjects[0].position);
        }

        switch (_state)
        {
            case EnemyState.Patrol:
                UpdatePatrol();
                break;
            case EnemyState.Investigate:
                UpdateInvestigate();
                break;
            case EnemyState.GrabFriend:
                GetFriendFromPatrolPoint();
                break;
            case EnemyState.InvestigateTogether:
                UpdateInvestigateTogether();
                break;
        }

    }

    private void GetFriendFromPatrolPoint()
    {
        if (_friendPoint != null)
        {
            _agent.SetDestination(_friendPoint.position);
            if (Vector3.Distance(_agent.transform.position, _friendPoint.position) <= _threshold)
            {
               
                _state = EnemyState.InvestigateTogether;
                _friendPoint.GetComponent<EnemyController>()._state = EnemyState.InvestigateTogether;
              
              
            }
            //Debug.Break();
        }
    }

    public void InvestigatePoint(Vector3 investigationPoint)
    {
        _state = EnemyState.Investigate;
        _investigationPoint = investigationPoint;
        _agent.SetDestination(_investigationPoint);
    } 
    public void InvestigatePointTogether(Vector3 investigationPoint)
    {
        _state = EnemyState.GrabFriend;
        _investigationPoint = investigationPoint;
        //_agent.SetDestination(_friendPoint.position);
    }

    // private bool checkRemainingPath()
    // {
    //     if (Vector3.Distance(_agent.destination, _agent.transform.position) <= _threshold)
    //     {
    //         //Reached Destination.
    //         if (!_agent.hasPath || _agent.velocity.sqrMagnitude == 0f)
    //         {
    //             return true;
    //         }
    //     }
    //
    //     return false;
    // }
    
    

    private void UpdateInvestigate()
    {
        //Debug.Log(gameObject.name + "POS: " + gameObject.transform.position);
        //Debug.Log("Investigating");
       if (Vector3.Distance(transform.position, _investigationPoint) < _threshold)
       {
           _waitTimer += Time.deltaTime;
           if (_waitTimer > _waitTime)
           {
               ReturnToPatrol();
           }
       }
    } 
    private void UpdateInvestigateTogether()
    {
        _agent.SetDestination(_investigationPoint);
        _friendPoint.GetComponent<EnemyController>()._agent.SetDestination(_investigationPoint);
        _state = EnemyState.Investigate;
        //_friendPoint.GetComponent<EnemyController>()._state = EnemyState.Investigate;
        //_friendPoint.GetComponent<EnemyController>()._state = EnemyState.Investigate;

        // if (checkRemainingPath())
        // {
        //     _agent.SetDestination(_investigationPoint);
        //     _state = EnemyState.Investigate;
        //     
        //     // _friendAgent.SetDestination(_investigationPoint);
        //     // _friend.GetComponent<EnemyController>()._state = _state;
        //
        // }
    }

    private void ReturnToPatrol()
    {
        _state = EnemyState.Patrol;
        _waitTimer = 0;
        _moving = false;
    }

    private void UpdatePatrol()
    {
        if (!_moving)
        {
            NextPatrolPoint();

            _agent.SetDestination(_currentPoint.position);
            _moving = true;
        }

        if (_moving && Vector3.Distance(transform.position, _currentPoint.position) < _threshold)
        {
            _moving = false;
        }
    }

    private void NextPatrolPoint()
    {
        if (_forwardsAlongPath)
        {
            _routeIndex++;
        }
        else
        {
            _routeIndex--;
        }
        
        if (_routeIndex == _patrolRoute.route.Count)
        {
            if (_patrolRoute.patrolType == PatrolRoute.PatrolType.Loop)
            {
                _routeIndex = 0;
            }
            else
            {
                _forwardsAlongPath = false;
                _routeIndex--;
            }

        }

        if (_routeIndex == 0)
        {
            _forwardsAlongPath = true;
        }
            
        _currentPoint = _patrolRoute.route[_routeIndex];
    }
    

}
