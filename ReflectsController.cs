using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReflectsController : MonoBehaviour {

float reflectsGapGradient = 30f;
public GameObject player;
public GameObject temporalCamera;
private bool cameraIsMoving;
private float velocity;
float absolutePos= 0;
	
	void Update () {
		velocity = player.GetComponent<Rigidbody2D>().velocity.x;
		cameraIsMoving = temporalCamera.GetComponent<CamaraControllerProvsional>().cameraMoving;
		calculateParallaxMov();
	}

	void calculateParallaxMov(){
		if(cameraIsMoving){
			GetComponent<MeshRenderer>().material.mainTextureOffset = new Vector2(absolutePos,0);
			absolutePos += (velocity/reflectsGapGradient)/100;
		}
	}
}
