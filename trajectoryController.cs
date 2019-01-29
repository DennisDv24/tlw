using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class trajectoryController : MonoBehaviour {
    Transform transform;

	public float trayectoryVelocity = 0.125f;

	public const int stoppedMaxTiming = 40;
	private int stoppedTiming = stoppedMaxTiming;

	public const int movingMaxTiminig = 6;
	private int movingTiming = movingMaxTiminig;

	public bool canMovInX = false;
	public bool canMovInY = false;

	void Start () {
		transform = GetComponent<Transform>();
	}

private float sum = 0;
public float alternador = 1;
public float desfaseTimingStopped = 0;
public float desfaseTimingMoving = 0;
	void FixedUpdate(){
		xMov();
		yMov();
	}

	void xMov(){
		if(canMovInX){
			if((stoppedTiming + desfaseTimingStopped)==0){
					transform.position = new Vector3(transform.position.x + sum,transform.position.y,transform.position.z);
					sum=sum+trayectoryVelocity*alternador;
						if((movingTiming+desfaseTimingMoving)==0){
							sum=0;
							movingTiming = movingMaxTiminig;
						 	stoppedTiming=stoppedMaxTiming;
						 	alternador*=-1;
						}
						else{
							movingTiming--;
						}
				}
				else{
					stoppedTiming--;
				}
		}
	}
	void yMov(){
		if(canMovInY){
			if((stoppedTiming + desfaseTimingStopped)==0){
					transform.position = new Vector3(transform.position.x,transform.position.y + sum,transform.position.z);
					sum=sum+trayectoryVelocity*alternador;
						if((movingTiming+desfaseTimingMoving)==0){
							sum=0;
							movingTiming = movingMaxTiminig;
						 	stoppedTiming=stoppedMaxTiming;
						 	alternador*=-1;
						}
						else{
							movingTiming--;
						}
				}
				else{
					stoppedTiming--;
				}	
		}

	}

}
