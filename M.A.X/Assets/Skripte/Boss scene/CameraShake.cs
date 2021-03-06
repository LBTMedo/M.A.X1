﻿using UnityEngine;
using System.Collections;

public class CameraShake : MonoBehaviour {

    public float shakeAmount;
    public float shakeDuration;

    float shakePercentage;
    float startAmount;
    float startDuration;

    bool isRunning = false;

    public bool smooth;
    public float smoothAmount = 5f;

    void Start()
    {
        startAmount = shakeAmount;
        startDuration = shakeDuration;
    }

    public void ShakeCamera(float amount, float duration)
    {
        shakeAmount += amount;
        startAmount = shakeAmount;
        shakeDuration += duration;
        startDuration = shakeDuration;

        if(!isRunning) StartCoroutine(Shake());
    }

    IEnumerator Shake()
    {
        isRunning = true;

        while(shakeDuration > 0.01f)
        {
            Vector3 rotationAmount = Random.insideUnitSphere * shakeAmount;
            rotationAmount.z = 0;

            shakePercentage = shakeDuration / startDuration;

            shakeAmount = startAmount * shakePercentage;
            shakeDuration = Mathf.Lerp(shakeDuration, 0, Time.deltaTime);

            if (smooth)
            {
                transform.localRotation = Quaternion.Lerp(transform.localRotation, Quaternion.Euler(rotationAmount), Time.deltaTime * smoothAmount);
            }
            else
            {
                transform.localRotation = Quaternion.Euler(rotationAmount);
            }
            yield return null;
        }
        transform.localRotation = Quaternion.identity;
        isRunning = false;
    }

}
