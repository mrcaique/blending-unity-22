using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Animator))]
public class RootMotionScript : MonoBehaviour {
            
    /*!
     * Specific for animations: Guarantee that the model will stay "idle" in
     * the origin position
     */
    void OnAnimatorMove() {
        Animator animator = GetComponent<Animator>(); 
                              
        if (animator) {
            Vector3 newPosition = transform.position;
            newPosition.z += 0f * Time.deltaTime; 
            transform.position = newPosition;
        }
    }
}
