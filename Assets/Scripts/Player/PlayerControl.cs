using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{

    [Header("Variable")]
    const float stopDistance = .1f;
    public float MoveSpeed = 3f;
    public float RotateSpeed = 10f;

    private Animator _animator;    
    public Vector3 targetPosition;
    private GridPosition _gridPosition;
    // Start is called before the first frame update
    void Start()
    {
        targetPosition=this.transform.position;
        _animator = GetComponentInChildren<Animator>();
        _gridPosition = LevelGrid.Instance.GetGridPosition(transform.position);
        LevelGrid.Instance.SetUnitAtGridPostion(_gridPosition, this);
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(transform.position, targetPosition) > stopDistance)
        {
            Vector3 moveDirection = (targetPosition - transform.position).normalized;
            transform.position += moveDirection * MoveSpeed * Time.deltaTime;

            transform.forward = Vector3.Lerp(transform.forward, moveDirection, Time.deltaTime * RotateSpeed);
            _animator.SetBool("IsWalking", true);
        }
        else
        {
            _animator.SetBool("IsWalking", false);
        }

        GridPosition newGridpos = LevelGrid.Instance.GetGridPosition(transform.position);
        if(newGridpos!=_gridPosition)
        {
            LevelGrid.Instance.UnitMovedGridPosition(this, newGridpos, _gridPosition);
            _gridPosition=newGridpos; 
        }
    }


    public void Move(Vector3 targetpostion)
    {
        this.targetPosition = targetpostion;
    }
}
