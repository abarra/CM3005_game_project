using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Events;

public class PlayerController : MonoBehaviour
{

    UnityEvent gearShifted;
    GameManager gameManager;

    float mult = 1000f;
    float extraGravity = -10f;
    float flyingDrag = .1f;
    float groundedDrag = 4f;

    [SerializeField] Rigidbody rb;
    [SerializeField] SphereCollider carCol;

    [SerializeField] LayerMask groundLayer;
    [SerializeField] GameObject groundRay;
    [SerializeField] float groundRayLength = 1000f;

    float maxSpeed = 12f;
    float minSpeed = -4f;
    float rawAccelFwd = 0.015f;
    float rawAccelBwd = 0.005f;
    float rawDecel = .03f;

    float accelFwd;
    float accelBwd;
    float decel;
    
    [SerializeField][Range(0f, 360f)] float turning = 180f;
    bool reversing = false;
    bool hasStopped = false;

    [SerializeField] GameObject[] wheels;
    int gear = 1;
    float wheelTurn = 25f;
    float forwardValue = 0f;
    float turnDir = 0f;
    float minTurn = 20f;
    float maxTurn = 180f;

    bool isGrounded = false;
    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        rb.transform.parent = null;
        
        gearShifted = new UnityEvent();
        gearShifted.AddListener(setHUD);

        ShiftGear();
    }

    // Update is called once per frame
    void Update()
    {
        ShiftGear();
        if (Input.GetAxisRaw("Vertical") != 0)
        {
            Drive();
        }
        else
        {
            float threshold = 1f * mult;
            if(forwardValue < -threshold)
            {
                forwardValue += decel * mult;
            }
            else if(forwardValue > threshold){
                forwardValue -= decel * mult;
            }
            else
            {
                forwardValue = 0f;
            }
            gearShifted.Invoke();
        }

        turnDir = Input.GetAxisRaw("Horizontal");

        float offsetY = carCol.radius;

        turning = (180 / maxSpeed) * (maxSpeed - (forwardValue /mult)*0.9f);

        turning = Mathf.Clamp(turning, minTurn, maxTurn);

        transform.position = new Vector3(rb.transform.position.x, rb.transform.position.y - offsetY, rb.transform.position.z);
        if (isGrounded)
        {
            if (forwardValue > 0f)
            {
                transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles + new Vector3(
                0f,
                turnDir * turning * Time.deltaTime,
                0f));
            }
            else if (forwardValue < 0f)
            {
                transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles + new Vector3(
               0f,
               turnDir * turning * Time.deltaTime * (-1),
               0f));
            }

        }
        rb.rotation = transform.rotation;

        for (int i = 0; i < wheels.Length; i++)
        {
            float xRot = wheels[i].transform.localRotation.eulerAngles.x;
            float zRot = wheels[i].transform.localRotation.eulerAngles.z;
            if (i % 2 == 0)
            {
                wheels[i].transform.localRotation = Quaternion.Euler(xRot, Time.timeScale * (turnDir * wheelTurn) - 180, zRot);
            }
            else
            {
                wheels[i].transform.localRotation = Quaternion.Euler(xRot, Time.timeScale * turnDir * wheelTurn, zRot);
            }
        }

    }

    void FixedUpdate()
    {
        RaycastHit hit;
        Debug.DrawRay(groundRay.transform.position, -transform.up, Color.red, groundRayLength, true);

        if (Physics.Raycast(groundRay.transform.position, -transform.up, out hit, groundRayLength, groundLayer))
        {
            isGrounded = true;
            rb.drag = groundedDrag;
            transform.rotation = Quaternion.FromToRotation(transform.up, hit.normal) * transform.rotation;
        }
        else
        {
            isGrounded = false;
            rb.drag = flyingDrag;
        }

        if (isGrounded)
        {
            if (Mathf.Abs(forwardValue) != 0)
            {
                rb.AddForce(rb.transform.forward * forwardValue);
            }
        }
        else
        {
            rb.AddForce(Vector3.up * extraGravity);
        }
    }

    void Drive()
    {
        float dir = Input.GetAxisRaw("Vertical");
        if (dir > 0)
        {
            dir *= accelFwd;
            reversing = false;
        }
        else
        {
            dir *= accelBwd;
            reversing = true;
        }

        forwardValue += dir * mult;

        if (forwardValue > 0 && forwardValue > maxSpeed * mult)
        {
            forwardValue = maxSpeed * mult;
        }
        if (forwardValue < 0 && forwardValue < minSpeed * mult)
        {
            forwardValue = minSpeed * mult;
        }
        gearShifted.Invoke();
    }

    void ShiftGear()
    {
        int toShift = 1;
        float max = maxSpeed * mult;
        for (int i = 2; i <= 5; i++)
        {
            float partition = max * ((float)i / 5f);
            if(forwardValue >= partition)
            {
                Debug.Log($"ShiftGear:Chagnge in for loop:\nFWV: {forwardValue}\nMax: {max}\nPartition:{partition}\ntoShift: {toShift}\nNewGear: {i}");
                toShift = i;
            }
        }
        if (gear != toShift)
        {
            Debug.Log($"toShift: {toShift}\ngear: {gear}");
            gear = toShift;
            gearShifted.Invoke();
        }
        setCarParams();
    }

    //accel should rise with gear,decel should lower
    void setCarParams()
    {

        accelFwd = (float)gear * rawAccelFwd;
        accelBwd = (float)gear * rawAccelBwd;
        decel = rawDecel / (float)gear;
        if (forwardValue > 0f)
        {
            accelBwd += 2f*decel;
        }
    }

    void setHUD()
    {
        string toSend = gear.ToString();
        if(forwardValue < 0f)
        {
            toSend = "R";
        }
        if(forwardValue == 0f)
        {
            toSend = "N";
        }
        gameManager.SetHUDGear(toSend, 5);
    }
}
