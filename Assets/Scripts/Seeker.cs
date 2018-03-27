using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Seeker : Mover {

	public GameObject target;

	// Use this for initialization
	protected override void Start () {
		base.Start ();
		mass = 3f;
		maxSpeed = 1f;
		maxTurn = 0.25f;
		radius = 0.5f;
	}

	protected override void CalcSteeringForces ()
	{
		Vector3 steerForce = Vector3.zero;

		Vector3 seekForce = Seek (target.transform.position);

		steerForce += seekForce;

		if (steerForce.sqrMagnitude > maxTurn * maxTurn) {
			steerForce = steerForce.normalized * maxTurn;
		}

		ApplyForce (seekForce);
	}
}
