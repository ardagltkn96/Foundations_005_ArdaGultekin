using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Quaternion = System.Numerics.Quaternion;

public class Cans : MonoBehaviour
{
    public Vector3 spawnPos;

    public UnityEngine.Quaternion spawnRot;

    private void Awake()
    {
        spawnPos = transform.position;
        spawnRot = transform.rotation;
    }
}
