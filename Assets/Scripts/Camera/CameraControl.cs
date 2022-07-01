using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraControl : MonoBehaviour
{
    private const float MIN_FOLLOW_Y_OFFSET = 2f;
    private const float MAX_FOLLOW_Y_OFFSET = 12f;

    [SerializeField] private CinemachineVirtualCamera m_virtualcamera;
    [Header("Variable")]
    public float MoveSpeed = 20f;
    public float RotateSpeed = 100f;
    public float ZoomAmount = 1f; 

    private CinemachineTransposer m_Transposer;
    private Vector3 m_CameraOffset;
    public void Start()
    {
        m_Transposer = m_virtualcamera.GetCinemachineComponent<CinemachineTransposer>();
        m_CameraOffset = m_Transposer.m_FollowOffset;
    }

    private void Update()
    {
        CameraMove();
        CameraRotate();
        CameraZoom();
    }
    private void CameraMove()
    {
        Vector3 InputMoveDir = new Vector3(0, 0, 0);
        if(Input.GetKey(KeyCode.W))
        {
            InputMoveDir += Vector3.forward;
        }
        if (Input.GetKey(KeyCode.S))
        {
            InputMoveDir += Vector3.back;
        }
        if (Input.GetKey(KeyCode.A))
        {
            InputMoveDir += Vector3.left;
        }
        if (Input.GetKey(KeyCode.D))
        {
            InputMoveDir += Vector3.right;
        }
        Vector3 MoveDir = (transform.forward * InputMoveDir.z + transform.right * InputMoveDir.x).normalized;
        this.transform.position += MoveDir * MoveSpeed * Time.deltaTime;
    }

    private void CameraRotate()
    {
        Vector3 InputRotateDir = new Vector3(0, 0, 0);
        if (Input.GetKey(KeyCode.Q))
        {
            InputRotateDir += Vector3.up;
        }
        if (Input.GetKey(KeyCode.E))
        {
            InputRotateDir += Vector3.down;
        }

        this.transform.eulerAngles += InputRotateDir.normalized *RotateSpeed * Time.deltaTime;
    }

    private void CameraZoom()
    {
        if(Input.mouseScrollDelta.y>0)
        {
            m_CameraOffset.y -= ZoomAmount;
        }
        if (Input.mouseScrollDelta.y < 0)
        {
            m_CameraOffset.y += ZoomAmount;
        }
        m_CameraOffset.y = Mathf.Clamp(m_CameraOffset.y, MIN_FOLLOW_Y_OFFSET, MAX_FOLLOW_Y_OFFSET);
        m_Transposer.m_FollowOffset = Vector3.Lerp(m_Transposer.m_FollowOffset, m_CameraOffset, ZoomAmount * Time.deltaTime);
    }
}
