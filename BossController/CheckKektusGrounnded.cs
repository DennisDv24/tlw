using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckKektusGrounnded : MonoBehaviour {

	private kektusController kektus;

	void Start () {
		kektus = GetComponentInParent<kektusController>();
	}

	void OnCollisionStay2D(Collision2D col){
		if(col.gameObject.tag == "Ground"){
			kektus.grounded = true;
			kektus.air = false;
		}
	}

	void OnCollisionExit2D(Collision2D col){
		if (col.gameObject.tag == "Ground") {
			kektus.grounded = false;
			kektus.air = true;
		}
	}
}
