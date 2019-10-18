using UnityEngine;

public class EnemyMovementChase : EnemyMovementBase
{
    [SerializeField]
    private Transform _target;

    // Update is called once per frame
    void Update()
    {
        this._targetPos = this._target.position;
        this._nav.SetDestination(this._targetPos);
    }

}
