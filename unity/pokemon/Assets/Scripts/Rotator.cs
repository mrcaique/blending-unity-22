using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*!
 *  Class for the behavior of the Pick-Ups Game Objects (Pokéballs) 
 */
public class Rotator : MonoBehaviour {

    /*!
     * Runs each frame
     */
	void Update () {
        // Rotate the object. Time.deltaTime is for a consistency moviment
        // in environments with low or high FPS
	    transform.Rotate(new Vector3(15, 30, 45) * Time.deltaTime);	
	}
}
