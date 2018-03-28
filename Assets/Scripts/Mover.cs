using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Mover : MonoBehaviour {

	protected Vector3 position;
	protected Vector3 acceleration;
	protected Vector3 velocity;
	protected Vector3 heading;

	public float mass;
	public float maxSpeed;
	public float maxTurn;
	public float radius;

	// Use this for initialization
	protected virtual void Start () {
		position = new Vector3 (transform.position.x, transform.position.y, transform.position.z);
		acceleration = Vector3.zero;
		velocity = Vector3.zero;
		heading = transform.forward;
	}

	protected abstract void CalcSteeringForces ();

	protected void ApplyForce(Vector3 force) {
		acceleration += force / mass;
	}

	void UpdatePosition() {
		velocity += acceleration;
		if (velocity.sqrMagnitude > maxSpeed * maxSpeed) {
			velocity = velocity.normalized * maxSpeed;
		}

		position += velocity * Time.deltaTime;

		heading = velocity.normalized;

		acceleration = Vector3.zero;
	}

	void SetTransform() {
		float angle = Mathf.Atan2 (heading.y, heading.x) * Mathf.Rad2Deg;
		Quaternion rotation = Quaternion.AngleAxis (angle, Vector3.forward);

		this.transform.SetPositionAndRotation (position, rotation);
	}
	
	// Update is called once per frame
	void Update () {
		CalcSteeringForces ();
		UpdatePosition ();
		SetTransform ();
	}

	public Vector3 Seek(Vector3 target) {
		Vector3 desiredVelocity = target - position;
		desiredVelocity = desiredVelocity.normalized * maxSpeed;

		return desiredVelocity;
	}
}
