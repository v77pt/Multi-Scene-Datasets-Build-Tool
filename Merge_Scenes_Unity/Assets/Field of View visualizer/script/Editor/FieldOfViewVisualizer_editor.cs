using UnityEngine;
using UnityEditor;
using System;
using System.Collections;

[CustomEditor(typeof(FieldOfViewVisualizer))] 
[System.Serializable]
public class FieldOfViewVisualizer_editor : Editor {

	private Texture2D head;
	private string layerNameForFov;
	private Material material;
	private bool init;

	void OnEnable() {
		head = Resources.Load("header_fov", typeof(Texture2D)) as Texture2D;
	}

	public override void OnInspectorGUI() {
		if (!head)		return;
		FieldOfViewVisualizer tgt = target as FieldOfViewVisualizer;
		if (tgt == null)		return;
		EditorGUIUtility.labelWidth = 250.0f;
		EditorGUILayout.BeginVertical();
			EditorGUILayout.BeginHorizontal();
				GUILayout.FlexibleSpace();
					DrawBox(Color.white, head);
				GUILayout.FlexibleSpace();
			EditorGUILayout.EndHorizontal();
			EditorGUILayout.Space();

		// Camera check
		if (tgt.GetComponent<Camera>() == null) {
			EditorGUILayout.HelpBox("This component must be used on a camera only.", MessageType.Error);
			EditorGUILayout.EndVertical();
			return;
		}

		// Get layer for the FOV visualizer
		layerNameForFov = LayerMask.LayerToName(EditorGUILayout.LayerField("Layer ", LayerMask.NameToLayer(tgt.layerNameForFov)));
		if (tgt.layerNameForFov != layerNameForFov || init) {
			tgt.layerNameForFov = layerNameForFov;
			tgt.fov.layer =  LayerMask.NameToLayer(layerNameForFov);
			// Set the camera cullingmask to avoid seeing the visualizer through the camera.
			if (layerNameForFov != "Default")
				tgt.GetComponent<Camera>().cullingMask &= ~(1 << LayerMask.NameToLayer(layerNameForFov));
			if (tgt.layerNameForFov == "")
				tgt.layerNameForFov = LayerMask.LayerToName(0);
		}

		// Material
		material = EditorGUILayout.ObjectField(new GUIContent("Material", "The override material for the field of view visualizer"), tgt.material, typeof(Material), false) as Material;
		if (tgt.material != material || init) {
			tgt.material = material;
			if (tgt.material == null)		material = tgt.default_material;
			#if UNITY_EDITOR
				((MeshRenderer) tgt.fov.GetComponent<MeshRenderer>()).sharedMaterial = material;
			#else
				((MeshRenderer) tgt.fov.GetComponent<MeshRenderer>()).material = material;
			#endif

			init = true;
		}


		EditorGUILayout.EndVertical();
		Repaint();
	}


	public static void DrawBox(Color color, Texture2D image) {
		Texture2D texture = new Texture2D(1, 1);
		texture.SetPixel(0,0,color);
		texture.Apply();
		GUI.skin.box.normal.background = texture;
		GUILayout.Box(image, GUILayout.Width(Screen.width-30));
		GUI.skin.box.normal.background = null;
	}

}
