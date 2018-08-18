using UnityEngine;
using System.Collections;

public class CameraPoseVisualization : MonoBehaviour {

	public static CameraPoseVisualization Instance;

	Table.CameraLabel[] cameraLabel;

	void Awake(){
		Instance = this;
	}

	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {
	
	}

	public GameObject SetCamera(string PATH){

		Table.CameraTableReader.Instance.ReadFromFile (PATH);
		cameraLabel = Table.CameraTableReader.Instance.GetAllItem ();

		GameObject CameraRoot = new GameObject();
		CameraRoot.name = PATH;
		CameraRoot.transform.position = Vector3.zero;

		GameObject[] CameraArrows = new GameObject[cameraLabel.Length];

		for(int i = 0; i < cameraLabel.Length; i++){
			CameraArrows [i] = GameObject.Instantiate (Resources.Load("Prefabs/model")) as GameObject;
			CameraArrows [i].name = cameraLabel [i].FileName;
			CameraArrows [i].transform.SetParent (CameraRoot.transform, false);
			CameraArrows [i].transform.position = GetCameraPosByID (i);
			CameraArrows [i].transform.rotation = GetCameraOrientationByID (i);
		}

		return CameraRoot;
	}

	Vector3 GetCameraPosByID(int i){
		Vector3 result = new Vector3 (cameraLabel [i].X, -cameraLabel [i].Y, cameraLabel [i].Z);
		return result;
	}

	Quaternion GetCameraOrientationByID(int i){
		Quaternion result = new Quaternion (cameraLabel [i].P, -cameraLabel [i].Q, cameraLabel [i].R, cameraLabel [i].W);
		return result;
	}

	public void SaveCameraData(string name){
		
		GameObject cameraRoot = GameObject.Find (name);

		Table.CameraLabel[] newLabel = new Table.CameraLabel[cameraRoot.transform.childCount];

		Debug.Log (cameraRoot.transform.childCount);

		for (int i = 0; i < cameraRoot.transform.childCount; i++) {
			
			newLabel [i] = new Table.CameraLabel ();

			Transform temp = cameraRoot.transform.GetChild (i);

			newLabel [i].CameraID = i;
			newLabel [i].FileName = temp.gameObject.name;
			newLabel [i].X = temp.position.x;
			newLabel [i].Y = -temp.position.y;
			newLabel [i].Z = temp.position.z;
			newLabel [i].W = temp.rotation.w;
			newLabel [i].P = temp.rotation.x;
			newLabel [i].Q = -temp.rotation.y;
			newLabel [i].R = temp.rotation.z;
		}

		Table.CameraTableReader.Instance.SaveCameraDataToCSV (newLabel, name);
	}


	public void SaveTransformData(){
		
	}
}
