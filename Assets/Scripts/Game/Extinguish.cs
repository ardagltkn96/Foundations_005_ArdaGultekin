using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Extinguish : MonoBehaviour
{
    [SerializeField] private ParticleSystem _firePS;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Torch") && _firePS.gameObject.activeSelf)
        {
            _firePS.gameObject.SetActive(false);
        }
    }
}
