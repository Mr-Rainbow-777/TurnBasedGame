using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    [SerializeField] private GameObject ActionCameraGameObj;


    private void Start()
    {
        BaseAction.OnAnyActionStarted += BaseAction_OnAnyActionStarted;
        BaseAction.OnAnyActionCompleted += BaseAction_OnAnyActionCompleted;

        HideActionCamera();
    }

    private void BaseAction_OnAnyActionCompleted(object sender, System.EventArgs e)
    {
        switch (sender)
        {
            case ShootAction shootAction:
                HideActionCamera();
                break;
            default:
                break;
        }
    }

    private void BaseAction_OnAnyActionStarted(object sender, System.EventArgs e)
    {
        switch (sender)
        {
            case ShootAction shootAction:
                Unit shootUnit = shootAction.GetUnit();
                Unit targetUnit = shootAction.GetTargetUnit();

                Vector3 cameraCharacterHeight = Vector3.up * 1.7f;
                Vector3 shootDir = (targetUnit.GetWorldPos() - shootUnit.GetWorldPos()).normalized;
                float shoulderOffsetAmount = 0.5f;
                Vector3 shoulderOffset = Quaternion.Euler(0, 90, 0) * shootDir * shoulderOffsetAmount;

                Vector3 actionCameraPos =
                shootUnit.GetWorldPos()
                + cameraCharacterHeight
                + shoulderOffset
                + (shootDir * -1);

                ActionCameraGameObj.transform.position = actionCameraPos;
                ActionCameraGameObj.transform.LookAt(targetUnit.GetWorldPos() + cameraCharacterHeight);
                ShowActionCamera();
                break;
            default:
                break;
        }
    }

    private void ShowActionCamera()
    {
        ActionCameraGameObj.SetActive(true);
    }

    private void HideActionCamera()
    {
        ActionCameraGameObj.SetActive(false);

    }
}
