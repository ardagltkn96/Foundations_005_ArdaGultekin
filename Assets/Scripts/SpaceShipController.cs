using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceShipController : MonoBehaviour
{
    public Transform controlVolume;
    public float speed = 1.0f;
    public float rotationSpeed = 1.0f;

    private Vector3 previousPosition;
    private Quaternion previousRotation;
    public GameObject spaceship;
    
    void FixedUpdate()
    {
        if (controlVolume.GetComponent<Collider>().bounds.Contains(transform.position))
        {
            //Debug.Log("RIGHT HAND IS IN THE CONTROL VOLUME");
            Vector3 movement = (transform.position - previousPosition) * speed;
            spaceship.transform.Translate(movement, Space.Self);

            Vector3 rotation = transform.rotation.eulerAngles - previousRotation.eulerAngles;
            rotation.x = Mathf.Clamp(rotation.x, -180, 180);
            rotation.y = Mathf.Clamp(rotation.y, -180, 180);
            rotation.z = Mathf.Clamp(rotation.z, -180, 180);

            float roll = rotation.z * rotationSpeed;
            float pitch = rotation.x * rotationSpeed;
            float yaw = -rotation.y * rotationSpeed;

            spaceship.transform.Rotate(pitch, yaw, roll, Space.Self);
        }

        previousPosition = transform.position;
        previousRotation = transform.rotation;
    }
}

