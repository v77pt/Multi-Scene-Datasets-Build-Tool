using UnityEngine;
using System.Collections;

/**
 * 		Visualize the FOV by creating a pyramid fitting the camera field of view.
 */
[AddComponentMenu("Tools/Field of View Visualizer")]
[ExecuteInEditMode()]
public class FieldOfViewVisualizer : MonoBehaviour {
	
	 /// 	Layer Name for the fov
	public string layerNameForFov;
	/// 	The material.of the Field of View Visualizer
	public Material material;
	
	public GameObject fov;
	[SerializeField]
	private Mesh mesh;
	[SerializeField]
	private Camera cam;

	public Material default_material;

	void OnDestroy() {
		if (fov != null)
			DestroyImmediate(fov);
	}

	void Start () {
		if (fov == null) {
			fov = (GameObject) Instantiate(Resources.Load("FOV"));
			#if UNITY_EDITOR
				default_material = (fov.GetComponent<MeshRenderer>() as MeshRenderer).sharedMaterial;
			#else
				default_material = (fov.GetComponent<MeshRenderer>() as MeshRenderer).material;
			#endif
		}
		cam = (Camera) this.GetComponent<Camera>();
		if (fov == null || cam == null)		return;
		// Assign layer
		if (layerNameForFov == "")	layerNameForFov = "Default";
		fov.layer = LayerMask.NameToLayer(layerNameForFov);

		// Assign a material if there is one.
		if (material != null) {
			#if UNITY_EDITOR
				((MeshRenderer) fov.GetComponent<MeshRenderer>()).sharedMaterial = material;
			#else
				((MeshRenderer) fov.GetComponent<MeshRenderer>()).material = material;
			#endif
		}
		fov.transform.parent = cam.transform;
		fov.transform.localPosition = Vector3.zero;
		fov.transform.localRotation = Quaternion.Euler(0f, 180f,0f) * cam.transform.localRotation;
		MeshFilter mf = fov.GetComponent<MeshFilter>() as MeshFilter;
		#if UNITY_EDITOR
			mesh = mf.sharedMesh;
		#else
			mesh = mf.mesh;
		#endif
	}
	
	void Update () {
		if (fov == null || cam == null)		return;
		Vector3[] f = new Vector3[28];

		Vector2 scaleFactor = new Vector2(cam.rect.width, cam.rect.height);
		Vector2 offset = new Vector2(cam.rect.x * Screen.width, cam.rect.y*Screen.height);

		// ---------------  FAR CLIP
		// Center vertex
		f[0] = cam.ScreenToWorldPoint(new Vector3((Screen.width*0.5f*scaleFactor.x)+offset.x,(Screen.height*0.5f*scaleFactor.y)+offset.y, cam.farClipPlane));
		// Top right vertex
		f[1] = cam.ScreenToWorldPoint(new Vector3((Screen.width*1f*scaleFactor.x)+offset.x,(Screen.height*1f*scaleFactor.y)+offset.y, cam.farClipPlane));
		f[5] = cam.ScreenToWorldPoint(new Vector3((Screen.width*1f*scaleFactor.x)+offset.x,(Screen.height*1f*scaleFactor.y)+offset.y, cam.farClipPlane));
		f[6] = cam.ScreenToWorldPoint(new Vector3((Screen.width*1f*scaleFactor.x)+offset.x,(Screen.height*1f*scaleFactor.y)+offset.y, cam.farClipPlane));
		f[21] = cam.ScreenToWorldPoint(new Vector3((Screen.width*1f*scaleFactor.x)+offset.x,(Screen.height*1f*scaleFactor.y)+offset.y, cam.farClipPlane));
		// Top left vertex
		f[2] = cam.ScreenToWorldPoint(new Vector3((Screen.width*0f*scaleFactor.x)+offset.x,(Screen.height*1f*scaleFactor.y)+offset.y, cam.farClipPlane));
		f[9] = cam.ScreenToWorldPoint(new Vector3((Screen.width*0f*scaleFactor.x)+offset.x,(Screen.height*1f*scaleFactor.y)+offset.y, cam.farClipPlane));
		f[10] = cam.ScreenToWorldPoint(new Vector3((Screen.width*0f*scaleFactor.x)+offset.x,(Screen.height*1f*scaleFactor.y)+offset.y, cam.farClipPlane));
		// Bottom left vertex
		f[3] = cam.ScreenToWorldPoint(new Vector3((Screen.width*0f*scaleFactor.x)+offset.x,(Screen.height*0f*scaleFactor.y)+offset.y, cam.farClipPlane));
		f[13] = cam.ScreenToWorldPoint(new Vector3((Screen.width*0f*scaleFactor.x)+offset.x,(Screen.height*0f*scaleFactor.y)+offset.y, cam.farClipPlane));
		f[14] = cam.ScreenToWorldPoint(new Vector3((Screen.width*0f*scaleFactor.x)+offset.x,(Screen.height*0f*scaleFactor.y)+offset.y, cam.farClipPlane));
		// Bottom right vertex
		f[4] = cam.ScreenToWorldPoint(new Vector3((Screen.width*1f*scaleFactor.x)+offset.x,(Screen.height*0f*scaleFactor.y)+offset.y, cam.farClipPlane));
		f[17] = cam.ScreenToWorldPoint(new Vector3((Screen.width*1f*scaleFactor.x)+offset.x,(Screen.height*0f*scaleFactor.y)+offset.y, cam.farClipPlane));
		f[18] = cam.ScreenToWorldPoint(new Vector3((Screen.width*1f*scaleFactor.x)+offset.x,(Screen.height*0f*scaleFactor.y)+offset.y, cam.farClipPlane));

		// ------------- NEAR CLIP
		// Center vertex
		f[22] = cam.ScreenToWorldPoint(new Vector3((Screen.width*0.5f*scaleFactor.x)+offset.x,(Screen.height*0.5f*scaleFactor.y)+offset.y, cam.nearClipPlane));
		// Top right vertex
		f[7] = cam.ScreenToWorldPoint(new Vector3((Screen.width*1f*scaleFactor.x)+offset.x,(Screen.height*1f*scaleFactor.y)+offset.y, cam.nearClipPlane));
		f[20] = cam.ScreenToWorldPoint(new Vector3((Screen.width*1f*scaleFactor.x)+offset.x,(Screen.height*1f*scaleFactor.y)+offset.y, cam.nearClipPlane));
		f[24] = cam.ScreenToWorldPoint(new Vector3((Screen.width*1f*scaleFactor.x)+offset.x,(Screen.height*1f*scaleFactor.y)+offset.y, cam.nearClipPlane));
		f[27] = cam.ScreenToWorldPoint(new Vector3((Screen.width*1f*scaleFactor.x)+offset.x,(Screen.height*1f*scaleFactor.y)+offset.y, cam.nearClipPlane));
		// Top left vertex
		f[8] = cam.ScreenToWorldPoint(new Vector3((Screen.width*0f*scaleFactor.x)+offset.x,(Screen.height*1f*scaleFactor.y)+offset.y, cam.nearClipPlane));
		f[11] = cam.ScreenToWorldPoint(new Vector3((Screen.width*0f*scaleFactor.x)+offset.x,(Screen.height*1f*scaleFactor.y)+offset.y, cam.nearClipPlane));
		f[23] = cam.ScreenToWorldPoint(new Vector3((Screen.width*0f*scaleFactor.x)+offset.x,(Screen.height*1f*scaleFactor.y)+offset.y, cam.nearClipPlane));
		// Bottom left vertex
		f[12] = cam.ScreenToWorldPoint(new Vector3((Screen.width*0f*scaleFactor.x)+offset.x,(Screen.height*0f*scaleFactor.y)+offset.y, cam.nearClipPlane));
		f[15] = cam.ScreenToWorldPoint(new Vector3((Screen.width*0f*scaleFactor.x)+offset.x,(Screen.height*0f*scaleFactor.y)+offset.y, cam.nearClipPlane));
		f[25] = cam.ScreenToWorldPoint(new Vector3((Screen.width*0f*scaleFactor.x)+offset.x,(Screen.height*0f*scaleFactor.y)+offset.y, cam.nearClipPlane));
		// Bottom right vertex
		f[16] = cam.ScreenToWorldPoint(new Vector3((Screen.width*1f*scaleFactor.x)+offset.x,(Screen.height*0f*scaleFactor.y)+offset.y, cam.nearClipPlane));
		f[19] = cam.ScreenToWorldPoint(new Vector3((Screen.width*1f*scaleFactor.x)+offset.x,(Screen.height*0f*scaleFactor.y)+offset.y, cam.nearClipPlane));
		f[26] = cam.ScreenToWorldPoint(new Vector3((Screen.width*1f*scaleFactor.x)+offset.x,(Screen.height*0f*scaleFactor.y)+offset.y, cam.nearClipPlane));

		Vector3[] vertices = mesh.vertices;
		for (int i=0; i<f.Length; i++) {
			vertices[i] = fov.transform.InverseTransformPoint(f[i]);
		}
		//point = fov.transform.TransformPoint(vertices[27]);
		//point = f[27];
		mesh.vertices = vertices;
	}

	//Vector3 point;

	/*void OnDrawGizmos() {
		Gizmos.DrawSphere(point, 0.1f);
	}*/

}
