using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class ActivateGrabRay : MonoBehaviour
{
    public GameObject _leftGrabRay;
    public GameObject _rightGrabRay;

    public XRDirectInteractor _leftDirectGrab;
    public XRDirectInteractor _rightDirectGrab;

    private void Update()
    {
        _leftGrabRay.SetActive(_leftDirectGrab.interactablesSelected.Count == 0);
        _rightGrabRay.SetActive(_rightDirectGrab.interactablesSelected.Count == 0);
    }
}
