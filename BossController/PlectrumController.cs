using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlectrumController : MonoBehaviour {

private Rigidbody2D rb2d;
public float plectrumXsum = 0.25f;
	
	float x = -5.1f;
	public float yGap;
	float plectrumYsum = 0.3f;
	public bool attack1;
	public bool attack2;
	public bool attack3;
	public bool fAttack;
	public bool fAttackfix;
	public bool regenerate;

	void Start(){	
		rb2d = GetComponent<Rigidbody2D>();
	}

	void Update(){
		if(attack1){
			rb2d.position = new Vector2(x, (float)senF(x/2)/2);
			x += plectrumXsum / 2.5f;
		}
		if (attack2){
			rb2d.position = new Vector2(x, rb2d.position.y);
			x += plectrumXsum/2;
		}
		if(attack3){
			rb2d.position = new Vector2(rb2d.position.x, rb2d.position.y + plectrumXsum);
			GetComponent<Transform>().localScale = new Vector3(2.5f,1f,1f);
				if(rb2d.position.y >-3.3f){
					Destroy(gameObject);
				}
		}
		if(fAttack){
			rb2d.position = new Vector2(x , rb2d.position.y);
			x += plectrumXsum / 3;
		}
	}

			double senF(double angle){
				return(Math.Sin(angle)+yGap*2);
			}

		void OnTriggerEnter2D(){
			Destroy(gameObject);
		}
		void OnCollisionStay2D(Collision2D col){
			if (col.gameObject.tag == "Lethal" ) {
				Destroy(gameObject);
				regenerate = true;
			}
		}
}
