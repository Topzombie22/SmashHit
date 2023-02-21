using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsCharacterController : MonoBehaviour
{
    private Rigidbody rb;
    [SerializeField]
    private GameObject cam;

    private float xInput;
    private float zInput;
    public float xSensativity;
    public float ySensativity;
    private float camX;
    private float camY;
    private float rotation;
    public Vector3 speedCap;
    public float speed;
    public float rbVelocity;
    public Vector3 playerInput;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        CamMovement();
        PushPlayer();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        MovePlayer();
    }

    private void MovePlayer()
    {
        zInput = Input.GetAxisRaw("Horizontal");
        xInput = Input.GetAxisRaw("Vertical");
        playerInput = new Vector3(zInput, 0f, xInput);
        rb.MovePosition(rb.position += transform.TransformDirection(playerInput.normalized) * speed * Time.fixedDeltaTime);
    }

    void CamMovement()
    {
        camX += xSensativity * Input.GetAxis("Mouse X");
        camY += -ySensativity * Input.GetAxis("Mouse Y");

        camY = Mathf.Clamp(camY, -89f, 89);

        transform.rotation = Quaternion.Euler(0f, camX, 0f);
        cam.transform.rotation = Quaternion.Euler(camY, camX, 0f);
    }
    void PushPlayer()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            rb.AddForce(transform.TransformDirection(Vector3.back + Vector3.up) * 50, ForceMode.Impulse);
        }
    }
}
