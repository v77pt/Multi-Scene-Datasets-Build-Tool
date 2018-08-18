using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.IO;

public class UI_Buttons : MonoBehaviour {

	/*
	GreatCourt
	KingsCollege
	OldHospital
	ShopFacade
	StMarysChurch
	Street
	*/


	Button GreatCourtBtn;
	Button KingsCollegeBtn;
	Button OldHospitalBtn;
	Button ShopFacadeBtn;
	Button StMarysChurchBtn;
	Button StreetBtn;
	Button OlympicGymBtn;
	Button EngineeringBuildingBtn;

	Button GreatCourtSaveBtn;
	Button KingsCollegeSaveBtn;
	Button OldHospitalSaveBtn;
	Button ShopFacadeSaveBtn;
	Button StMarysChurchSaveBtn;
	Button StreetSaveBtn;
	Button OlympicGymSaveBtn;
	Button EngineeringBuildingSaveBtn;

	Button GreatCourtCamBtn;
	Button KingsCollegeCamBtn;
	Button OldHospitalCamBtn;
	Button ShopFacadeCamBtn;
	Button StMarysChurchCamBtn;
	Button StreetCamBtn;
	Button OlympicGymCamBtn;
	Button EngineeringBuildingCamBtn;

	Button GreatCourtCamSaveBtn;
	Button KingsCollegeCamSaveBtn;
	Button OldHospitalCamSaveBtn;
	Button ShopFacadeCamSaveBtn;
	Button StMarysChurchCamSaveBtn;
	Button StreetCamSaveBtn;
	Button OlympicGymCamSaveBtn;
	Button EngineeringBuildingCamSaveBtn;

	GameObject GreatCourtCamROOT;
	GameObject KingsCollegeCamROOT;
	GameObject OldHospitalCamROOT;
	GameObject ShopFacadeCamROOT;
	GameObject StMarysChurchCamROOT;
	GameObject StreetCamROOT;
	GameObject OlympicGymCamROOT;
	GameObject EngineeringBuildingCamROOT;

	public PointCloudManager GreatCourtOFF;
	public PointCloudManager KingsCollegeOFF;
	public PointCloudManager OldHospitalOFF;
	public PointCloudManager ShopFacadeOFF;
	public PointCloudManager StMarysChurchOFF;
	public PointCloudManager StreetOFF;
	public PointCloudManager OlympicGymOFF;
	public PointCloudManager EngineeringBuildingOFF;

	public bool CloseOnStart;

	GameObject GreatCourtPointCloud;
	GameObject KingsCollegePointCloud;
	GameObject OldHospitalPointCloud;
	GameObject ShopFacadePointCloud;
	GameObject StMarysChurchPointCloud;
	GameObject StreetPointCloud;
	GameObject OlympicGymPointCloud;
	GameObject EngineeringBuildingPointCloud;

	// Use this for initialization
	void Start () {
		//OlympicGym
		//EngineeringBuilding

		GreatCourtBtn = transform.FindChild ("OFFButtonROOT/GreatCourt").GetComponent<Button> ();
		GreatCourtBtn.onClick.AddListener (OnGreatCourtBtnClk);

		KingsCollegeBtn = transform.FindChild ("OFFButtonROOT/KingsCollege").GetComponent<Button> ();
		KingsCollegeBtn.onClick.AddListener (OnKingsCollegeBtnClk);

		OldHospitalBtn = transform.FindChild ("OFFButtonROOT/OldHospital").GetComponent<Button> ();
		OldHospitalBtn.onClick.AddListener (OnOldHospitalBtnClk);

		ShopFacadeBtn = transform.FindChild ("OFFButtonROOT/ShopFacade").GetComponent<Button> ();
		ShopFacadeBtn.onClick.AddListener (OnShopFacadeBtnClk);

		StMarysChurchBtn = transform.FindChild ("OFFButtonROOT/StMarysChurch").GetComponent<Button> ();
		StMarysChurchBtn.onClick.AddListener (OnStMarysChurchBtnClk);

		StreetBtn = transform.FindChild ("OFFButtonROOT/Street").GetComponent<Button> ();
		StreetBtn.onClick.AddListener (OnStreetBtnClk);

		OlympicGymBtn = transform.FindChild ("OFFButtonROOT/OlympicGym").GetComponent<Button> ();
		OlympicGymBtn.onClick.AddListener (OnOlympicGymBtnClk);

		EngineeringBuildingBtn = transform.FindChild ("OFFButtonROOT/EngineeringBuilding").GetComponent<Button> ();
		EngineeringBuildingBtn.onClick.AddListener (OnEngineeringBuildingBtnClk);




		GreatCourtSaveBtn = transform.FindChild ("OFFButtonROOT/GreatCourt_save").GetComponent<Button> ();
		GreatCourtSaveBtn.onClick.AddListener (OnGreatCourtSaveBtnClk);
		GreatCourtSaveBtn.interactable = false;

		KingsCollegeSaveBtn = transform.FindChild ("OFFButtonROOT/KingsCollege_save").GetComponent<Button> ();
		KingsCollegeSaveBtn.onClick.AddListener (OnKingsCollegeSaveBtnClk);
		KingsCollegeSaveBtn.interactable = false;

		OldHospitalSaveBtn = transform.FindChild ("OFFButtonROOT/OldHospital_save").GetComponent<Button> ();
		OldHospitalSaveBtn.onClick.AddListener (OnOldHospitalSaveBtnClk);
		OldHospitalSaveBtn.interactable = false;

		ShopFacadeSaveBtn = transform.FindChild ("OFFButtonROOT/ShopFacade_save").GetComponent<Button> ();
		ShopFacadeSaveBtn.onClick.AddListener (OnShopFacadeSaveBtnClk);
		ShopFacadeSaveBtn.interactable = false;

		StMarysChurchSaveBtn = transform.FindChild ("OFFButtonROOT/StMarysChurch_save").GetComponent<Button> ();
		StMarysChurchSaveBtn.onClick.AddListener (OnStMarysChurchSaveBtnClk);
		StMarysChurchSaveBtn.interactable = false;

		StreetSaveBtn = transform.FindChild ("OFFButtonROOT/Street_save").GetComponent<Button> ();
		StreetSaveBtn.onClick.AddListener (OnStreetSaveBtnClk);
		StreetSaveBtn.interactable = false;

		OlympicGymSaveBtn = transform.FindChild ("OFFButtonROOT/OlympicGym_save").GetComponent<Button> ();
		OlympicGymSaveBtn.onClick.AddListener (OnOlympicGymSaveBtnClk);
		OlympicGymSaveBtn.interactable = false;

		EngineeringBuildingSaveBtn = transform.FindChild ("OFFButtonROOT/EngineeringBuilding_save").GetComponent<Button> ();
		EngineeringBuildingSaveBtn.onClick.AddListener (OnEngineeringBuildingSaveBtnClk);
		EngineeringBuildingSaveBtn.interactable = false;





		GreatCourtCamBtn = transform.FindChild ("CameraButtonROOT/GreatCourt").GetComponent<Button> ();
		GreatCourtCamBtn.onClick.AddListener (OnGreatCourtCamBtnClk);

		KingsCollegeCamBtn = transform.FindChild ("CameraButtonROOT/KingsCollege").GetComponent<Button> ();
		KingsCollegeCamBtn.onClick.AddListener (OnKingsCollegeCamBtnClk);

		OldHospitalCamBtn = transform.FindChild ("CameraButtonROOT/OldHospital").GetComponent<Button> ();
		OldHospitalCamBtn.onClick.AddListener (OnOldHospitalCamBtnClk);

		ShopFacadeCamBtn = transform.FindChild ("CameraButtonROOT/ShopFacade").GetComponent<Button> ();
		ShopFacadeCamBtn.onClick.AddListener (OnShopFacadeCamBtnClk);

		StMarysChurchCamBtn = transform.FindChild ("CameraButtonROOT/StMarysChurch").GetComponent<Button> ();
		StMarysChurchCamBtn.onClick.AddListener (OnStMarysChurchCamBtnClk);

		StreetCamBtn = transform.FindChild ("CameraButtonROOT/Street").GetComponent<Button> ();
		StreetCamBtn.onClick.AddListener (OnStreetCamBtnClk);

		OlympicGymCamBtn = transform.FindChild ("CameraButtonROOT/OlympicGym").GetComponent<Button> ();
		OlympicGymCamBtn.onClick.AddListener (OnOlympicGymCamBtnClk);

		EngineeringBuildingCamBtn = transform.FindChild ("CameraButtonROOT/EngineeringBuilding").GetComponent<Button> ();
		EngineeringBuildingCamBtn.onClick.AddListener (OnEngineeringBuildingCamBtnClk);





		GreatCourtCamSaveBtn = transform.FindChild ("CameraButtonROOT/GreatCourt_save").GetComponent<Button> ();
		GreatCourtCamSaveBtn.onClick.AddListener (OnGreatCourtCamSaveBtnClk);
		GreatCourtCamSaveBtn.interactable = false;

		KingsCollegeCamSaveBtn = transform.FindChild ("CameraButtonROOT/KingsCollege_save").GetComponent<Button> ();
		KingsCollegeCamSaveBtn.onClick.AddListener (OnKingsCollegeCamSaveBtnClk);
		KingsCollegeCamSaveBtn.interactable = false;

		OldHospitalCamSaveBtn = transform.FindChild ("CameraButtonROOT/OldHospital_save").GetComponent<Button> ();
		OldHospitalCamSaveBtn.onClick.AddListener (OnOldHospitalCamSaveBtnClk);
		OldHospitalCamSaveBtn.interactable = false;

		ShopFacadeCamSaveBtn = transform.FindChild ("CameraButtonROOT/ShopFacade_save").GetComponent<Button> ();
		ShopFacadeCamSaveBtn.onClick.AddListener (OnShopFacadeCamSaveBtnClk);
		ShopFacadeCamSaveBtn.interactable = false;

		StMarysChurchCamSaveBtn = transform.FindChild ("CameraButtonROOT/StMarysChurch_save").GetComponent<Button> ();
		StMarysChurchCamSaveBtn.onClick.AddListener (OnStMarysChurchCamSaveBtnClk);
		StMarysChurchCamSaveBtn.interactable = false;

		StreetCamSaveBtn = transform.FindChild ("CameraButtonROOT/Street_save").GetComponent<Button> ();
		StreetCamSaveBtn.onClick.AddListener (OnStreetCamSaveBtnClk);
		StreetCamSaveBtn.interactable = false;

		OlympicGymCamSaveBtn = transform.FindChild ("CameraButtonROOT/OlympicGym_save").GetComponent<Button> ();
		OlympicGymCamSaveBtn.onClick.AddListener (OnOlympicGymCamSaveBtnClk);
		OlympicGymCamSaveBtn.interactable = false;

		EngineeringBuildingCamSaveBtn = transform.FindChild ("CameraButtonROOT/EngineeringBuilding_save").GetComponent<Button> ();
		EngineeringBuildingCamSaveBtn.onClick.AddListener (OnEngineeringBuildingCamSaveBtnClk);
		EngineeringBuildingCamSaveBtn.interactable = false;

		if (CloseOnStart) {
			StartCoroutine (CleanScene ());
		}
	}


	IEnumerator CleanScene(){
		yield return new WaitForEndOfFrame ();
		OnGreatCourtBtnClk();
		yield return new WaitForEndOfFrame ();
		OnKingsCollegeBtnClk();
		yield return new WaitForEndOfFrame ();
		OnOldHospitalBtnClk();
		yield return new WaitForEndOfFrame ();
		OnShopFacadeBtnClk();
		yield return new WaitForEndOfFrame ();
		OnStMarysChurchBtnClk();
		yield return new WaitForEndOfFrame ();
		OnStreetBtnClk();
		yield return new WaitForEndOfFrame ();
		OnOlympicGymBtnClk();
		yield return new WaitForEndOfFrame ();
		OnEngineeringBuildingBtnClk();
	}






	void SetTansformData(GameObject gameobj, string PATH){
		if (File.Exists (Application.dataPath + "/Resources/Table/SavedData/" + PATH + ".txt")) {

			Table.CameraTableReader.Instance.ReadFromFile ("SavedData/" + PATH);
			Table.CameraLabel cameraLabel = Table.CameraTableReader.Instance.GetItem (0);

			gameobj.transform.position = new Vector3 (cameraLabel.X, -cameraLabel.Y, cameraLabel.Z);
			gameobj.transform.rotation = new Quaternion (cameraLabel.P, -cameraLabel.Q, cameraLabel.R, cameraLabel.W);

			if (cameraLabel.S > 0f) {
				gameobj.transform.localScale = new Vector3 (cameraLabel.S, cameraLabel.S, cameraLabel.S);
			}
		}
	}









	void OnGreatCourtBtnClk(){
		if (GreatCourtOFF.loaded && GreatCourtPointCloud == null) {
			if (GameObject.Find ("GreatCourt(Clone)") != null) {
				GreatCourtPointCloud = GameObject.Find ("GreatCourt(Clone)");
			} else if(GameObject.Find ("/GreatCourt") != null){
				GreatCourtPointCloud = GameObject.Find ("/GreatCourt");
			}

			SetTansformData (GreatCourtPointCloud,"GreatCourtTransform");
		}
		if (GreatCourtPointCloud != null) {
			GreatCourtPointCloud.SetActive (!GreatCourtPointCloud.activeSelf);
		}
		GreatCourtSaveBtn.interactable = GreatCourtPointCloud.activeSelf;
	}

	void OnKingsCollegeBtnClk(){
		if (KingsCollegeOFF.loaded && KingsCollegePointCloud == null) {
			if (GameObject.Find ("KingsCollege(Clone)") != null) {
				KingsCollegePointCloud = GameObject.Find ("KingsCollege(Clone)");
			} else if(GameObject.Find ("/KingsCollege") != null){
				KingsCollegePointCloud = GameObject.Find ("/KingsCollege");
			}

			SetTansformData (KingsCollegePointCloud,"KingsCollegeTransform");
		}
		if (KingsCollegePointCloud != null) {
			KingsCollegePointCloud.SetActive (!KingsCollegePointCloud.activeSelf);
		}
		KingsCollegeSaveBtn.interactable = KingsCollegePointCloud.activeSelf;
	}

	void OnOldHospitalBtnClk(){
		if (OldHospitalOFF.loaded && OldHospitalPointCloud == null) {
			if (GameObject.Find ("OldHospital(Clone)") != null) {
				OldHospitalPointCloud = GameObject.Find ("OldHospital(Clone)");
			} else if(GameObject.Find ("/OldHospital") != null){
				OldHospitalPointCloud = GameObject.Find ("/OldHospital");
			}

			SetTansformData (OldHospitalPointCloud,"OldHospitalTransform");
		}
		if (OldHospitalPointCloud != null) {
			OldHospitalPointCloud.SetActive (!OldHospitalPointCloud.activeSelf);
		}
		OldHospitalSaveBtn.interactable = OldHospitalPointCloud.activeSelf;
	}

	void OnShopFacadeBtnClk(){
		if (ShopFacadeOFF.loaded && ShopFacadePointCloud == null) {
			if (GameObject.Find ("ShopFacade(Clone)") != null) {
				ShopFacadePointCloud = GameObject.Find ("ShopFacade(Clone)");
			} else if(GameObject.Find ("/ShopFacade") != null){
				ShopFacadePointCloud = GameObject.Find ("/ShopFacade");
			}

			SetTansformData (ShopFacadePointCloud,"ShopFacadeTransform");
		}
		if (ShopFacadePointCloud != null) {
			ShopFacadePointCloud.SetActive (!ShopFacadePointCloud.activeSelf);
		}
		ShopFacadeSaveBtn.interactable = ShopFacadePointCloud.activeSelf;
	}

	void OnStMarysChurchBtnClk(){
		if (StMarysChurchOFF.loaded && StMarysChurchPointCloud == null) {
			if (GameObject.Find ("StMarysChurch(Clone)") != null) {
				StMarysChurchPointCloud = GameObject.Find ("StMarysChurch(Clone)");
			} else if(GameObject.Find ("/StMarysChurch") != null){
				StMarysChurchPointCloud = GameObject.Find ("/StMarysChurch");
			}

			SetTansformData (StMarysChurchPointCloud,"StMarysChurchTransform");
		}
		if (StMarysChurchPointCloud != null) {
			StMarysChurchPointCloud.SetActive (!StMarysChurchPointCloud.activeSelf);
		}
		StMarysChurchSaveBtn.interactable = StMarysChurchPointCloud.activeSelf;
	}

	void OnStreetBtnClk(){
		if (StreetOFF.loaded && StreetPointCloud == null) {
			if (GameObject.Find ("Street(Clone)") != null) {
				StreetPointCloud = GameObject.Find ("Street(Clone)");
			} else if(GameObject.Find ("/Street") != null){
				StreetPointCloud = GameObject.Find ("/Street");
			}

			SetTansformData (StreetPointCloud,"StreetTransform");
		}
		if (StreetPointCloud != null) {
			StreetPointCloud.SetActive (!StreetPointCloud.activeSelf);
		}
		StreetSaveBtn.interactable = StreetPointCloud.activeSelf;
	}

	//OlympicGym
	//EngineeringBuilding
	void OnOlympicGymBtnClk(){
		if (OlympicGymOFF.loaded && OlympicGymPointCloud == null) {
			if (GameObject.Find ("OlympicGym(Clone)") != null) {
				OlympicGymPointCloud = GameObject.Find ("OlympicGym(Clone)");
			} else if(GameObject.Find ("/OlympicGym") != null){
				OlympicGymPointCloud = GameObject.Find ("/OlympicGym");
			}

			SetTansformData (OlympicGymPointCloud,"OlympicGymTransform");
		}
		if (OlympicGymPointCloud != null) {
			OlympicGymPointCloud.SetActive (!OlympicGymPointCloud.activeSelf);
		}
		OlympicGymSaveBtn.interactable = OlympicGymPointCloud.activeSelf;
	}

	void OnEngineeringBuildingBtnClk(){
		if (EngineeringBuildingOFF.loaded && EngineeringBuildingPointCloud == null) {
			if (GameObject.Find ("EngineeringBuilding(Clone)") != null) {
				EngineeringBuildingPointCloud = GameObject.Find ("EngineeringBuilding(Clone)");
			} else if(GameObject.Find ("/EngineeringBuilding") != null){
				EngineeringBuildingPointCloud = GameObject.Find ("/EngineeringBuilding");
			}

			SetTansformData (EngineeringBuildingPointCloud,"EngineeringBuildingTransform");
		}
		if (EngineeringBuildingPointCloud != null) {
			EngineeringBuildingPointCloud.SetActive (!EngineeringBuildingPointCloud.activeSelf);
		}
		EngineeringBuildingSaveBtn.interactable = EngineeringBuildingPointCloud.activeSelf;
	}







	void OnGreatCourtSaveBtnClk(){
		if (GreatCourtPointCloud != null) {
			Table.CameraTableReader.Instance.SaveTransDataToCSV (GreatCourtPointCloud.transform, "GreatCourtTransform");
		}
	}

	void OnKingsCollegeSaveBtnClk(){
		if (KingsCollegePointCloud != null) {
			Table.CameraTableReader.Instance.SaveTransDataToCSV (KingsCollegePointCloud.transform, "KingsCollegeTransform");
		}
	}

	void OnOldHospitalSaveBtnClk(){
		if (OldHospitalPointCloud != null) {
			Table.CameraTableReader.Instance.SaveTransDataToCSV (OldHospitalPointCloud.transform, "OldHospitalTransform");
		}
	}

	void OnShopFacadeSaveBtnClk(){
		if (ShopFacadePointCloud != null) {
			Table.CameraTableReader.Instance.SaveTransDataToCSV (ShopFacadePointCloud.transform, "ShopFacadeTransform");
		}
	}

	void OnStMarysChurchSaveBtnClk(){
		if (StMarysChurchPointCloud != null) {
			Table.CameraTableReader.Instance.SaveTransDataToCSV (StMarysChurchPointCloud.transform, "StMarysChurchTransform");
		}
	}

	void OnStreetSaveBtnClk(){
		if (StreetPointCloud != null) {
			Table.CameraTableReader.Instance.SaveTransDataToCSV (StreetPointCloud.transform, "StreetTransform");
		}
	}

	//OlympicGym
	//EngineeringBuilding
	void OnOlympicGymSaveBtnClk(){
		if (OlympicGymPointCloud != null) {
			Table.CameraTableReader.Instance.SaveTransDataToCSV (OlympicGymPointCloud.transform, "OlympicGymTransform");
		}
	}

	void OnEngineeringBuildingSaveBtnClk(){
		if (EngineeringBuildingPointCloud != null) {
			Table.CameraTableReader.Instance.SaveTransDataToCSV (EngineeringBuildingPointCloud.transform, "EngineeringBuildingTransform");
		}
	}






	void OnGreatCourtCamBtnClk(){
		if (GreatCourtCamROOT == null) {
			GreatCourtCamROOT = new GameObject ();
			GreatCourtCamROOT.name = "GreatCourtCamROOT";

			CameraPoseVisualization.Instance.SetCamera ("GreatCourt_csv_dataset_train").transform.SetParent (GreatCourtCamROOT.transform);
			CameraPoseVisualization.Instance.SetCamera ("GreatCourt_csv_dataset_test").transform.SetParent (GreatCourtCamROOT.transform);

			SetTansformData (GreatCourtCamROOT,"GreatCourtTransform");
		} else {
			GreatCourtCamROOT.SetActive (!GreatCourtCamROOT.activeSelf);
		}
		GreatCourtCamSaveBtn.interactable = GreatCourtCamROOT.activeSelf;
	}

	void OnKingsCollegeCamBtnClk(){
		if (KingsCollegeCamROOT == null) {
			KingsCollegeCamROOT = new GameObject ();
			KingsCollegeCamROOT.name = "KingsCollegeCamROOT";

			CameraPoseVisualization.Instance.SetCamera ("KingsCollege_csv_dataset_train").transform.SetParent (KingsCollegeCamROOT.transform);
			CameraPoseVisualization.Instance.SetCamera ("KingsCollege_csv_dataset_test").transform.SetParent (KingsCollegeCamROOT.transform);

			SetTansformData (KingsCollegeCamROOT,"KingsCollegeTransform");
		} else {
			KingsCollegeCamROOT.SetActive (!KingsCollegeCamROOT.activeSelf);
		}
		KingsCollegeCamSaveBtn.interactable = KingsCollegeCamROOT.activeSelf;
	}

	void OnOldHospitalCamBtnClk(){
		if (OldHospitalCamROOT == null) {
			OldHospitalCamROOT = new GameObject ();
			OldHospitalCamROOT.name = "OldHospitalCamROOT";

			CameraPoseVisualization.Instance.SetCamera ("OldHospital_csv_dataset_train").transform.SetParent (OldHospitalCamROOT.transform);
			CameraPoseVisualization.Instance.SetCamera ("OldHospital_csv_dataset_test").transform.SetParent (OldHospitalCamROOT.transform);

			SetTansformData (OldHospitalCamROOT,"OldHospitalTransform");
		} else {
			OldHospitalCamROOT.SetActive (!OldHospitalCamROOT.activeSelf);
		}
		OldHospitalCamSaveBtn.interactable = OldHospitalCamROOT.activeSelf;
	}

	void OnShopFacadeCamBtnClk(){
		if (ShopFacadeCamROOT == null) {
			ShopFacadeCamROOT = new GameObject ();
			ShopFacadeCamROOT.name = "ShopFacadeCamROOT";

			CameraPoseVisualization.Instance.SetCamera ("ShopFacade_csv_dataset_train").transform.SetParent (ShopFacadeCamROOT.transform);
			CameraPoseVisualization.Instance.SetCamera ("ShopFacade_csv_dataset_test").transform.SetParent (ShopFacadeCamROOT.transform);

			SetTansformData (ShopFacadeCamROOT,"ShopFacadeTransform");
		} else {
			ShopFacadeCamROOT.SetActive (!ShopFacadeCamROOT.activeSelf);
		}
		ShopFacadeCamSaveBtn.interactable = ShopFacadeCamROOT.activeSelf;
	}

	void OnStMarysChurchCamBtnClk(){
		if (StMarysChurchCamROOT == null) {
			StMarysChurchCamROOT = new GameObject ();
			StMarysChurchCamROOT.name = "StMarysChurchCamROOT";

			CameraPoseVisualization.Instance.SetCamera ("StMarysChurch_csv_dataset_train").transform.SetParent (StMarysChurchCamROOT.transform);
			CameraPoseVisualization.Instance.SetCamera ("StMarysChurch_csv_dataset_test").transform.SetParent (StMarysChurchCamROOT.transform);

			SetTansformData (StMarysChurchCamROOT,"StMarysChurchTransform");
		} else {
			StMarysChurchCamROOT.SetActive (!StMarysChurchCamROOT.activeSelf);
		}
		StMarysChurchCamSaveBtn.interactable = StMarysChurchCamROOT.activeSelf;
	}

	void OnStreetCamBtnClk(){
		if (StreetCamROOT == null) {
			StreetCamROOT = new GameObject ();
			StreetCamROOT.name = "StreetCamROOT";

			CameraPoseVisualization.Instance.SetCamera ("Street_csv_dataset_train").transform.SetParent (StreetCamROOT.transform);
			CameraPoseVisualization.Instance.SetCamera ("Street_csv_dataset_test").transform.SetParent (StreetCamROOT.transform);

			SetTansformData (StreetCamROOT,"StreetTransform");
		} else {
			StreetCamROOT.SetActive (!StreetCamROOT.activeSelf);
		}
		StreetCamSaveBtn.interactable = StreetCamROOT.activeSelf;
	}

	//OlympicGym
	//EngineeringBuilding
	void OnOlympicGymCamBtnClk(){
		if (OlympicGymCamROOT == null) {
			OlympicGymCamROOT = new GameObject ();
			OlympicGymCamROOT.name = "OlympicGymCamROOT";

			CameraPoseVisualization.Instance.SetCamera ("OlympicGym_csv_dataset_train").transform.SetParent (OlympicGymCamROOT.transform);
			CameraPoseVisualization.Instance.SetCamera ("OlympicGym_csv_dataset_test").transform.SetParent (OlympicGymCamROOT.transform);

			SetTansformData (OlympicGymCamROOT,"OlympicGymTransform");
		} else {
			OlympicGymCamROOT.SetActive (!OlympicGymCamROOT.activeSelf);
		}
		OlympicGymCamSaveBtn.interactable = OlympicGymCamROOT.activeSelf;
	}

	void OnEngineeringBuildingCamBtnClk(){
		if (EngineeringBuildingCamROOT == null) {
			EngineeringBuildingCamROOT = new GameObject ();
			EngineeringBuildingCamROOT.name = "EngineeringBuildingCamROOT";

			CameraPoseVisualization.Instance.SetCamera ("EngineeringBuilding_csv_dataset_train").transform.SetParent (EngineeringBuildingCamROOT.transform);
			CameraPoseVisualization.Instance.SetCamera ("EngineeringBuilding_csv_dataset_test").transform.SetParent (EngineeringBuildingCamROOT.transform);

			SetTansformData (EngineeringBuildingCamROOT,"EngineeringBuildingTransform");
		} else {
			EngineeringBuildingCamROOT.SetActive (!EngineeringBuildingCamROOT.activeSelf);
		}
		EngineeringBuildingCamSaveBtn.interactable = EngineeringBuildingCamROOT.activeSelf;
	}







	void OnGreatCourtCamSaveBtnClk(){
		CameraPoseVisualization.Instance.SaveCameraData ("GreatCourt_csv_dataset_test");
		CameraPoseVisualization.Instance.SaveCameraData ("GreatCourt_csv_dataset_train");
	}

	void OnKingsCollegeCamSaveBtnClk(){
		CameraPoseVisualization.Instance.SaveCameraData ("KingsCollege_csv_dataset_test");
		CameraPoseVisualization.Instance.SaveCameraData ("KingsCollege_csv_dataset_train");
	}

	void OnOldHospitalCamSaveBtnClk(){
		CameraPoseVisualization.Instance.SaveCameraData ("OldHospital_csv_dataset_test");
		CameraPoseVisualization.Instance.SaveCameraData ("OldHospital_csv_dataset_train");
	}

	void OnShopFacadeCamSaveBtnClk(){
		CameraPoseVisualization.Instance.SaveCameraData ("ShopFacade_csv_dataset_test");
		CameraPoseVisualization.Instance.SaveCameraData ("ShopFacade_csv_dataset_train");
	}

	void OnStMarysChurchCamSaveBtnClk(){
		CameraPoseVisualization.Instance.SaveCameraData ("StMarysChurch_csv_dataset_test");
		CameraPoseVisualization.Instance.SaveCameraData ("StMarysChurch_csv_dataset_train");
	}

	void OnStreetCamSaveBtnClk(){
		CameraPoseVisualization.Instance.SaveCameraData ("Street_csv_dataset_test");
		CameraPoseVisualization.Instance.SaveCameraData ("Street_csv_dataset_train");
	}

	//OlympicGym
	//EngineeringBuilding
	void OnOlympicGymCamSaveBtnClk(){
		CameraPoseVisualization.Instance.SaveCameraData ("OlympicGym_csv_dataset_test");
		CameraPoseVisualization.Instance.SaveCameraData ("OlympicGym_csv_dataset_train");
	}

	void OnEngineeringBuildingCamSaveBtnClk(){
		CameraPoseVisualization.Instance.SaveCameraData ("EngineeringBuilding_csv_dataset_test");
		CameraPoseVisualization.Instance.SaveCameraData ("EngineeringBuilding_csv_dataset_train");
	}
}
