using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class kektusController : MonoBehaviour {//NOTA PUEDE QUE HAGA FALTA IMPLEMENTAR UN FIXEDUPDATE PARA LOS MOVIMIENTOS FISICOS

private Rigidbody2D rb2d;
private Animator anim;	
public GameObject plectrum;
public Rigidbody2D player;
public SpriteRenderer bg;

bool introRunning = true; //La intro running tiene que tener implementada la phase changing animation
bool phase1Running;
bool phase2Running;
bool phase3Running;

public const float kektusInitialHealt = 100;
	float battleLim1 = kektusInitialHealt*2/3;
	float battleLim2 = kektusInitialHealt*1/3;
float kektusHealt = kektusInitialHealt;

	public bool grounded;
	public bool air;
	public bool canjump;
	public bool canarea;
	public bool sppining;
	public bool throwingPlectrums;
	public bool phaseIsChanging;
		const int phaseAnimationChangingMaxTimer = 80;
		private int phaseAnimationChangingTimer = phaseAnimationChangingMaxTimer;
	public bool canDeadAnimation;
		
		int introTiming = 120;

		public const float plectrumInstantiateMaxTiming = 40;
		float plectrumInstantiateTiming = plectrumInstantiateMaxTiming;
		Vector3 plectrumPosition;
			int randomAttackSelection;
		public const int randomAttackSelectionMaxTiming = 110;
		int randomAttackSelectionTiming;
			bool canDoAttack1;
			bool canDoAttack2;
			private Quaternion rotationPlectrum = Quaternion.Euler(0, 0, 90);
				
		public const int finalAttackMaxTiming = 60;
		int finalAttackTiming = finalAttackMaxTiming;
			int randomPositionPlectrumsCalculate;
			public bool regenerate;
				int finalAnimationTimer = 200;

		public const int healtedDownMaxTimer = 9;
		private int healtedDownTimer = healtedDownMaxTimer;

	void Start () {
		initialize_GetComponentsAndGravity();
	}
			void initialize_GetComponentsAndGravity(){
				Physics.gravity = new Vector2(0, -1);
				rb2d = GetComponent<Rigidbody2D>();
				anim = GetComponent<Animator>();
				player.GetComponent<Rigidbody2D>();
				bg.GetComponent<SpriteRenderer>();
			}


void Update () {
print(kektusHealt);
	if(introRunning == false){
	}
	initializeEngineVars();	
	bossConditions();
}

	void initializeEngineVars(){
		anim.SetBool("Grounded", grounded);
		anim.SetBool("Air", air);
		anim.SetBool("Spinning", sppining);
		anim.SetBool("ThrowingPlectrum(s)", throwingPlectrums);
		anim.SetBool("PhaseIsChanging", phaseIsChanging);
		anim.SetBool("CanDeadAnimation", canDeadAnimation);
		plectrumPosition = new Vector3(-5.1f,30f,plectrum.transform.position.z);
		if(healtedDownTimer<=0) {
			GetComponent<SpriteRenderer>().color = new Color(255,255,255);
		}
		healtedDownTimer--;
	}	

	void bossConditions(){
		if(phase1Running){
			phase1();
		}
		else if(introRunning){
			intro();
		}
		if(phase2Running){
			phase2();
		}
		if(phase3Running && phaseIsChanging == false){
			phase3();
		}
		if(phaseIsChanging){
			if(phaseAnimationChangingTimer == 0){
				phaseIsChanging = false;
				phaseAnimationChangingTimer = phaseAnimationChangingMaxTimer;
			}
			else{
				phaseAnimationChangingTimer--;
			}
		}
	}
				void intro(){
					if(introTiming != 0){
						introTiming --;
					}
					else{
						introRunning = false;
						phase1Running = true;
						bg.GetComponent<SpriteRenderer>().color = new Color(255,255,255,0);
					}
				}

		void phase1(){
			if(kektusHealt>battleLim1){
				attack1();
			}
			else{
				throwingPlectrums = false;
				plectrum.GetComponent<PlectrumController>().attack1 = false;
				phaseIsChanging =true;
				phase2Running = true;
				phase1Running = false;
				randomAttackSelectionTiming = randomAttackSelectionMaxTiming;
			}
		}
				void attack1(){
					if(plectrumInstantiateTiming<=0){
						throwingPlectrums = true;

						plectrum.GetComponent<PlectrumController>().attack1 = true;
						Instantiate(plectrum, plectrumPosition, Quaternion.identity);
						plectrum.GetComponent<PlectrumController>().yGap = player.position.y;
						plectrumInstantiateTiming = plectrumInstantiateMaxTiming;
					}
					else{
						plectrumInstantiateTiming--;
						throwingPlectrums = false;
					}
				}

		void phase2(){
			if(kektusHealt>battleLim2){
				attack2();
			}
			else{
				throwingPlectrums = false;
				canjump = false;
				canarea = false;
				phaseIsChanging = true;
				phase3Running = true;
				plectrum.GetComponent<PlectrumController>().attack2 = false;
				plectrum.GetComponent<PlectrumController>().attack3 = false;
				phase2Running = false;
			}
		}
				void attack2(){
					throwingPlectrums = false;
					if(randomAttackSelectionTiming == 0){
						randomAttackSelection = (int)Random.Range(0,2);
						randomAttackSelectionTiming = randomAttackSelectionMaxTiming;
						throwingPlectrums = false;
						canDoAttack1 = true;
						canDoAttack2 = true;
					}
					else{
						randomAttackSelectionTiming--;
					}
						if(randomAttackSelection == 0 && canDoAttack1){
							plectrum.GetComponent<PlectrumController>().attack3 = true;
							attack2_1_JumpAttack();
						}
						else if(randomAttackSelection == 1 && canDoAttack2){
							plectrum.GetComponent<PlectrumController>().attack2 = true;
							attack2_2_PlectrumWallAttack();
						}
				}

					void attack2_1_JumpAttack(){
							if(canjump){
								rb2d.AddForce(Vector2.up * 30, ForceMode2D.Impulse);
								canjump = false;
								canarea = true;
							}
							else if(grounded && canarea ==false){
								canjump = true;
							}
								if(grounded && canarea){
									area();
								}
					}
							void area(){
								for(int i = 0; i<16; i++){
									Instantiate(plectrum, new Vector3(-5.1f + i, (-22f) - (i*0.5f), plectrum.transform.position.z), rotationPlectrum); 
								}
									canarea = false;
									canDoAttack1 = false;
									plectrum.GetComponent<PlectrumController>().attack3 = false;
							}

					void attack2_2_PlectrumWallAttack(){
						throwingPlectrums = true;
							for(int i = 0; i<8; i++){
								Instantiate(plectrum, new Vector3(-5.1f,i-4,plectrum.transform.position.z), Quaternion.identity); 
							}
						canDoAttack2 = false;
						plectrum.GetComponent<PlectrumController>().attack2 = false;
					}


		void phase3(){
			if(kektusHealt >0){
				sppining = true;
				attack3();
			}
			else{
				plectrum.GetComponent<PlectrumController>().fAttack = false;
				kill();
			}
		}
					float spin = 0;
				void attack3(){
					if(finalAttackTiming ==0){
						for(int i = 0; i<3;i++){
							plectrum.GetComponent<PlectrumController>().fAttack = true;
							if(plectrum.GetComponent<PlectrumController>().regenerate == true){
								i--;
								plectrum.GetComponent<PlectrumController>().regenerate = false;
							}
								randomPositionPlectrumsCalculate = (int)Random.Range(-4,0);
								Instantiate(plectrum, new Vector3(-5.1f, randomPositionPlectrumsCalculate, plectrum.transform.position.z), Quaternion.identity);
						}
						finalAttackTiming = finalAttackMaxTiming;
					}
					else{
						finalAttackTiming--;
						plectrum.GetComponent<PlectrumController>().fAttack = false;
					}
						if(sppining){
							transform.localRotation = Quaternion.Euler(0,spin,0);
							spin+=40;
						}
	
				}

		void kill(){
			print(finalAnimationTimer);
			sppining = false;
			canDeadAnimation = true;
				if(finalAnimationTimer == 0){
					Destroy(gameObject);
				}
				else{
					finalAnimationTimer--;
				}
			
		}

		void OnTriggerEnter2D(Collider2D col){
			if (col.gameObject.tag == "LethalToEnemy" ) {
				kektusHealt-=5;
				GetComponent<SpriteRenderer>().color = new Color(0,0,0);
				healtedDownTimer = healtedDownMaxTimer;
			}
		}
}
