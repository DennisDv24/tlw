using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class WarpController : MonoBehaviour {

		public GameObject target;
		public GameObject targetMap;

	void Awake(){

		Assert.IsNotNull (target);
		
		transform.GetChild (0).GetComponent<SpriteRenderer> ().enabled = false;
		transform.GetChild (1).GetComponent<SpriteRenderer> ().enabled = false;

		Assert.IsNotNull (targetMap);

	}

	void OnTriggerEnter2D(Collider2D other){
			
		if (other.tag == "Player") {
			
			other.transform.position = target.transform.GetChild (1).transform.position;

		}

	}


}
