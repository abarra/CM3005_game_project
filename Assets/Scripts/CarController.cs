using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour
{
    [SerializeField] List<ParticleSystem> smokeParticles;
    private const float SlipAngleMax = 120f;
    private const float FrontBrakeMultiplier = 0.7f;
    private const float RearBrakeMultiplier = 0.3f;

    // Debug display vars
    public WheelColliders colliders;
    public WheelMeshes wheelMeshes;

    public float accelerationSignal;
    public float steeringInput;
    public float brakeSignal;

    public float motorTorque;
    public float brakeTorque;
    public AnimationCurve steeringCurve;

    public float speed;
    private float slipAngle;
    private Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        speed = rb.velocity.magnitude;
        OperateGasAndSteering();
        ComputeAcceleration();
        ComputeBrake();
        ComputeSteering();
        UpdateWheelsPosition();
    }

    private void OperateGasAndSteering()
    {

        accelerationSignal = Input.GetAxis("Vertical");
        steeringInput = Input.GetAxis("Horizontal");

        // Calculate slip angle
        slipAngle = Vector3.Angle(transform.forward, rb.velocity - transform.forward);
        if (slipAngle < SlipAngleMax && accelerationSignal < 1f)
        {
            if (accelerationSignal < 0f)
            {
                PlaySmokeParticles();
                brakeSignal = Mathf.Abs(accelerationSignal);
                accelerationSignal = 0f;
            }
        }
        else
        {
            brakeSignal = 0f;
        }
    }

    private void ComputeBrake()
    {
        colliders.FLWheel.brakeTorque = brakeSignal * brakeTorque * FrontBrakeMultiplier;
        colliders.FRWheel.brakeTorque = brakeSignal * brakeTorque * FrontBrakeMultiplier;
        colliders.RLWheel.brakeTorque = brakeSignal * brakeTorque * RearBrakeMultiplier;
        colliders.RRWheel.brakeTorque = brakeSignal * brakeTorque * RearBrakeMultiplier;
    }

    private void ComputeAcceleration()
    {
        colliders.RLWheel.motorTorque = motorTorque * accelerationSignal;
        colliders.RRWheel.motorTorque = motorTorque * accelerationSignal;
    }

    private void ComputeSteering()
    {
        // Calculate steering angle
        var steeringAngle = steeringInput * steeringCurve.Evaluate(speed);
        if (Math.Abs(steeringAngle) > 35f)
        {
            if (steeringAngle < 0)
            {
                //Play Right Tires Smoke
                PlaySmokeParticles(2);
                PlaySmokeParticles(3);
            }
            else
            {
                //Play Left Tires Smoke
                PlaySmokeParticles(0);
                PlaySmokeParticles(1);
            }
        }
        colliders.FLWheel.steerAngle = steeringAngle;
        colliders.FRWheel.steerAngle = steeringAngle;
    }

    private void UpdateWheelsPosition()
    {
        UpdateWheel(colliders.FLWheel, wheelMeshes.FLWheel);
        UpdateWheel(colliders.FRWheel, wheelMeshes.FRWheel);
        UpdateWheel(colliders.RLWheel, wheelMeshes.RLWheel);
        UpdateWheel(colliders.RRWheel, wheelMeshes.RRWheel);
    }

    private static void UpdateWheel(WheelCollider coll, Transform wheelMesh)
    {
        coll.GetWorldPose(out var position, out var quat);
        wheelMesh.position = position;
        wheelMesh.rotation = quat;
    }

    //On collecting speed collectable
    public void AddSpeedForTime(float value)
    {
        Debug.Log($"Speed:{speed + value}");
        float[] args = new float[2] { value, 2f };
        StartCoroutine("NewMotorTorqueForTime", args);
    }
    IEnumerator NewMotorTorqueForTime(float[] args)
    {
        float value = args[0];
        float duration = args[1];
        float oldMotorTorque = motorTorque;
        //Debug.Log($"Motor Torque Before: {motorTorque}");
        motorTorque += value;
        //Debug.Log($"Motor Torque During: {motorTorque}");
        yield return new WaitForSeconds(duration);
        motorTorque = oldMotorTorque;
        //Debug.Log($"Motor Torque After: {motorTorque}");
    }
    /// <summary>
    /// Emits 1 count of smoke Particles for each tire.
    /// </summary>
    void PlaySmokeParticles()
    {
        smokeParticles.ForEach(n => n.Emit(1));
    }

    /// <summary>
    /// Emits 1 count of smoke Particles for each tire.
    /// </summary>
    /// <param name="index">The index to emit from: 0 - LF, 1- LR, 2 - RF, 3 - RR </param>
    /// <param name="count">The count of emmisions, default = 1</param>
    void PlaySmokeParticles(int index, int count = 1)
    {
        smokeParticles[index].Emit(count);
    }
}