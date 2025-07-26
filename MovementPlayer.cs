using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
public class MovementPlayer : MonoBehaviour
{

    [SerializeField] private float moveSpeed = 7f;
    [SerializeField] private float jumpForce = 8f;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;

    // Variabel internal
    private Rigidbody rb;
    private Vector3 moveInput;
    private bool isGrounded;


    void Start()
    {

        rb = GetComponent<Rigidbody>();
    }


    void Update()
    {

        isGrounded = Physics.CheckSphere(groundCheck.position, 0.2f, groundLayer);


        moveInput = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical"));


        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {

            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }


    void FixedUpdate()
    {
        Vector3 targetVelocity = moveInput.normalized * moveSpeed;
        rb.linearVelocity = new Vector3(targetVelocity.x, rb.linearVelocity.y, targetVelocity.z);

        float moveHorizontal = Input.GetAxis("Horizontal"); // A, D
        float moveVertical = Input.GetAxis("Vertical");     // W, S

        Vector3 movement = new Vector3(moveHorizontal, 0f, moveVertical);

        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);

        if (movement != Vector3.zero)
        {
            Quaternion toRotation = Quaternion.LookRotation(movement, Vector3.up);
            rb.rotation = Quaternion.RotateTowards(rb.rotation, toRotation, 720 * Time.fixedDeltaTime);
        }
    }
    public void GameOver()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}


