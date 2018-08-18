using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class LoadCameraData_Origin : MonoBehaviour {

	public int index;
	Table.CameraLabel[] cameraData;

	GameObject CameraRoot;
	GameObject[] CameraArrows;

	public Image RenderPlane;

	// Use this for initialization
	void Start () {

		//index = -1;

		StartCoroutine(ChangePose());
//		StartCoroutine(SetCamera());
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetButtonDown("Fire1")) 
		{
			index = (index + 1) % cameraData.Length;

			transform.position = GetCameraPos ();
			transform.rotation = GetCameraOrientation ();
		}
	}

	void LoadCameraInfo(string PATH){
		Table.CameraTableReader.Instance.ReadFromFile (PATH);
		cameraData = Table.CameraTableReader.Instance.GetAllItem ();
	}

	IEnumerator ChangePose(){

		yield return new WaitForSeconds (0.5f);

		LoadCameraInfo ("dataset_predict_csv");

		yield return new WaitForSeconds (0.5f);

		while (true) {
			index = (index + 1) % cameraData.Length;

			transform.position = GetCameraPos ();
			transform.rotation = GetCameraOrientation ();

			StartCoroutine(SetSysSprite ());
//			GetSprite ();
			Debug.Log("("+transform.position.x + ", "+transform.position.y + ", "+transform.position.z+" )");

			yield return new WaitForSeconds (2f);
		}
	}

	IEnumerator SetCamera(){

		CameraRoot = new GameObject();
		CameraRoot.name = "CameraRoot";
		CameraRoot.transform.position = Vector3.zero;

		CameraArrows = new GameObject[cameraData.Length];

		for(int i = 0; i < cameraData.Length; i++){
			CameraArrows[i] = GameObject.Instantiate (Resources.Load("Prefabs/model")) as GameObject;
			CameraArrows [i].transform.SetParent (CameraRoot.transform, false);
			CameraArrows [i].transform.position = GetCameraPosByID (i);
			CameraArrows [i].transform.rotation = GetCameraOrientationByID (i);
		}
		yield return new WaitForEndOfFrame ();
	}

	Vector3 GetCameraPosByID(int i){
		Vector3 result = new Vector3 (cameraData [i].X, -cameraData [i].Y, cameraData [i].Z);
		return result;
	}

	Quaternion GetCameraOrientationByID(int i){
		Quaternion result = new Quaternion (cameraData [i].P, -cameraData [i].Q, cameraData [i].R, cameraData [i].W);
		return result;
	}

	Vector3 GetCameraPos(){
		return GetCameraPosByID (index);
	}

	Quaternion GetCameraOrientation(){
		return GetCameraOrientationByID (index);
	}

	Sprite GetSprite(){
//		Debug.Log ("Image/" + cameraData [index].FileName);

		string Path = "Image/" + cameraData [index].FileName;
		Path = Path.Replace (".JPG", "");
		Debug.Log (Path);
		Texture2D img = Resources.Load(Path) as Texture2D;
		Debug.Log (img.name);
		Sprite result = Sprite.Create (img, new Rect (0f, 0f, img.width, img.height), new Vector2 (0.5f, 0.5f));
		return result;
	}

	IEnumerator SetSysSprite(){
		//		Debug.Log ("Image/" + cameraData [index].FileName);

		string Path = cameraData [index].FileName;

		//
		//Path = "D:/_git/posenet/PoseNet_Dataset/EngineeringBuilding/" + Path;
		Path = Path.Insert (32, "resize/");

//		Debug.Log (Path);
		Texture2D img = null;

		using(WWW www = new WWW("file://" + Path))//Application.streamingAssetsPath+"fileName")) 
		{ 
			yield return www; 
			img = www.texture; 
		} 
		Debug.Log ("?:" + img == null);
//		Texture2D img = WWW
		Debug.Log (img.name);
		Sprite result = Sprite.Create (img, new Rect (0f, 0f, img.width, img.height), new Vector2 (0.5f, 0.5f));

		RenderPlane.sprite = result;
//		return result;
	}


}

