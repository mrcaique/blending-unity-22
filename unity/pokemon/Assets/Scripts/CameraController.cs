using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*!
 * Script for the camera in the game.
 */
public class CameraController : MonoBehaviour {

    public Transform player; //!< The protagonist, where the camera will focus
    public Transform pivot; //!< Camera that is child of the player
    public Vector3 offset; //!< (x, y, z) values defined by the user
    public bool useOffset; //!< Uses the values specified in 'offset' 
    public float rotateSpeed; //!< Speed of camera rotation

    /*!
     * First method to be executed
     */
    void Start() {
        if (!useOffset) {
            offset = player.position - transform.position;
        }
        
        // align the pivot camera with the player
        pivot.transform.position = player.transform.position;
        pivot.transform.parent = null;

        // Remove the cursor icon and lock in the center of the window
        Cursor.lockState = CursorLockMode.Locked;
    }
    
    /*!
     * Runs each frame, but it is the last to be executed.
     */
    void LateUpdate() {
        pivot.transform.position = player.transform.position;

        // Get the position of mouse cursor and rotates, that is,
        // rotate the camera in the horizontal with the mouse
        float horizontal = Input.GetAxis("Mouse X") * rotateSpeed;
        pivot.Rotate(0, horizontal, 0);

        // Get the position of mouse cursor and rotates, that is,
        // rotate the camera in the vertical with the mouse
        float vertical = Input.GetAxis("Mouse Y") * rotateSpeed;
        pivot.Rotate(-vertical, 0, 0);

        // Condition that prevents the camera to go beyond the floor and
        // to go beyond the top of the player
        if (pivot.rotation.eulerAngles.x > 45f && pivot.rotation.eulerAngles.x < 180f) {
            pivot.rotation = Quaternion.Euler(45f, 0f, 0f);
        }

        // Guarantee the consistency of the player's moviment when the camera
        // is rotating
        float xAngle = pivot.eulerAngles.x;
        float yAngle = pivot.eulerAngles.y;
        Quaternion rotation = Quaternion.Euler(xAngle, yAngle, 0);

        transform.position = player.position - (rotation * offset);

        if (transform.position.y < player.position.y) {
            float x = transform.position.x;
            float y = player.position.y - .5f;
            float z = transform.position.z;

            transform.position = new Vector3(x, y, z);
        }

        transform.LookAt(player);
    }
}
