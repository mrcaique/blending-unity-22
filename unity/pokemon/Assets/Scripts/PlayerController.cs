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
    public Animator anime;
    public Transform pivot;
    public float rotateSpeed;
    public GameObject heracrossModel;

    private Vector3 moveDirection;
    private int pokeballs;

    void Start() {
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
        pokecountText.text = "Pokéballs: " + count.ToString() + "/12";
        if (count >= 12) {
            winText.text = "You Win!";
        }
    }

    void Update() {
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
        float absVertical = Mathf.Abs(Input.GetAxis("Vertical"));
        float absHorizontal = Mathf.Abs(Input.GetAxis("Horizontal"));

        moveDirection.y = moveDirection.y + physics;
        controller.Move(moveDirection * Time.deltaTime);

        // Move player in different directions based on camera direction
        if (Input.GetAxis("Horizontal") != 0 ||
            Input.GetAxis("Vertical") != 0) {
            transform.rotation = Quaternion.Euler(
                    0f,
                    pivot.rotation.eulerAngles.y,
                    0f);
            Quaternion newRotation = Quaternion.LookRotation(new
                    Vector3(moveDirection.x, 0f, moveDirection.z));

            heracrossModel.transform.rotation =
                Quaternion.Slerp(heracrossModel.transform.rotation,
                        newRotation, rotateSpeed * Time.deltaTime);
        }

        anime.SetBool("isGrounded", controller.isGrounded);
        anime.SetFloat("speed", absVertical + absHorizontal);
    }
}
