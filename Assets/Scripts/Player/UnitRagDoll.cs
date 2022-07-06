using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitRagDoll : MonoBehaviour
{
    [SerializeField] private Transform ragdollRootBone;


    public void SetUp(Transform originalRootBone)
    {
        MatchAllChildTransforms(originalRootBone, ragdollRootBone);

        ApplyExplosionToRagDoll(ragdollRootBone, 300f, transform.position, 10f);
    }

    private void MatchAllChildTransforms(Transform root,Transform clone)
    {
        foreach (Transform child in root)
        {
            Transform clonechild = clone.Find(child.name);
            if(clonechild != null)
            {
                clonechild.position = child.position;
                clonechild.rotation = child.rotation;

                MatchAllChildTransforms(child, clonechild);
            }
        }
    }

    private void ApplyExplosionToRagDoll(Transform root,float explosionForce,Vector3 explosionPostion,float explosionRange)
    {
        foreach (Transform child in root)
        {
            if(child.TryGetComponent<Rigidbody>(out Rigidbody childRigidBody))
            {
                childRigidBody.AddExplosionForce(explosionForce, explosionPostion,explosionRange);
            }
            ApplyExplosionToRagDoll(child,explosionForce,explosionPostion,explosionRange);
        }
    }

}
