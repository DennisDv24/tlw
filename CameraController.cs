using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

	public GameObject player;
	float x;
	void Start(){
		x = player.GetComponent<Transform>().position.x;
	}


	void Update(){
		
		x = player.GetComponent<Transform>().position.x;
			if (x > 127.1) {
				transform.position = new Vector3 (138f, 20f, -10);
			} 
			else if (x <= 127.1 && x >= 99) {
				transform.position = new Vector3 (109f, 19.6f, -10);
			} 
			else if (x < 99.1 && x >= 77.7) {
				transform.position = new Vector3 (88.4f, 1.1f, -10);
			} 
			else if (x > 50.5f && x < 77.6f) {
				transform.position = new Vector3 (66.85f, 18.894f, -10);
			}
		    else if (x >= 24f && x <= 50.5f) {
				transform.position = new Vector3 (40, 23.2f, -10);
			} 
			else if (x <= -19.2f) {
				transform.position = new Vector3 (-19.2f, 0.4f, -10);
			} 
			else {
				transform.position = new Vector3 (x,0.4f,-10);
			}
				//solucion provisonal.
	}





}
