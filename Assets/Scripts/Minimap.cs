using System;
using UnityEngine;

public class Minimap : MonoBehaviour
{
    [SerializeField]
    private Transform player;

    [SerializeField] 
    private Transform target;

    [SerializeField] 
    private RectTransform destImg;

    private Camera miniCamera;
    private const float Radius = 85f;
    private const float SquareLength = 180f;


    void Start()
    {
        miniCamera = GetComponent<Camera>();
    }


    void Update()
    {
        Vector3 viewPos = miniCamera.WorldToViewportPoint(target.position);
        if (viewPos.x > 0 && viewPos.x <= 1 && viewPos.y >= 0 && viewPos.y <= 1 && viewPos.z > 0)
        {
            destImg.gameObject.SetActive(false);
        }
        else
        {
            destImg.gameObject.SetActive(true);
            var dir = (target.transform.position - player.transform.position).normalized;
            var angle = Vector3.SignedAngle(player.transform.forward, dir, Vector3.down);
            ////0 to 360 angle
            var fullAngle = Quaternion.Euler(0f, angle, 0f).eulerAngles.y;
            // adjust initial rotation
            fullAngle -= 270f;
            // Ensure angle is within the range of 0 to 360 degrees
            if (fullAngle < 0f)
            {
                fullAngle += 360f;
            }
            destImg.localPosition = MapToSquare(fullAngle, Radius, SquareLength);
            // print(fullAngle);
        }
    }

    void LateUpdate()
    {
        var newPos = player.position;
        newPos.y = transform.position.y;
        transform.position = newPos;

        transform.rotation = Quaternion.Euler(90f, player.eulerAngles.y, 0f);
    }

    static Vector3 MapToSquare(float angle, float radius, float squareSideLength)
    {
        // Convert angle to radians
        float theta = angle * Mathf.Deg2Rad;

        // Calculate the coordinates on the square
        float x, y;

        // Map the circle on the square
        // Determine which side of the square the point falls on
        if (theta >= 0f && theta < Mathf.PI / 4f)
        {
            x = radius;
            y = Mathf.Tan(theta) * radius;
        }
        else if (theta >= Mathf.PI / 4f && theta < 3 * Mathf.PI / 4f)
        {
            y = radius;
            x = 1 / Mathf.Tan(theta) * radius;
        }
        else if (theta >= 3 * Mathf.PI / 4f && theta < 5 * Mathf.PI / 4f)
        {
            x = -radius;
            y = -Mathf.Tan(theta) * radius;
        }
        else if (theta >= 5 * Mathf.PI / 4f && theta < 7 * Mathf.PI / 4f)
        {
            y = -radius;
            x = -1 / Mathf.Tan(theta) * radius;
        }
        else
        {
            x = radius;
            y = Mathf.Tan(theta) * radius;
        }

        return new Vector3(x, y);
    }

}
