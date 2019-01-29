using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Security;
using UnityEngine.SceneManagement;

public class PlayerController2 : MonoBehaviour {

private Rigidbody2D rb2d;
private Animator anim;
public GameObject cc2d;

public const float gravityVar = -1f;
	public const float player_max_velocity = 6f;
	public const float jump_max_velocity = 19f;
	public const float dash_max_power = 19f;
	public const int dash_max_timing = 9;
	public const int dash_max_cooldown = 10;
	public const float wallGrabing_max_ratio = 3f;
	public const float wallJumping_max_power = 14f;
	public const int wallJumping_max_timing = 3;				
	public const int wallJumpingExit_max_timing = 1;
	public const float attackingMaxCooldown = 20;
	public const int attackingMaxTimer = 2;
		public float player_velocity = player_max_velocity;
		private int dash_cooldown = dash_max_cooldown;
		private int dash_timing = dash_max_timing;
		private float dash_power = dash_max_power;
		private float wallGrabing_ratio = wallGrabing_max_ratio;
		private float wallJumping_power = wallJumping_max_power;
		private float wallJumping_timing = wallJumping_max_timing;
			private const int extraWallJumpingMaxTimer = 10;
			private int extraWallJumpingTimer = extraWallJumpingMaxTimer;
		private float attackingCooldown = attackingMaxCooldown;
		private float attackingTimer = attackingMaxTimer;

	public bool moving;
	public bool grounded;
	public bool air;
	public bool jumping; 
	public bool dashing;
	public bool atacking;
	public bool wall;
	public bool wallGrabing;
	public bool wallJumping;

	public float player_last_direction;

void Start () {
	initialize_GetComponentsAndGravity();
}

		void initialize_GetComponentsAndGravity(){
			Physics.gravity = new Vector2(0, gravityVar);
				rb2d = GetComponent<Rigidbody2D>();
				anim = GetComponent<Animator>();
		}
	
	void Update () {
		initializeEngineVars();	
		conditions();

	}
	void conditions() {
		if(Input.GetKeyDown (KeyCode.Z)  && grounded){
			jump();
			grounded = false;
			jumping = true;
		}
		if(air == true && wall==true){
			wallGrabing = true;
			extraWallJumpingTimer = extraWallJumpingMaxTimer;
		}
		else{
			wallGrabing = false;
			extraWallJumpingTimer--;
		}
		if(extraWallJumpingTimer>=0){
			if(Input.GetKeyDown (KeyCode.Z)) {
				wallJumping = true;
			}
			else if(wallJumping_timing <=0){
				wallJumping_timing = wallJumping_max_timing;
				wallJumping = false;
			}
		}
		else if(wallJumping_timing <=0){
				wallJumping_timing = wallJumping_max_timing;
				wallJumping = false;
		}
		if(grounded){
			jumping=false;
			air = false;
		}
		else{
			air=true;
		}
		if (Input.GetKeyDown(KeyCode.C) && dash_cooldown <= 0) {
			dashing = true;
		}
			if(Input.GetKeyDown(KeyCode.X) && attackingCooldown<=0){
				atacking = true;
			}
			if(atacking){
				cc2d.GetComponent<CircleCollider2D>().enabled = true;
					if(attackingTimer<=0){
						atacking = false;
						attackingCooldown = attackingMaxCooldown;
						attackingTimer = attackingMaxTimer;
					}
					else{
						attackingTimer--;
					}
			}
			else{
				attackingCooldown--;
				cc2d.GetComponent<CircleCollider2D>().enabled = false;
			}


	}
			void initializeEngineVars(){
				anim.SetFloat ("Speed", Mathf.Abs(rb2d.velocity.x)); 
				anim.SetFloat ("SpeedDir", player_velocity);
				anim.SetBool("Grounded", grounded); 
				anim.SetBool("Atacking", atacking);
				anim.SetBool("Dashing",dashing);
				anim.SetBool("Wall", wall);
				anim.SetBool("WallGrabing", wallGrabing);
			}


	void FixedUpdate(){
		player_velocity = Input.GetAxis ("Horizontal");
		movements();
	}
		void movements(){
			walk(walkDirection() * player_max_velocity);
			dash();
			wallGrabingFunction();
			wallJumpingFunction();
			animDirection();


		}

			private void walk(float dir){
				rb2d.velocity = new Vector2(dir, rb2d.velocity.y);  		
					if(dir != 0){
						moving=true;
					}
					else{
						moving=false;
					}
			}

				private float walkDirection(){
					if (Input.GetKey (KeyCode.RightArrow) && dashing == false) {
						return 1;
					} 
					else if (Input.GetKey (KeyCode.LeftArrow) && dashing == false) {
						return -1;
					} 
					else{
						return 0;
					}
				}

		void jump() {
			rb2d.AddForce (Vector2.up * jump_max_velocity, ForceMode2D.Impulse);
		}

		void dash(){
			if(dashing){
				dash_cooldown = dash_max_cooldown;
				rb2d.velocity = new Vector2(dash_power * player_last_direction, 0);
				dash_timing--;
					if(dash_timing<=0){
						finishDash();
					}
			}
			else{
				dash_cooldown--;
			}
		}
				
				void finishDash(){
					dashing = false;
					dash_timing = dash_max_timing;
					dash_cooldown = dash_max_cooldown;

				}

		void wallGrabingFunction(){
			if(wallGrabing){
				if((player_last_direction == 1 && Input.GetKey (KeyCode.RightArrow)) || (player_last_direction == -1 && Input.GetKey (KeyCode.LeftArrow))){
						rb2d.velocity = new Vector2(rb2d.velocity.x, -wallGrabing_ratio);
				}
			}
		}
		void wallJumpingFunction(){
			if(wallJumping){
				rb2d.velocity = new Vector2(-wallJumping_power * player_last_direction, wallJumping_power);
				wallJumping_timing--;
			}
		}

			void animDirection(){
				if (player_velocity > 0.1f && dashing == false) {
					transform.localScale = new Vector3 (1f,1f,1f);
						if(dashing == false){
							player_last_direction = 1;
						}
				}
				if (player_velocity < -0.1f && dashing == false) {
					transform.localScale = new Vector3 (-1f,1f,1f);
						if(dashing == false){
							player_last_direction = -1;
						}
				}
			}



		public void kill(){
			//ANIMACION DE MUERTE AQUI
			respawn();
		}
	public string sceneName;
		void respawn(){
			//Aparece en el ultimo checkpoint
			//Solucion provisional:
			SceneManager.LoadScene(sceneName);

		}

			

}
