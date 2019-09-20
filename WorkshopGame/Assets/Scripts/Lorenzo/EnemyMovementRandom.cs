using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class EnemyMovementRandom : MonoBehaviour
{
    private NavMeshAgent _nav;

    private Vector3 _targetPos;

    [SerializeField]
    private PlayerMovement _target;

    [SerializeField]
    private MeshRenderer _ground;
    
    [SerializeField]
    private float _distanceToChase = 5f;

    [SerializeField]
    private Color _drawColor = Color.green;

    [SerializeField]
    private float _wanderDistance = 10f;

    private void Awake()
    {
        this._nav = this.GetComponent<NavMeshAgent>();
    }
    private void Start()
    {
        SetNextRandomPosition();
        this.StartCoroutine(SetPosition());
    }
    private IEnumerator SetPosition()
    {
        while (this.isActiveAndEnabled)
        {
            if (this._target == null)
            {
                Debug.LogWarning("EnemyMovementRandom did not destroy gracefully.");
                break;
            }
            if (Vector3.Distance(this._target.transform.position, this.transform.position) < this._distanceToChase)
            {
                this._targetPos = this._target.transform.position;
                _nav.SetDestination(this._targetPos);
                // wait for next frame
                yield return null;
            }
            else if (Vector3.Distance(this.transform.position, this._targetPos) < 2f)
            {
                SetNextRandomPosition();
                yield return null;
            }
            else
                yield return new WaitForSeconds(0.1f);
        }
    }

    private void SetNextRandomPosition()
    {
        Bounds b = this._ground.bounds;
        float x = Random.Range(b.min.x, b.max.x);
        float z = Random.Range(b.min.z, b.max.z);
        NavMesh.SamplePosition(new Vector3(x, 0, z), out NavMeshHit hit, _wanderDistance, 1);
        this._targetPos = hit.position;
        _nav.SetDestination(this._targetPos);

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
