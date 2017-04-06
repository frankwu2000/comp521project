using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ballistic : MonoBehaviour {
	public float mass; 
	public float gravity;
	public int ShootingMode; // ShootingMode: 0 - none, 1 - cannon, 2 - bullet
	Particle particle ;

	void Start () {
//		particle = new Particle (mass, gravity, velocity, gameObject.transform.position);
		if (ShootingMode == 0) {
			particle = new Particle (mass, gravity, Vector3.zero, gameObject.transform.position);
		} else if (ShootingMode == 1) {
			particle = new Particle (200, gravity, new Vector3 (20, 15, 0), gameObject.transform.position);
			particle.acceleration = new Vector3 (0, -20, 0);
		} else if (ShootingMode == 2) {
			particle = new Particle (2, gravity, new Vector3 (50, 0, 0), gameObject.transform.position);
			particle.acceleration = new Vector3 (0, -1, 0);

		}
	}

	void Update () {
		UpdateParticle ();
		if (Input.GetKey(KeyCode.Space)) {
			particle.Integration (Time.deltaTime);
		}
		if (Input.GetKey(KeyCode.A)) {
			particle.AddForce (new Vector3(0,0,300));
			particle.Integration (Time.deltaTime);
		}
//		if (Input.GetKey(KeyCode.D)) {
//			particle.AddForce (new Vector3(0,0,-300));
//			//particle.Integration (Time.deltaTime);
//		}
//		particle.Integration (Time.deltaTime);

	}

	void UpdateParticle(){
		gameObject.transform.position = particle.position;
	}
}
