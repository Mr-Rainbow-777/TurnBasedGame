using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletProjectile : MonoBehaviour
{
    [SerializeField] private TrailRenderer trailRender;
    [SerializeField] private Transform BulletVFX;
    private Vector3 targetPos;
    public void SetUp(Vector3 targetPos)
    {
        this.targetPos = targetPos;
    }

    private void Update()
    {
        Vector3 moveDir = (targetPos - transform.position).normalized;
        float distanceBeforeMoving = Vector3.Distance(transform.position, targetPos);
        float MoveSpeed = 200;
        transform.position += moveDir * MoveSpeed * Time.deltaTime;

        float DistanceAfterMoving = Vector3.Distance(transform.position, targetPos);
        if(distanceBeforeMoving<DistanceAfterMoving)
        {
            transform.position = targetPos;
            trailRender.transform.parent = null;
            Destroy(gameObject);

            Instantiate(BulletVFX, targetPos, Quaternion.identity);
        }
    }
}
