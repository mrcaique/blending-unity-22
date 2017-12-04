using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {
    
    public float moveSpeed;
    public float jumpForce;
    public CharacterController controller;
    public float gravityScale;
    public Text pokecountText;
    public Text winText;

    private Vector3 moveDirection;
    private int pokeballs;

    void Start() {
        //rb = GetComponent<Rigidbody>();
        controller = GetComponent<CharacterController>();
        pokeballs = 0;
        SetPokecountText(pokeballs);
        winText.text = "";
    }

    void OnTriggerEnter(Collider other) {
        if (other.gameObject.CompareTag("Pick Up")) {
            other.gameObject.SetActive(false);
            pokeballs++;
            SetPokecountText(pokeballs);
        } 
    }
    
    void SetPokecountText(int count) {
        pokecountText.text = "Pokéballs: " + count.ToString();
        if (count >= 11) {
            winText.text = "You Win!";
        }
    }

    void Update() {
        //float moveX = Input.GetAxis("Horizontal") * moveSpeed;
        //float moveY = moveDirection.y;
        //float moveZ = Input.GetAxis("Vertical") * moveSpeed;

        //rb.velocity = new Vector3(moveX, moveY, moveZ);

        // by default, jump is "space"
        //if (Input.GetButtonDown("Jump")) {
            //rb.velocity = new Vector3(rb.velocity.x, jumpForce, rb.velocity.z); 
        //}
        //moveDirection = new Vector3(moveX, moveY, moveZ);
        float yStore = moveDirection.y;
        Vector3 t1 = transform.forward * Input.GetAxis("Vertical");
        Vector3 t2 = transform.right * Input.GetAxis("Horizontal");
        
        moveDirection = t1 + t2;
        moveDirection = moveDirection.normalized * moveSpeed;
        moveDirection.y = yStore;

        if (controller.isGrounded) {
            moveDirection.y = 0f;

            if (Input.GetButtonDown("Jump")) {
                moveDirection.y = jumpForce;
            }
        }

        float physics = Physics.gravity.y * gravityScale * Time.deltaTime;
        moveDirection.y = moveDirection.y + physics;
        controller.Move(moveDirection * Time.deltaTime);
    }

    
    /*  
 *  public float speed;
    public Text countText;
    public Text winText;
    private Rigidbody rb; //! Holds the physics of the sphere
    private int count;

    void Start() {
        rb = GetComponent<Rigidbody>();
        count = 0;
        SetCountText(count);
        winText.text = "";
    }

    // physics codes
    void FixedUpdate() {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 moviment = new Vector3(moveHorizontal, 0.0f, moveVertical);

        rb.AddForce(moviment * speed);
    }

    void OnTriggerEnter(Collider other) {
        if (other.gameObject.CompareTag("Pick Up")) {
            other.gameObject.SetActive(false);
            count++;
            SetCountText(count);
        } 
    }
    
    void SetCountText(int count) {
        countText.text = "Count: " + count.ToString();
        if (count >= 11) {
            winText.text = "You Win!";
        }
    }*/
}
