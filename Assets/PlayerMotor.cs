using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ConfigurableJoint))]
[RequireComponent(typeof(Rigidbody))]
public class PlayerMotor : MonoBehaviour {

    [SerializeField] Camera cam;

    Vector3 velocity = Vector3.zero;
    Vector3 roataion = Vector3.zero;
    float camRoataion = 0f;
    float currentCameraRoationX = 0f;
    Vector3 thrustForce = Vector3.zero;

    [SerializeField] float CameraLimit = 85f;


    Rigidbody rb;

    void Start(){
        rb = GetComponent<Rigidbody>();
    }
    
    public void Move(Vector3 _veleocity)
    {
        velocity = _veleocity;
    }

    public void Rotation(Vector3 _rotation)
    {
        roataion = _rotation;
    }

    public void CamRotation(float _camrotation)
    {
        camRoataion = _camrotation;
    }

    private void FixedUpdate()
    {
        PerfromMovement();
        PerfromRotate();
    }

    private void PerfromRotate()
    {
        rb.MoveRotation(rb.rotation * Quaternion.Euler(roataion));
        if (cam != null)
        {
            currentCameraRoationX -= camRoataion;
            currentCameraRoationX = Mathf.Clamp(currentCameraRoationX, -CameraLimit, CameraLimit);

            cam.transform.localEulerAngles = new Vector3(currentCameraRoationX, 0f, 0f);
        }
    }

    private void PerfromMovement()
    {
        if (velocity != Vector3.zero)
        {
            rb.MovePosition(rb.position + velocity * Time.deltaTime);
        }

        if (thrustForce != Vector3.zero)
        {
            rb.AddForce(thrustForce * Time.deltaTime, ForceMode.Acceleration);
        }
    }

    public void ApplyForce(Vector3 _thrustFroce)
    {
        thrustForce = _thrustFroce;
    }
}
