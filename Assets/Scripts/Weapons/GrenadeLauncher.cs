using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.XR.Interaction.Toolkit;
using Weapons;

public class GrenadeLauncher : Gun
{
    [SerializeField] private Renderer[] _gunRenderers;

    [SerializeField] private Material[] _ammoScreenMaterials;

    [SerializeField] private Projection _projection;

    [SerializeField] private Grenade _grenadePrefab;

    [SerializeField] private float _force = 20;
    // Start is called before the first frame update
    void Start()
    {
        var activeAmmoSocket = GetComponentInChildren<XRTagLimitedSocketInteractor>();
        _ammoSocket = activeAmmoSocket;
        
        base.Start();
        
        Assert.IsNotNull(_gunRenderers, "You have not assigned a renderer to gun: " + name);
        Assert.IsNotNull(_ammoScreenMaterials, "You have not assigned ammo screen materials  to gun: " + name);
    }


    private void Update()
    {
        _projection.SimulateTrajectory(_grenadePrefab, _gunBarrel.position, _gunBarrel.forward * _force);
    }

    protected override void AmmoDetached(SelectExitEventArgs arg0)
    {
        base.AmmoDetached(arg0);
        UpdateGrenadeLauncherScreen();
    }
    

    protected override void AmmoAttached(SelectEnterEventArgs arg0)
    {
        base.AmmoAttached(arg0);
        UpdateGrenadeLauncherScreen();
    }
    
    protected override void Fire(ActivateEventArgs arg0)
    {
        if (!CanFire())
        {
            _projection.GetComponent<LineRenderer>().enabled = false;
            return;
        }
        _projection.GetComponent<LineRenderer>().enabled = true;
            
        base.Fire(arg0);
        UpdateGrenadeLauncherScreen();

        var bullet = Instantiate(_grenadePrefab, _gunBarrel.position, Quaternion.identity)
            .GetComponent<Rigidbody>();
        bullet.AddForce(_gunBarrel.forward*_ammoClip.bulletSpeed, ForceMode.Impulse);
        Destroy(bullet.gameObject, 6f);
    }

    private void UpdateGrenadeLauncherScreen()
    {
        if (!_ammoClip)
        {
            AssignScreenMaterial(_ammoScreenMaterials[0]);
            return;
        }
        AssignScreenMaterial(_ammoScreenMaterials[_ammoClip.amount]);
    }

    private void AssignScreenMaterial(Material newMaterial)
    {
        foreach (var rend in _gunRenderers)
        {
            if(!rend.gameObject.activeSelf) continue;
                
            var mats = rend.materials;
            mats[1] = newMaterial;
            rend.materials = mats;
        }
            
    }
}


