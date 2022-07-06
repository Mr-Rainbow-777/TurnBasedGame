using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class UnitRagdollSpawner : MonoBehaviour
{
    [SerializeField] private Transform ragdollPrefab;
    [SerializeField] private Transform originalRootBone;


    private HealthSystem healthSystem;


    private void Awake()
    {
        healthSystem = GetComponent<HealthSystem>();

        healthSystem.OnDead += HealthSystem_OnDead;
    }

    private void HealthSystem_OnDead(object sender, System.EventArgs e)
    {
        Transform ragdollTrans=Instantiate(ragdollPrefab, transform.position, Quaternion.identity);
        UnitRagDoll ragdoll = ragdollTrans.GetComponent<UnitRagDoll>();
        ragdoll.SetUp(originalRootBone);
    }


}
