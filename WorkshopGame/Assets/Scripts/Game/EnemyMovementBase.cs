using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public abstract class EnemyMovementBase : MonoBehaviour
{
    protected NavMeshAgent _nav;
    protected Vector3 _targetPos;

    [SerializeField]
    private Color _drawColor = Color.red;

    private void Awake()
    {
        this._nav = this.GetComponent<NavMeshAgent>();
    }

    protected virtual void OnDrawGizmos()
    {
        var old = Gizmos.color;
        Gizmos.color = _drawColor;
        Gizmos.DrawLine(this.transform.position, _targetPos);
        Gizmos.color = old;
    }
}
