using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    public float MoveSpeed = 10f;
    public float TurnSpeed = 150f;
    public float JumpForce = 100f;

    private Rigidbody _myRigidbody;
    private Vector3 _movementForce = Vector3.zero;
    private Vector3 _rotationForce = Vector3.zero;
    private bool _canJump = true;

    // Use this for initialization
    void Start()
    {
        this._myRigidbody = GetComponent<Rigidbody>();
        this.JumpForce = 200f;
        this._canJump = true;
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

        this.transform.Rotate(this._rotationForce);

        this._movementForce = Vector3.zero;
        this._rotationForce = Vector3.zero;

        if(transform.position.y < -4f)
        {
            transform.position = new Vector3(0f, 2f, 0f);
        }

        if(InputManager.Instance.FireInput > float.Epsilon && this._canJump)
        {
            this._myRigidbody.AddForce(new Vector3(0f, JumpForce, 0f));
            this._canJump = false;
        }

    }

    void OnCollisionEnter(Collision obj)
    {
        if (obj.gameObject.tag == "floor" && transform.position.y == 1f)
        {
            _canJump = true;
        }
    }

    void OnCollisionStay(Collision obj)
    {
        if (obj.gameObject.tag == "floor" && transform.position.y == 1f)
        {
            _canJump = true;
        }
    }
}
