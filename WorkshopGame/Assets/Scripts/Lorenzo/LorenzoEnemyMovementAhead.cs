using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class LorenzoEnemyMovementAhead : EnemyMovementBase
{
    [SerializeField]
    private LorenzoPlayerMovement _target;

    [SerializeField]
    private float _aheadModifier = 10f;

    [SerializeField]
    private float _updatePositionInterval = 0.5f;

    [SerializeField]
    private float _distanceToChase = 5f;

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
                Debug.LogWarning("EnemyMovementAhead did not destroy gracefully.");
                break;
            }
            // chase the enemy when within aggro range
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

}
