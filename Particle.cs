using System.Collections;
using UnityEngine;

//This class is the fundamental data structure for physical engine

public class Particle{
	
	public float gravity;
	public float mass;
	float damping;
	public Vector3 position;
	public Vector3 velocity;
	public Vector3 acceleration;


	float inverseMass;
	public Vector3 gravityForce;
	public Vector3 forceAccum;
	//constructor without force input
	public Particle(float mass, float gravity, Vector3 velocity, Vector3 position){
		this.position = position;
		this.damping = 0.995f;
		this.mass = mass;
		this.inverseMass = 1 / mass;
			this.gravityForce = new Vector3 (0, -gravity * mass, 0);
		this.forceAccum = Vector3.zero;
		forceAccum += gravityForce;

		this.velocity = velocity;



	}

	//physics integration, duration is time to update one frame
	public void Integration(float duration){
		
		GenerateGravity ();


		//get acceleration from force by Newton second law , f = m*a so a = f*1/m
		Vector3 resultAcc = Vector3.zero;

		resultAcc = resultAcc + forceAccum * inverseMass;

		acceleration = resultAcc;
		//update velocity from acceleration , v = v + a * time

		velocity += resultAcc * duration;

		// Impose drag force for newton first law
		velocity = velocity * Mathf.Pow(damping, duration);

		//update position , p = p + v * time
		position += velocity * duration;
		ClearForce ();



	}

	public void ClearForce(){
		forceAccum = Vector3.zero;
	}

	void GenerateGravity(){
		forceAccum += gravityForce;
	}

	public void AddForce(Vector3 force){
		forceAccum += force;
	}
		

}
