using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlataformController : MonoBehaviour {

private Rigidbody2D platrb2d;
public int timerMax = 20;
int timer;
bool falling;

	void Start () {
		platrb2d = GetComponentInParent<Rigidbody2D>();
		timer = timerMax;
	}

	void Update(){
		if(falling){
			if(timer == 0){
				platrb2d.isKinematic = false;
				GetComponent<BoxCollider2D>().isTrigger = true;
				timer = timerMax;
			}
			else{
				timer--;
			}
		}
	}

	void OnCollisionEnter2D(Collision2D col){
		if(col.gameObject.CompareTag("Player")){
			falling = true;

		}
	}

}
