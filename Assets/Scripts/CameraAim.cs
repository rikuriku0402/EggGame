using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraAim : MonoBehaviour
{
    [SerializeField]
    [Header("�p�x����")]
    private float _viewAngle;

    private float _inputX, _inputY;

    void Update()
    {
        _inputX = Input.GetAxis("Mouse X");
        _inputY = Input.GetAxis("Mouse Y");

        Rotate(_inputX, _inputY, _viewAngle);
    }

    void Rotate(float _inputX, float _inputY, float limit)
    {
        float maxLimit = limit, minLimit = 360 - maxLimit;

        //X����]
        var localAngle = transform.localEulerAngles;
        localAngle.x += _inputY;
        if (localAngle.x > maxLimit && localAngle.x < 180)
        {
            localAngle.x = maxLimit;
        }

        if (localAngle.x < minLimit && localAngle.x > 180)
        {
            localAngle.x = minLimit;
        }

        transform.localEulerAngles = localAngle;
        //Y����]
        var angle = transform.eulerAngles;
        angle.y += _inputX;
        transform.eulerAngles = angle;
    }
}