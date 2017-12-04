using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    public Transform player;
    public Transform pivot;
    public Vector3 offset;
    public bool useOffset;
    public float rotateSpeed;

    void Start() {
        if (!useOffset) {
            offset = player.position - transform.position;
        }
        
        pivot.transform.position = player.transform.position;
        pivot.transform.parent = player.transform;

        // Remove the cursor icon and lock in the center of the window
        Cursor.lockState = CursorLockMode.Locked;
    }
    
    void LateUpdate() {
        float horizontal = Input.GetAxis("Mouse X") * rotateSpeed;
        player.Rotate(0, horizontal, 0);

        float vertical = Input.GetAxis("Mouse Y") * rotateSpeed;
        pivot.Rotate(-vertical, 0, 0);

        if (pivot.rotation.eulerAngles.x > 45f && pivot.rotation.eulerAngles.x < 180f) {
            pivot.rotation = Quaternion.Euler(45f, 0f, 0f);
        }

        float xAngle = player.eulerAngles.x;
        float yAngle = pivot.eulerAngles.y;
        Quaternion rotation = Quaternion.Euler(xAngle, yAngle, 0);

        transform.position = player.position - (rotation * offset);
        //transform.position = player.position - offset;
        if (transform.position.y < player.position.y) {
            float x = transform.position.x;
            float y = player.position.y - .5f;
            float z = transform.position.z;

            transform.position = new Vector3(x, y, z);
        }

        transform.LookAt(player);
    }
}
