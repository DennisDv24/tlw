using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxControllerLimited : MonoBehaviour {

public float parallaxGapGradient = 2;
public GameObject player;
public GameObject temporalCamera;
private bool cameraIsMoving;
private float velocity;

	void Update () {
		velocity = player.GetComponent<Rigidbody2D>().velocity.x;
		cameraIsMoving = temporalCamera.GetComponent<CamaraControllerProvsional>().cameraMoving;
		calculateParallaxMov();
	}

	void calculateParallaxMov(){
		if(cameraIsMoving){
			transform.position = new Vector3(transform.position.x + (velocity / parallaxGapGradient), transform.position.y, transform.position.z);
		}
	}
}
