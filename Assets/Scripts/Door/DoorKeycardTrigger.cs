using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class DoorKeycardTrigger : DoorTrigger
{
    [SerializeField] private XRSocketInteractor _socket;
    [SerializeField] private int _keycardLevel = 1;
    [SerializeField] private Renderer _lightObject;
    [SerializeField] private Light _light;
    [SerializeField] private Color _defaultColor = Color.yellow;
    [SerializeField] private Color _errorColor = Color.red;
    [SerializeField] private Color _successColor = Color.green;
    private bool _isOpen = false;
    
    // Start is called before the first frame update
    void Start()
    {
        SetLightColor(_defaultColor);
        
        _socket.selectEntered.AddListener(KeycardInserted);
        _socket.selectExited.AddListener(KeycardRemoved);
    }

    private void KeycardRemoved(SelectExitEventArgs arg0)
    {
       SetLightColor(_defaultColor);
       _isOpen = false;
       CloseDoor();
    }

    private void KeycardInserted(SelectEnterEventArgs arg0)
    {
        if (!arg0.interactable.TryGetComponent(out Keycard keycard))
        {
            Debug.Log("No Keycard component is attached");
            SetLightColor(_errorColor);
            return;
        }

        if (keycard.keycardlevel >= _keycardLevel)
        {
            SetLightColor(_successColor);
            _isOpen = true;
            OpenDoor();
        }
        else
        {
            SetLightColor(_errorColor);
        }
    }

    protected override void OnTriggerExit(Collider other)
    {
        if (_isOpen)
        {
            return;
        }

        base.OnTriggerExit(other);
    }

    private void SetLightColor(Color color)
    {
        _lightObject.material.color = color;
        _light.color = color;
    }
}
