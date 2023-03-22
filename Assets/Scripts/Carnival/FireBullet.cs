using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class FireBullet : MonoBehaviour
{
    [SerializeField] private GameObject _bullet;

    [SerializeField] private Transform _spawnPoint;

    [SerializeField] private float _fireSpeed = 20;

    public int numberOfBullets = 9;

    public bool needToReload = false;
    
    // Start is called before the first frame update
    void Start()
    {
        XRGrabInteractable grabbable = GetComponent<XRGrabInteractable>();
        grabbable.activated.AddListener(Fire);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void Fire(ActivateEventArgs arg)
    {
        if (numberOfBullets > 0)
        {
            GameObject spawnedBullet = Instantiate(_bullet);
            spawnedBullet.transform.position = _spawnPoint.position;
            spawnedBullet.GetComponent<Rigidbody>().velocity = _spawnPoint.forward * _fireSpeed;
            numberOfBullets -= 1;
            Destroy(spawnedBullet, 3);
        }
        else
        {
            needToReload = true;
        }
        
    }
}
