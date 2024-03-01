using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Enums;
using static UnityEditorInternal.VersionControl.ListControl;
public class CarController : MonoBehaviour
{
    public CarStates state = CarStates.neutral;
    public Vector3 carPos;
    public float angle;
    SoundManager _sm;
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

    float lastSpd;
    public float speed;
    public Vector3 velocity;
    private float slipAngle;
    private Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        _sm = SoundManager.Instance;
        lastSpd = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        carPos = new Vector3(0, 0, rb.position.z);
        speed = rb.velocity.magnitude;
        OperateGasAndSteering();
        ComputeAcceleration();
        ComputeBrake();
        ComputeSteering();
        UpdateWheelsPosition();
        SetCarState();
        SetPlaySmoke();
        velocity = rb.velocity;
        lastSpd = speed;
        angle = Vector3.Dot(transform.forward.normalized, velocity.normalized);
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
        float[] args = new float[2] { value, 2f };
        StartCoroutine("NewMotorTorqueForTime", args);
    }
    IEnumerator NewMotorTorqueForTime(float[] args)
    {
        float value = args[0];
        float duration = args[1];
        float oldMotorTorque = motorTorque;
        motorTorque += value;
        yield return new WaitForSeconds(duration);
        motorTorque = oldMotorTorque;
    }

    void SetPlaySmoke()
    {
        switch (state)
        {
            case CarStates.brake:
                PlaySmokeParticles();
                break;
            case CarStates.harshTurn:
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
                break;
            default:
                break;
        }
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

    private void SetCarState()
    {
        CarStates lastState = state;

        float fwdDir = Input.GetAxisRaw("Vertical");

        switch (Input.GetAxisRaw("Vertical"))
        {
            case -1:
                if (speed < lastSpd)
                {
                    state = CarStates.brake;
                }
                if (angle < 0)
                {
                    state = CarStates.reverse;
                }
                break;
            case 1:
                SetCarStateGearDrive();
                break;
            default:
                SetCarStateGearDrive();
                if (state == CarStates.gear1 || state == CarStates.brake || state == CarStates.reverse)
                {
                    state = CarStates.neutral;
                }
                break;
        }

        var steeringAngle = steeringInput * steeringCurve.Evaluate(speed);
        //if steering too high
        if (Math.Abs(steeringAngle) > 35f)
        {
            state = CarStates.harshTurn;
        }
        _sm.PlayCarSoundByState(state);
    }
    //call only when pressing forward
    private void SetCarStateGearDrive()
    {
        if (angle < 0)
        {
            state = CarStates.brake;
        }
        else
        {
            for (int i = 0; i < 5; i++)
            {
                if (speed > 5f * i)
                {
                    state = (CarStates)i;
                }
            }
        }
    }
}