using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class flemencoAnim : MonoBehaviour {
public float initialMaxPosition;
float initialPosition;
public float yPos;
	void Start () {
		initialPosition = initialMaxPosition;
	}

	void Update () {
		GetComponent<Transform>().position = new Vector3(initialPosition,yPos,0);
		initialPosition-=0.005f;
			if(initialPosition<-9){
				initialPosition = initialMaxPosition;
			}
				
	}
}
