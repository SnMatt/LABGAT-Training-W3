using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtCamera : MonoBehaviour
{
    private enum MODE
    {
        LookAt,
        LookAtInverted,
        CameraForward,
        CameraForwardInverted
    };
    [SerializeField] private MODE _mode;

    private void LateUpdate()
    {
        switch (_mode)
        {
            case MODE.LookAt:
                transform.LookAt(Camera.main.transform);
                break;
            case MODE.LookAtInverted:
                Vector3 dir = transform.position - Camera.main.transform.position;
                transform.LookAt(transform.position + dir);
                break;
            case MODE.CameraForward:
                transform.forward = Camera.main.transform.forward;
                break;
            case MODE.CameraForwardInverted:
                transform.forward = -Camera.main.transform.forward;
                break;
            default:
                break;
        }

        
    }
}
