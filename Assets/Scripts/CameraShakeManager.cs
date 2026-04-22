using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShakeManager : MonoBehaviour
{
    private float shakeDuration = 0f;
    public float shakeMagnitude = 0.1f;
    public float dampingSpeed = 1f;
    private Vector3 originalPosition;

    private void Awake()
    {
        originalPosition = transform.position;
    }

    private void Update ()
    {
        if(shakeDuration > 0)
        {
            transform.position = originalPosition + Random.insideUnitSphere * shakeMagnitude;
            shakeDuration -= Time.deltaTime * dampingSpeed;
            if(shakeDuration <= 0)
            {
                shakeDuration = 0f;
                transform.position = originalPosition;            
            }
        }        
    }

    public void TriggerShake(float duration, float magnitude)
    {
        shakeDuration = duration;
        shakeMagnitude = magnitude;
    }
}
