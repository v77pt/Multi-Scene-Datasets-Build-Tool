using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;

public class CAU_TEP : MonoBehaviour {



	// Use this for initialization
	void Start () {
		VCamera ("EngineeringBuilding_csv_dataset_test");
		VCamera ("EngineeringBuilding_csv_dataset_train");
		VCamera ("OlympicGym_csv_dataset_test");
		VCamera ("OlympicGym_csv_dataset_train");

		//Init();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void VCamera(string name){

		GameObject CamROOT = new GameObject ();
		CamROOT.name = name + "CamROOT";

		CameraPoseVisualization.Instance.SetCamera (name).transform.SetParent (CamROOT.transform);

	}

	void Init(){
		StartCoroutine (loadOFF("/Datasets/eb1", "eb1Transform"));
		StartCoroutine (loadOFF("/Datasets/eb2", "eb2Transform"));
		StartCoroutine (loadOFF("/Datasets/eb3", "eb3Transform"));
		StartCoroutine (loadOFF("/Datasets/Gym1", "Gym1Transform"));
		StartCoroutine (loadOFF("/Datasets/Gym2", "Gym2Transform"));
		StartCoroutine (loadOFF("/Datasets/Gym3", "Gym3Transform"));
		StartCoroutine (loadOFF("/Datasets/Gym4", "Gym4Transform"));
		StartCoroutine (loadOFF("/Datasets/Gym5", "Gym5Transform"));
	}

	// Start Coroutine of reading the points from the OFF file and creating the meshes
	IEnumerator loadOFF(string dPath, string transPath){

		// Read file
		StreamReader sr = new StreamReader (Application.dataPath + dPath  + ".off");
		sr.ReadLine (); // OFF
		string[] buffer = sr.ReadLine ().Split(); // nPoints, nFaces

		int numPoints = int.Parse (buffer[0]);
		Vector3[] points = new Vector3[numPoints];
		int[,] colors = new int[numPoints, 3];
		Vector3 minValue = new Vector3();

		for (int i = 0; i< numPoints; i++){
			buffer = sr.ReadLine ().Split ();

			points[i] = new Vector3 (float.Parse (buffer[0]), float.Parse (buffer[1]) * -1f,float.Parse (buffer[2]));

			colors [i, 0] = int.Parse (buffer [3]);
			colors [i, 1] = int.Parse (buffer [4]);
			colors [i, 2] = int.Parse (buffer [5]);
		}

		string fullPath = "D:/UnityWorkSpace/PointCloudLibrary/Assets/Resources/Table/SavedData/" + dPath + ".off";

		System.IO.FileInfo fi = new System.IO.FileInfo(fullPath);  
		if (!fi.Directory.Exists)  
		{  
			fi.Directory.Create();  
		}  
		System.IO.FileStream fs = new System.IO.FileStream(fullPath, System.IO.FileMode.Create,   
			System.IO.FileAccess.Write);  
		System.IO.StreamWriter sw = new System.IO.StreamWriter(fs, System.Text.Encoding.UTF8);  

		string data = "COFF";
		sw.WriteLine(data);  

		data = String.Format ("{0} 0 0", numPoints);
		sw.WriteLine(data);  

		GameObject ROOT = new GameObject ();

		if (File.Exists (Application.dataPath + "/Resources/Table/SavedData/" + transPath + ".txt")) {

			Table.CameraTableReader.Instance.ReadFromFile ("SavedData/" + transPath);
			Table.CameraLabel cameraLabel = Table.CameraTableReader.Instance.GetItem (0);

			ROOT.transform.position = new Vector3 (cameraLabel.X, -cameraLabel.Y, cameraLabel.Z);
			ROOT.transform.rotation = new Quaternion (cameraLabel.P, -cameraLabel.Q, cameraLabel.R, cameraLabel.W);
			ROOT.transform.localScale = new Vector3 (cameraLabel.S, cameraLabel.S, cameraLabel.S);
		}

		for (int i = 0; i < numPoints; i++) {
			points [i] = ROOT.transform.localToWorldMatrix.MultiplyPoint (points [i]);

			data = String.Format ("{0:F6} {1:F6} {2:F6} {3} {4} {5} 255", points[i].x, points[i].y  * -1f, points[i].z, colors[i,0], colors[i,1], colors[i,2]);
			sw.WriteLine(data);  
		}

		sw.Close();  
		fs.Close();  
		Debug.Log ("Done.");

		yield return new WaitForEndOfFrame ();

	}

}
