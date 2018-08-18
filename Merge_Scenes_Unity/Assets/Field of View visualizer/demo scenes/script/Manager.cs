using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Manager : MonoBehaviour {

	public Camera cam;
	public Slider slider1;
	public Slider slider2;
	public Slider slider3;


	void Start() {

	}

	public void ChangeNearClip() {
		cam.nearClipPlane = slider1.value;
	}

	public void ChangeFarClip() {
		cam.farClipPlane = slider2.value;
	}

	public void ChangeFOV() {
		cam.fieldOfView = slider3.value;
	}
}
