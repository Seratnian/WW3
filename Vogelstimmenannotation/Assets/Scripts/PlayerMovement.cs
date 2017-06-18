using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]private GameObject _camera;

    [Range(0.1f,10)]public float WalkSpeed;
    [Range(1, 5)] public float RotationSpeed;
    private Vector3 _direction, _cameraRotation;
    private float _mouseX, _mouseY;

    public bool Invert;

	private void Start ()
	{
	    WalkSpeed = 5;
	    RotationSpeed = 5;
	}
	
	private void Update ()
	{
        //if (!Input.anyKey) return;

	    //if (Input.GetKey(KeyCode.Mouse1))
	        Rotate();

        if (Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0)
        {
            CalculateMovement();
            Move();
        }
	}

    private void CalculateMovement()
    {
        _direction = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));

        _cameraRotation = _camera.transform.eulerAngles;
        _cameraRotation.x = 0;

        _direction = Vector3.Normalize(Quaternion.Euler(_cameraRotation) * _direction);
    }

    private void Move()
    {
        transform.Translate(_direction * WalkSpeed * Time.deltaTime);
    }

    private void Rotate()
    {
        if (Invert) _mouseY += RotationSpeed * Input.GetAxis("Mouse Y");
        else _mouseY -= RotationSpeed * Input.GetAxis("Mouse Y");
        _mouseX += RotationSpeed * Input.GetAxis("Mouse X");

        _mouseY = Mathf.Clamp(_mouseY, -90f, 90f);

        while (_mouseX < 0f)
        {
            _mouseX += 360f;
        }
        while (_mouseX >= 360f)
        {
            _mouseX -= 360f;
        }

        _camera.transform.eulerAngles = new Vector3(_mouseY, _mouseX, 0f);
    }
}
