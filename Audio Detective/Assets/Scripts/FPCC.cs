using UnityEngine;

public class FPCC : MonoBehaviour
{
    public float walkSpeed = 3f;
    public float runSpeed = 5f;
    public float rotationSpeed = 60f;
    public float mouseSensitivity = 1f;

    private bool _isRunning;
    private float _rotationX;
    private CharacterController _controller;

    void Start()
    {
        _controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        CheckIfRunning();
        Move();
        //Look();
    }

    void CheckIfRunning()
    {
        _isRunning = Input.GetKey(KeyCode.LeftShift);
    }
    
    void Move()
    {
        // Rotate around y axis
        // Since we're directly affecting the transform in the Update method, we need
        // to scale it with Time.deltaTime.
        float turn = Input.GetAxis("Horizontal") * rotationSpeed * Time.deltaTime;
        transform.Rotate(0, turn, 0);

        // Move forward / backward
        Vector3 forward = transform.TransformDirection(Vector3.forward);
        // This is a very short way of writing 'if _isRunning is true, speed = runSpeed
        // otherwise speed = walkSpeed'
        float speed = _isRunning ? runSpeed : walkSpeed;
        float curSpeed = speed * Input.GetAxis("Vertical");
        // SimpleMove doesn't require Time.DeltaTime, which you can only learn from
        // the documentation or by trial and error
        _controller.SimpleMove(forward * curSpeed);
    }

    void Look()
    {
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;
        _rotationX -= mouseY;  // Subtracting to invert the up and down look
        _rotationX = Mathf.Clamp(_rotationX, -90f, 90f);  // Clamp the up and down look to avoid flipping
        transform.localEulerAngles = new Vector3(_rotationX, transform.localEulerAngles.y, 0);
    }
}