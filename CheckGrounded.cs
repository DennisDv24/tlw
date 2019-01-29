using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckGrounded : MonoBehaviour {

	private PlayerController2 player;

	void Start () {
		player = GetComponentInParent<PlayerController2>();//Pillamos los componentes que tiene playercontroller
	}													  //Y los almacenamos en un objeto player

	void OnCollisionStay2D(Collision2D col){
		if (col.gameObject.tag == "Ground") {//Esta condicion mira si contra lo que esta tocando tiene el tag Ground
			player.grounded = true;			 //Tag que uso para determinar que es un suelo y que componentes se comportan
		}									 //como tal. Bajo player.grounded accedemos a la boolean var de PlayerController											
	}										 //"grounded"

	void OnCollisionExit2D(Collision2D col){
		if (col.gameObject.tag == "Ground") {
			player.grounded = false;
		}
	}

}
