using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementProto : MonoBehaviour
{
    [SerializeField]
    float movementSpeed  = 30;

    [SerializeField]
    float rotateSpeed = 20;


    private float vInput;
    private float hInput;

    private Rigidbody _rb;


    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {

        /*
        transform.Translate(new Vector3(0, -movement, 0) * movementSpeed * Time.deltaTime);
        transform.Rotate(new Vector3(0, 0, turn ) * rotateSpeed * Time.deltaTime);
        */
    }

    private void FixedUpdate()
    {


        float movement = Input.GetAxis("Vertical") * movementSpeed;
        float turn = Input.GetAxis("Horizontal");

        Vector3 rotation = Vector3.up * turn * rotateSpeed;

        Quaternion angleRot = Quaternion.Euler(rotation * Time.fixedDeltaTime);

        _rb.MovePosition(this.transform.position + this.transform.forward * movement * Time.fixedDeltaTime);

        _rb.MoveRotation(_rb.rotation * angleRot);


    }
}

