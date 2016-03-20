using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour {

    public float MoveSpeed = 10f;
    public float TurnSpeed = 15f;

    private Rigidbody _myRigidbody;
    private Vector3 _movementForce = Vector3.zero;
    private Vector3 _rotationForce = Vector3.zero;

    // Use this for initialization
    void Start()
    {
        this._myRigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        this._movementForce = Vector3.forward * InputManager.Instance.VerticalInput * MoveSpeed * Time.deltaTime;

        this._rotationForce = new Vector3(
            0f,
            InputManager.Instance.HorizontalInput * TurnSpeed * Time.deltaTime,
            0f);

        transform.Translate(this._movementForce);
        //this._myRigidbody.AddForce(this._movementForce);
        this.transform.Rotate(this._rotationForce);

        this._movementForce = Vector3.zero;
        this._rotationForce = Vector3.zero;

    }
}
