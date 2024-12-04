using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LegKick : MonoBehaviour
{
    public float kickSpeed = 100f; 
    public float kickAngle = 45f; 
    public float kickForce = 500f; 
    private bool isKicking = false; 
    private float originalXRotation; 
    private float targetXRotation; 

    void Start()
    {
        originalXRotation = transform.rotation.eulerAngles.x;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space))
        {
            isKicking = true;
            targetXRotation = originalXRotation - kickAngle;
        }

        if (isKicking)
        {
            float newXRotation = Mathf.MoveTowardsAngle(
                transform.rotation.eulerAngles.x,
                targetXRotation,
                kickSpeed * Time.deltaTime
            );

            transform.rotation = Quaternion.Euler(newXRotation, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z);

            if (Mathf.Approximately(newXRotation, targetXRotation))
            {
                isKicking = false;
                Invoke("ResetKick", 0.2f);
            }
        }
    }

    void ResetKick()
    {
        transform.rotation = Quaternion.Euler(originalXRotation, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z);
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Collision detected with " + collision.gameObject.name);

        Rigidbody rb = collision.rigidbody;
        if (rb != null)
        {
            Vector3 kickDirection = transform.forward + transform.up;
            rb.AddForce(kickDirection * kickForce);
        }
    }
}
