using UnityEngine;
using System.Collections;

/**
 * CameraFacingBilboard
 *  credit : Neil Carter
 */

[ExecuteInEditMode()]
public class Bilboard : MonoBehaviour {
	public Camera m_Camera;
	
	void Update() {
		if (m_Camera)
			transform.LookAt(transform.position + m_Camera.transform.rotation * Vector3.forward,
		                 	m_Camera.transform.rotation * Vector3.up);
	}
}