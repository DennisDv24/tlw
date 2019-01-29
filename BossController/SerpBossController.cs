using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Xml.Schema;

public class SerpBossController : MonoBehaviour {

Vector2[] codePoints;
int codePointsCount;
private PolygonCollider2D pc2d;

	void Start () {
		pc2d = GetComponent<PolygonCollider2D>();
		codePointsCount = pc2d.GetTotalPointCount();
		codePoints = new Vector2[codePointsCount];

	}
	int n;
	int x;
	int x2;
	bool initialize = true;

	void Update () {
		pc2d.points = codePoints;
		initializeVars(initialize);
		initialize = false;
			if(x<=(codePoints.Length/2)-1){
				
				codePoints[x] = new Vector2(x, (float)senF(x) * 2*x * 0.1f);
				x++;
			}
			if(x2<codePoints.Length){
				codePoints[x2] = new Vector2(n, ((float)senF(x)-2) *2);
				n--;
				x2++;

			}
		
			
	}
		void initializeVars(bool initialize){
			if(initialize){
					n = (codePoints.Length/2);
					x = 0;
					x2 = (codePoints.Length/2);
				}
		}


	double senF(double angle){
			return (Math.Sin((angle)/2) *3);
	}


		void pointsLine1(){
			for(int i =0; i<8; i++){
				codePoints[i] = new Vector2(i,0);
			}

		}
		void pointsLine2(){
			for(int i =7; i>0; i--){
				codePoints[16-i] = new Vector2(i,1);
			}

		}
	//HAY QUE SEGUIR UNA FORMA HORARIA, PARA QUE QUEDE BIEN UNA VEZ HALLAS UNIDO UNA FILA DE
	//PUNTOS VAS A TENER QUE EMPEZAR LA OTRA DESDE ATRAS.
}
