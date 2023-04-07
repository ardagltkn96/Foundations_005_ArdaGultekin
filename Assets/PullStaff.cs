using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PullStaff : MonoBehaviour
{
    [SerializeField] private Transform _spawnPoint;

    [SerializeField] private ParticleSystem _particleSystem;

    [SerializeField] private float _force = 20f;
    
    
    public GameObject Staff;
    
    public bool isPulling = false;
    //[SerializeField] private LineRenderer _lineRenderer;
    

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Physics.Raycast(_spawnPoint.position, _spawnPoint.TransformDirection(Vector3.forward), out RaycastHit hitInfo, 100))
        {
            if (hitInfo.transform.CompareTag("Item"))
            {
                //Debug.Log("Hit :" + hitInfo.transform.name);
                _particleSystem.gameObject.SetActive(true);
                _particleSystem.Play();
                isPulling = true;
                Rigidbody rb = hitInfo.transform.GetComponent<Rigidbody>();
                PullRB(rb);
                
            }
            else
            {
                _particleSystem.gameObject.SetActive(false);
                isPulling = false;
                _particleSystem.Pause();
            }
        }
        else
        {
            _particleSystem.gameObject.SetActive(false);
            isPulling = false;
            _particleSystem.Pause();
        }

    }
    
    private void PullRB(Rigidbody rb)
    {
        if (!Staff.GetComponent<Staff>().isPushing)
        {
            rb.AddForce(-_spawnPoint.forward * _force);
        }
        else
        {
            Debug.Log("Started to move through space");
        }
    }
}
