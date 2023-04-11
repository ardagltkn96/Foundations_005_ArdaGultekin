using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class SafeController : MonoBehaviour
{
    public Transform dialTransform;
    public Transform handTransform;
    public TMP_Text displayText;
    public TMP_Text lockedText;
    public int[] combination = {1, 3, 0, 3};
    public float rotationSpeed = 10.0f;
    public float minValue = 0;
    public float maxValue = 9;
    public float waitDuration = 2.0f;

    private float currentValue;
    private bool isRotating;
    private float lastAngle;
    private int currentIndex = 0;

    private bool canOpenSafe = false;
    //private bool canCheckDigit = false;
    

    private void Update()
    {
        if (isRotating)
        {
            Vector3 dialForward = dialTransform.forward;
            Vector3 handForward = handTransform.position - dialTransform.position;
            float angle = Vector3.SignedAngle(handForward, dialForward, dialTransform.up);
            float delta = angle - lastAngle;
            if (delta != 0)
            {
                currentValue += delta * rotationSpeed;
                currentValue = Mathf.Clamp(currentValue, minValue, maxValue);
                displayText.text = Mathf.RoundToInt(currentValue).ToString();
                lastAngle = angle;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        
        if (other.gameObject.CompareTag("Hand"))
        {
            //Debug.Log(other.gameObject.name);
            isRotating = true;
            lastAngle = Vector3.SignedAngle(handTransform.position - dialTransform.position, dialTransform.forward, dialTransform.up);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        StartCoroutine(DelayDigitEntry());
        if (other.gameObject.CompareTag("Hand"))
        {
            isRotating = false;
            int currentNumber;
            if (int.TryParse(displayText.text, out currentNumber))
            {
                Debug.Log("CURRENT NUMBER: " + currentNumber);
                if (currentNumber == combination[currentIndex])
                {
                    currentIndex++;
                    if (currentIndex == combination.Length)
                    {
                        Debug.Log("Safe is unlocked!");
                        lockedText.text = "Unlocked!";
                        canOpenSafe = true;
                        currentIndex = 0;
                    }
                    else
                    {
                        Debug.Log("Correct digit entered. Moving on to next digit...");
                        StartCoroutine(DelayDigitEntry());
                    }
                }
                else
                {
                    currentIndex = 0;
                    Debug.Log("Incorrect digit entered. Starting over...");
                }
                //canCheckDigit = false;
            }
            else
            {
                Debug.LogError("Invalid format for current dial number!");
            }
        }
    }

    private IEnumerator DelayDigitEntry()
    {
        yield return new WaitForSeconds(waitDuration);
        //canCheckDigit = true;
    }
}

