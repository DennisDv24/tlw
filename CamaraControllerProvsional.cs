using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamaraControllerProvsional : MonoBehaviour {

public GameObject player;
float x;
public float cameraLim1;
public float cameraLim2;
public bool cameraMoving;

public bool itsy;

private bool con1;
private bool con2;

	void Update () {
		if(itsy == false){
			x = player.GetComponent<Transform>().position.x;
				if(x <= cameraLim1){
					transform.position = new Vector3 (cameraLim1,0,-10);
				}
				else if(x >= cameraLim2){
					transform.position = new Vector3 (cameraLim2,0,-10);
				}
				else{
					transform.position = new Vector3 (x,0,-10);

				}
					checkIfCameraIsMoving();
			}
		else{
			x = player.GetComponent<Transform>().position.y;
				if(x <= cameraLim1){
					transform.position = new Vector3 (0,cameraLim1,-10);
				}
				else if(x >= cameraLim2){
					transform.position = new Vector3 (0,cameraLim2,-10);
				}
				else{
					transform.position = new Vector3 (0,x,-10);

				}
					//checkIfCameraIsMoving();

		}
	}

	void checkIfCameraIsMoving(){
		if(player.GetComponent<Rigidbody2D>().velocity.x != 0 && x>=cameraLim1 && x<=cameraLim2){
			cameraMoving = true;
		}
		else{
			cameraMoving = false;
		}
	}
}
