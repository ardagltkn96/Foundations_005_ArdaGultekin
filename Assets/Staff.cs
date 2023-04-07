using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Staff : MonoBehaviour
{
    [SerializeField] private Transform _spawnPoint;

    [SerializeField] private ParticleSystem _particleSystem;

    [SerializeField] private float _force = 20f;

    [SerializeField] private float _moveSpeed = 1f;

    public GameObject PullStaff;

    public bool isPushing = false;

    private bool isAnyPointing = false;

    private Rigidbody rb;

    //[SerializeField] private LineRenderer _lineRenderer;
    

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Physics.Raycast(_spawnPoint.position, _spawnPoint.TransformDirection(Vector3.forward), out RaycastHit hitInfo, 10000))
        {
            if (hitInfo.transform.CompareTag("Item"))
            {
                //Debug.Log("Hit :" + hitInfo.transform.name);
                isAnyPointing = true;
                _particleSystem.gameObject.SetActive(true);
                _particleSystem.Play();
                isPushing = true;
                GameObject go = hitInfo.transform.gameObject;
                rb = hitInfo.transform.GetComponent<Rigidbody>();
                PushRB(rb, go);
                
            }
            else
            {
                isAnyPointing = false;
                _particleSystem.gameObject.SetActive(false);
                isPushing = false;
                _particleSystem.Pause();
            }
        }
        else
        {
             isAnyPointing = false;
            _particleSystem.gameObject.SetActive(false);
            isPushing = false;
            _particleSystem.Pause();
        }

    }

    private void Update()
    {
        if (!isAnyPointing && rb != null)
        {
            rb.useGravity = true;
        }
    }


    private void PushRB(Rigidbody rb, GameObject go)
    {
        if (!PullStaff.GetComponent<PullStaff>().isPulling)
        {
            rb.AddForce(_spawnPoint.forward * _force);
        }
        //IF BOTH STAFFS ARE POINTING, MOVE THROUGH SPACE
        else
        {
            Vector3 wandPos = _spawnPoint.position;
            Vector3 wandDir = _spawnPoint.forward;

            Vector3 cubePosition = go.transform.position;
            Vector3 direction = Vector3.Normalize(wandPos - cubePosition);

            float movementFactor = Vector3.Distance(wandPos, cubePosition) * _moveSpeed;
            Vector3 movement = direction * movementFactor * Time.deltaTime;
            rb.useGravity = false;
            rb.MovePosition(cubePosition + movement);
        }
        

    }
    private void PullRB(Rigidbody rb)
    {
        rb.AddForce(-_spawnPoint.up * _force);
    }
}
