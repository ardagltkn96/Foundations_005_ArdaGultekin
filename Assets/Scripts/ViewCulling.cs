using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent((typeof(FieldOfView)))]
public class ViewCulling : MonoBehaviour
{
    private FieldOfView _fov;

    private List<Transform> _enemiesInViewLastFrame;
    // Start is called before the first frame update
    void Start()
    {
        _fov = GetComponent<FieldOfView>();
        _enemiesInViewLastFrame = new List<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        //out of vıew
        var enemiesInOutOfView = _fov.visibleObjects.Except(_enemiesInViewLastFrame).ToList();
        foreach (var enemy in enemiesInOutOfView)
        {
            Debug.Log(enemy.name + "is in view");
            var rend = enemy.GetComponentInChildren<SkinnedMeshRenderer>();
            if (rend)
            {
                rend.enabled = false;
            }
        }
        //IN VIEW
        var enemiesInViewNow = _fov.visibleObjects.Except(_enemiesInViewLastFrame).ToList();
        foreach (var enemy in enemiesInViewNow)
        {
            Debug.Log(enemy.name + "is in view");
            var rend = enemy.GetComponentInChildren<SkinnedMeshRenderer>();
            if (rend)
            {
                rend.enabled = true;
            }
        }
        _enemiesInViewLastFrame = new List<Transform>(_fov.visibleObjects);
    }
}
