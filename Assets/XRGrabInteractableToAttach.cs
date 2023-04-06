using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine;

public class XRGrabInteractableToAttach : XRGrabInteractable
{
    // [SerializeField] private Transform _attachPointLeft;
    // [SerializeField] private Transform _attachPointRight;
    public bool isGrabbed = false;

    public Action OnGrabbed;
    protected override void OnSelectEntered(SelectEnterEventArgs args)
    {
        // if (args.interactorObject.transform.CompareTag("LeftHand"))
        // {
        //     attachTransform = _attachPointLeft;
        // }
        // if (args.interactorObject.transform.CompareTag("RightHand"))
        // {
        //     attachTransform = _attachPointRight;
        // }
        OnGrabbed.Invoke();
        base.OnSelectEntered(args);

        isGrabbed = true;
        

    }

    protected override void OnSelectExited(SelectExitEventArgs args)
    {
        base.OnSelectExited(args);
        isGrabbed = false;
    }
}
