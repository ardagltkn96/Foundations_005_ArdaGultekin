using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class GoalTrigger : MonoBehaviour
{
    public UnityEvent onGoalReached;
    
    private void OnTriggerEnter(Collider other)
    {
        if (!other.gameObject.CompareTag("Player")) return;
        onGoalReached.Invoke();
    }

    
}
