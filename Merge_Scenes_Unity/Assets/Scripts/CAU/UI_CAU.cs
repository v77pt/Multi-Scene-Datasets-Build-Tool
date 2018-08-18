using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.IO;

public class UI_CAU : MonoBehaviour {

	private GameObject Gym1;
	private GameObject Gym2;
	private GameObject Gym3;
	private GameObject Gym4;
	private GameObject Gym5;
	private GameObject eb1;
	private GameObject eb2;
	private GameObject eb3;

	public Button saveBtn;

	// Use this for initialization
	void Start () {
		saveBtn.onClick.AddListener (SaveCamera);

		StartCoroutine (Init());
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	IEnumerator Init(){
		
		yield return new WaitForEndOfFrame ();

		Gym1 = VCamera ("Gym1");
		Gym2 = VCamera ("Gym2");
		Gym3 = VCamera ("Gym3");
		Gym4 = VCamera ("Gym4");
		Gym5 = VCamera ("Gym5");
		eb1 = VCamera ("eb1");
		eb2 = VCamera ("eb2");
		eb3 = VCamera ("eb3");
	}

	GameObject VCamera(string name){

		GameObject pointCloud;

		if (GameObject.Find (name + "(Clone)") != null) {
			pointCloud = GameObject.Find (name + "(Clone)");
		} else if (GameObject.Find ("/" + name) != null) {
			pointCloud = GameObject.Find ("/" + name);
		} else {
			pointCloud = new GameObject ();
			pointCloud.name = name + "pointCloud";
		}

		SetTansformData (pointCloud, name + "Transform");


		GameObject CamROOT = new GameObject ();
		CamROOT.name = name + "CamROOT";

		CameraPoseVisualization.Instance.SetCamera (name).transform.SetParent (CamROOT.transform);

		SetTansformData (CamROOT, name + "Transform");

		return CamROOT;
	}

	void SaveCamera(){
		CameraPoseVisualization.Instance.SaveCameraData (Gym1.name + "/" + "Gym1");
		CameraPoseVisualization.Instance.SaveCameraData (Gym2.name + "/" + "Gym2");
		CameraPoseVisualization.Instance.SaveCameraData (Gym3.name + "/" + "Gym3");
		CameraPoseVisualization.Instance.SaveCameraData (Gym4.name + "/" + "Gym4");
		CameraPoseVisualization.Instance.SaveCameraData (Gym5.name + "/" + "Gym5");
		CameraPoseVisualization.Instance.SaveCameraData (eb1.name + "/" + "eb1");
		CameraPoseVisualization.Instance.SaveCameraData (eb2.name + "/" + "eb2");
		CameraPoseVisualization.Instance.SaveCameraData (eb3.name + "/" + "eb3");

		Table.CameraTableReader.Instance.SaveTransDataToCSV (Gym1.transform, "Gym1Transform");
		Table.CameraTableReader.Instance.SaveTransDataToCSV (Gym2.transform, "Gym2Transform");
		Table.CameraTableReader.Instance.SaveTransDataToCSV (Gym3.transform, "Gym3Transform");
		Table.CameraTableReader.Instance.SaveTransDataToCSV (Gym4.transform, "Gym4Transform");
		Table.CameraTableReader.Instance.SaveTransDataToCSV (Gym5.transform, "Gym5Transform");
		Table.CameraTableReader.Instance.SaveTransDataToCSV (eb1.transform, "eb1Transform");
		Table.CameraTableReader.Instance.SaveTransDataToCSV (eb2.transform, "eb2Transform");
		Table.CameraTableReader.Instance.SaveTransDataToCSV (eb3.transform, "eb3Transform");

		Debug.Log ("Done");
	}

	void SetTansformData(GameObject gameobj, string PATH){
		if (File.Exists (Application.dataPath + "/Resources/Table/SavedData/" + PATH + ".txt")) {

			Table.CameraTableReader.Instance.ReadFromFile ("SavedData/" + PATH);
			Table.CameraLabel cameraLabel = Table.CameraTableReader.Instance.GetItem (0);

			gameobj.transform.position = new Vector3 (cameraLabel.X, -cameraLabel.Y, cameraLabel.Z);
			gameobj.transform.rotation = new Quaternion (cameraLabel.P, -cameraLabel.Q, cameraLabel.R, cameraLabel.W);
			gameobj.transform.localScale = new Vector3 (cameraLabel.S, cameraLabel.S, cameraLabel.S);
		}
	}
}
