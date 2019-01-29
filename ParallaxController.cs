using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxController : MonoBehaviour {

	float parallax_velocity = 0.0001f;

	void Update () {
		parallax_velocity-=0.00005f;
		GetComponent<MeshRenderer> ().material.mainTextureOffset = new Vector2 (parallax_velocity, 0);
	}
}
