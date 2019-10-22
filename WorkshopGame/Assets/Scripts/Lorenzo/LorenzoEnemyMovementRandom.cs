using System.Collections;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class LorenzoEnemyMovementRandom : EnemyMovementBase
{
    [SerializeField]
    private MeshRenderer _ground;

    [SerializeField]
    private Transform _player;

    [SerializeField]
    private float _distanceToChase = 5f;

    [SerializeField]
    private float _wanderDistance = 10f;

    private void Start()
    {
        SetNextRandomPosition();
        this.StartCoroutine(SetPosition());
    }

    private IEnumerator SetPosition()
    {
        while (this.isActiveAndEnabled)
        {
            if (this._player == null)
            {
                Debug.LogWarning("EnemyMovementRandom did not destroy gracefully.");
                break;
            }
            if (Vector3.Distance(this._player.transform.position, this.transform.position) < this._distanceToChase)
            {
                this._targetPos = this._player.transform.position;
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

}
