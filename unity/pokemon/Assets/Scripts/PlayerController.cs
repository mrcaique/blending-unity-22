using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {
    
    public float moveSpeed; //!< Speed of moviment
    public float jumpForce; //!< How much the high is the jump
    public CharacterController controller; //!< Player's controller
    public float gravityScale; //!< gravity force
    public Text pokecountText; //!< Pokéballs counter
    public Text winText; //!< "You Win!" text
    public Animator anime; //!< Related to the animation of the player
    public Transform pivot; //!< Camera that is child of the player
    public float rotateSpeed; //!< speed of camera rotation
    public GameObject heracrossModel; //!< model used for the player

    private Vector3 moveDirection; //!< Direction of the moviment
    private int pokeballs; //!< Number of pokéballs collected

    /*!
     * First method to be executed
     */
    void Start() {
        controller = GetComponent<CharacterController>();
        pokeballs = 0;
        SetPokecountText(pokeballs);
        winText.text = "";
    }

    /*!
     * Collision detector: if the player touches a pokéball, the pokéball
     * "vanishes" and the counter of pokéballs is increased, that is, the
     * player collected that pokéball.
     *
     * @param other: Another game object that collided with the player
     */
    void OnTriggerEnter(Collider other) {
        if (other.gameObject.CompareTag("Pick Up")) {
            other.gameObject.SetActive(false);
            pokeballs++;
            SetPokecountText(pokeballs);
        } 
    }
    
    /*!
     * Updates the pokéball counter. If all pokéballs are collected, the
     * victory message will display
     */
    void SetPokecountText(int count) {
        pokecountText.text = "Pokéballs: " + count.ToString() + "/25";
        if (count >= 25) {
            winText.text = "You Win!";
        }
    }

    /*!
     * Runs each frame
     */
    void Update() {
        float yStore = moveDirection.y;
        Vector3 t1 = transform.forward * Input.GetAxis("Vertical");
        Vector3 t2 = transform.right * Input.GetAxis("Horizontal");
        
        moveDirection = t1 + t2;
        moveDirection = moveDirection.normalized * moveSpeed;
        moveDirection.y = yStore;

        // Prevents the player jumps forever (double jumps, triple jumps, etc.)
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

        // Move player in different directions based on camera direction,
        // that is, if the player go to the right, the correct animation is
        // displayed.
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

        // Set the respectives variables for the animation's finite state
        anime.SetBool("isGrounded", controller.isGrounded);
        anime.SetFloat("speed", absVertical + absHorizontal);

        if (Input.GetKey("escape")) {
            Application.Quit();
        }
    }
}
