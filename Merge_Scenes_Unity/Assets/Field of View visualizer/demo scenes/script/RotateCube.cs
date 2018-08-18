using UnityEngine;
using System.Collections;

public class RotateCube : MonoBehaviour {

	// Update is called once per frame
	void Update () {
		this.transform.RotateAround(this.transform.position, Vector3.up, Time.deltaTime*20);
	}
}
