using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallandRestart : MonoBehaviour
{
    public GameObject player;
    public Transform startPos;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == player)
        {
            player.transform.position = startPos.position;
        }
    }
}
