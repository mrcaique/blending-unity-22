using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Moviment : MonoBehaviour {
  
    public float speed;
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
    }
}
