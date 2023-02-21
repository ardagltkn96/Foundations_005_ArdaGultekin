using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinCondition : MonoBehaviour
{
    public GameObject player;
    public GameObject winScreen;
    private bool isGameComplete;

    private void Start()
    {
        isGameComplete = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == player)
        {
            gameWon();
        }
    }

    private void gameWon()
    {
        winScreen.SetActive(true);
        isGameComplete = true;
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.R) && isGameComplete)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}
