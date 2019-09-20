using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class EnemyMovement1 : MonoBehaviour
{
    private NavMeshAgent _nav;
    private Vector3 _targetPos;

    [SerializeField]
    private Transform _target;

    [SerializeField]
    private Color _drawColor = Color.red;

    private void Awake()
    {
        this._nav = this.GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        this._targetPos = this._target.position;
        this._nav.SetDestination(this._targetPos);
    }


    private void OnDrawGizmos()
    {
        var old = Gizmos.color;
        Gizmos.color = _drawColor;
        Gizmos.DrawLine(this.transform.position, _targetPos);
        Gizmos.color = old;
    }
}
