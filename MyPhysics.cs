using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// this class is the script to do calculation for physical simulation 
//and also for object to enter desired physical attribute 

public class MyPhysics : MonoBehaviour {
//	public float fritionFactor;
	public float restitution = 0.6f;
	public float mass; 
	public float gravity;
	public int ShootingMode; // ShootingMode: 0 - none, 1 - cannon, 2 - bullet

	public Particle particle ;

	void Start () {
//		particle = new Particle (mass, gravity, velocity, gameObject.transform.position);
		particle = new Particle (mass, gravity, Vector3.zero, gameObject.transform.position);
		if (ShootingMode == 1) {
			particle = new Particle (200, gravity, new Vector3 (40, 25, 0), gameObject.transform.position);
			particle.acceleration = new Vector3 (0, -20, 0);
		} else if (ShootingMode == 2) {
			particle = new Particle (2, gravity, new Vector3 (50, 10, 0), gameObject.transform.position);
			particle.acceleration = new Vector3 (0, -1, 0);
		}

	}

	//Resolve the resting collision
	void FixedUpdate(){
		//if ball is sinking to ground, fix by add the difference
		if (particle.position.y< 0.49f && particle.forceAccum.y < 0.1f && particle.forceAccum.y > -0.1f ) {
			float support = 0.49f - particle.position.y;
			particle.position.y += support;
		}
		UpdateParticle ();
		particle.Integration (Time.fixedDeltaTime);
	}

	//Pass gameobject position to particle
	void UpdateParticle(){
		gameObject.transform.position = particle.position;
	}

	void OnCollisionEnter(Collision collision)
	{
		if (collision.collider.CompareTag ("Ground")) {
			ParticleContact pC = new ParticleContact (particle, null, restitution, collision.contacts [0].normal, 0);
			pC.Resolve (Time.fixedDeltaTime);
		} else {
			Particle other = collision.collider.GetComponent<MyPhysics> ().particle;
			ParticleContact pC = new ParticleContact (particle, other, restitution, collision.contacts [0].normal, 0);
			pC.Resolve (Time.fixedDeltaTime);
		}

	}


	void OnCollisionStay(Collision collision)
	{
		float penetrationDepth = 0;
		if (collision.collider.CompareTag ("Ground")) {
			ParticleContact pC = new ParticleContact (particle, null, restitution, collision.contacts [0].normal, penetrationDepth);
			pC.Resolve (Time.fixedDeltaTime);
//			Vector3 FrictionForce = new Vector3(particle.gravityForce.y * fritionFactor,0,0);
//			particle.AddForce (FrictionForce);
		}

	}

}
