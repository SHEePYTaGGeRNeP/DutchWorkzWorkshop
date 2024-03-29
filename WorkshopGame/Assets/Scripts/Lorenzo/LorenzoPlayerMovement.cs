﻿using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class LorenzoPlayerMovement : MonoBehaviour
{
    private Rigidbody _rb;

    [SerializeField]
    private float _speed = 0.1f;

    // Since we don't rotate, the forward vector stays the same.
    public Vector3 Direction { get; private set; }

    // Start is called before the first frame update
    void Awake()
    {
        this._rb = this.GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        Direction = Vector3.ClampMagnitude(new Vector3(x, 0, z), 1f);
        this._rb.MovePosition(this.transform.position + (new Vector3(x, 0, z) * this._speed));
    }

}
