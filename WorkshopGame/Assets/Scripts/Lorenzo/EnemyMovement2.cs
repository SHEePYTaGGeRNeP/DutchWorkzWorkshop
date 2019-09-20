using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class EnemyMovement2 : MonoBehaviour
{
    private NavMeshAgent _nav;

    private Vector3 _targetPos;

    [SerializeField]
    private PlayerMovement _target;

    [SerializeField]
    private float _aheadModifier = 10f;

    [SerializeField]
    private float _updatePositionInterval = 0.5f;

    [SerializeField]
    private float _distanceToChase = 2f;

    [SerializeField]
    private Color _drawColor = Color.green;

    private void Awake()
    {
        this._nav = this.GetComponent<NavMeshAgent>();
    }
    private void Start()
    {
        this.StartCoroutine(SetPosition());
    }
    private IEnumerator SetPosition()
    {
        while (this.isActiveAndEnabled)
        {
            if (this._target == null)
            {
                Debug.LogWarning("EnemyMovement2 did not destroy gracefully.");
                break;
            }
            if (Vector3.Distance(this._target.transform.position, this.transform.position) < this._distanceToChase)
            {
                this._targetPos = this._target.transform.position;
                _nav.SetDestination(this._targetPos);
                // wait for next frame
                yield return null;
            }
            else
            {
                this._targetPos = this._target.transform.position + (this._target.Direction * _aheadModifier);
                _nav.SetDestination(this._targetPos);
                yield return new WaitForSeconds(this._updatePositionInterval);
            }
        }
    }


    void OnDestroy()
    {
        this.StopCoroutine(this.SetPosition());
    }

    private void OnDrawGizmos()
    {
        var old = Gizmos.color;
        Gizmos.color = _drawColor;
        Gizmos.DrawLine(this.transform.position, _targetPos);
        Gizmos.color = old;
    }
}
