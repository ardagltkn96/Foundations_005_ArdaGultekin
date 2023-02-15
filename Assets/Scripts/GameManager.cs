using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject messagePanel;

    private void Start()
    {
        StartCoroutine(RemovePanel());
    }

    IEnumerator RemovePanel()
    {
        yield return new WaitForSeconds(5);
        messagePanel.SetActive(false);
    }
}
