using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LethalController : MonoBehaviour {

private PlayerController2 player;

	void Start () {
		player = GetComponentInParent<PlayerController2>();
	}

	void OnCollisionStay2D(Collision2D col){
		if(col.gameObject.tag == "Lethal"){
			player.kill();
		}
	}

}
