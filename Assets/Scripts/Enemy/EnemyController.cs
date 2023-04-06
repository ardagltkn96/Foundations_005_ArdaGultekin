using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

public class EnemyController : MonoBehaviour
{
    public enum EnemyState
    {
        Patrol = 0,
        Investigate = 1,
        GrabFriend = 2,
        InvestigateTogether = 3,
        Stunned = 4
    }
    
    [SerializeField]private NavMeshAgent _agent;
    [SerializeField] private float _waitTime = 2f;
    [SerializeField] private PatrolRoute _patrolRoute;
    [SerializeField] private FieldOfView _fov;
    [SerializeField] private Transform _friendPoint;
    [SerializeField] private Transform[] points;
    [SerializeField] private float _stunnedTime = 3f;
    
    public float _threshold = 0.5f;
    public EnemyState _state = EnemyState.Patrol;
    public Animator _animator;
    public List<Collider> RagdollParts = new List<Collider>();

    public UnityEvent<Transform> onPlayerFound;
    public UnityEvent onInvestigate;
    public UnityEvent onReturnToPatrol;
    public UnityEvent onStunned;
  
    private Transform _currentPoint;
    private bool _moving = false;
    private int _routeIndex = 0;
    private bool _forwardsAlongPath = true;
    private Vector3 _investigationPoint;
    private Vector3 _investigationTogetherPoint;
    private float _waitTimer = 0f;
    private bool _playerFound = false;
    private float _stunnedTimer = 0f;
    void Start()
    {
        _currentPoint = _patrolRoute.route[_routeIndex];
    }

    // Update is called once per frame
    void Update()
    {
        _animator.SetFloat("Speed", _agent.velocity.magnitude);

        if (_fov.visibleObjects.Count > 0)
        {
            PlayerFound(_fov.visibleObjects[0].position);
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
            case EnemyState.Stunned:
                _stunnedTimer += Time.deltaTime;
                if (_stunnedTimer >= _stunnedTime)
                {
                    ReturnToPatrol();
                    _animator.SetBool("Stunned", false);
                }
                break;
        }

    }

    public void SetStunned()
    {
        _animator.SetBool("Stunned", true);
        _stunnedTimer = 0f;
        _state = EnemyState.Stunned;
        _agent.SetDestination(transform.position);
        onStunned.Invoke();
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
        SetInvestigationPoint(investigationPoint);

        onInvestigate.Invoke();
    }

    private void SetInvestigationPoint(Vector3 investigationPoint)
    {
        _state = EnemyState.Investigate;
        _investigationPoint = investigationPoint;
        _agent.SetDestination(_investigationPoint);
    }

    public void InvestigatePointTogether(Vector3 investigationPoint)
    {
        onInvestigate.Invoke();
        _state = EnemyState.GrabFriend;
        _investigationPoint = investigationPoint;
    }

    private void PlayerFound(Vector3 investigationPoint)
    {
        if (_playerFound) return;
        
        SetInvestigationPoint(investigationPoint);
        
        onPlayerFound.Invoke(_fov.creature.head);

        _playerFound = true;

    }
    
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
        
        onReturnToPatrol.Invoke();
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
