/// <summary>
/// Author: Cheers K
/// Date: 3/12/2018
/// </summary>


using System.Collections;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;

public class TextEditor : EditorWindow {

	public Text[] allTexts;

	// below are relative property in Text component
	public Font font;
	public FontStyle fontStyle;
	public uint fontSize;
	public float fontSpacing;
	public bool richText;

	public TextAnchor textAlignment = TextAnchor.MiddleCenter;
	public bool alignByGeo;
	public HorizontalWrapMode horizontalWrap = HorizontalWrapMode.Wrap;
	public VerticalWrapMode verticalWrap = VerticalWrapMode.Truncate;
	public bool bestFit;

	public Color color = Color.white;
	public Material material;
	public bool raycastTarget;

	// if just want to inspector the Text, uncheck it
	public bool applyTextToggle = false;

	// use for the scroll view
	private Vector2 scrollPos = Vector2.zero;

	[MenuItem ("My Window/Text Editor")]
	static void Init () {
		
		EditorWindow window =  EditorWindow.GetWindow (typeof(TextEditor));
		window.minSize = new Vector2 (500, 500);

		window.Show ();

	}

	private void OnGUI () {
		GUILayout.Space (15f);
		if (GUILayout.Button ("Find All Texts In This Scene")) {
			FindAllTexts ();
		}

		SerializedObject serializedObject = new SerializedObject (this);
		SerializedProperty textProperty = serializedObject.FindProperty ("allTexts");
		SerializedProperty applyToggleProperty = serializedObject.FindProperty ("applyTextToggle");


		scrollPos = GUILayout.BeginScrollView (scrollPos);

		EditorGUILayout.PropertyField (textProperty, true);
		GUILayout.Space (20f);

		EditorGUILayout.PropertyField (applyToggleProperty, true);
		if (applyTextToggle) {
			Configuration (serializedObject);
		}


		// This is very important, if delete it, you cannot modify the values above.
		serializedObject.ApplyModifiedProperties ();

		EditorGUILayout.HelpBox ("You can configue every property, and Apply this property" +
		"on every Text object in allTexts, if you want certain one change, remove it from" +
			" the array.",MessageType.Info);
		GUILayout.EndScrollView ();
	}

	#region Configuration

	private void Configuration (SerializedObject serializedObject) {

		SerializedProperty fontProperty = serializedObject.FindProperty ("font");
		SerializedProperty fontStyleProperty = serializedObject.FindProperty ("fontStyle");
		SerializedProperty fontSizeProperty = serializedObject.FindProperty ("fontSize");
		SerializedProperty fontSpacingProperty = serializedObject.FindProperty ("fontSpacing");
		SerializedProperty richTextProperty = serializedObject.FindProperty ("richText");

		SerializedProperty textAlignmentProperty = serializedObject.FindProperty ("textAlignment");
		SerializedProperty alignByGeoProperty = serializedObject.FindProperty ("alignByGeo");
		SerializedProperty horizontalWrapProperty = serializedObject.FindProperty ("horizontalWrap");
		SerializedProperty verticalWrapProperty = serializedObject.FindProperty ("verticalWrap");
		SerializedProperty bestFitProperty = serializedObject.FindProperty ("bestFit");

		SerializedProperty colorProperty = serializedObject.FindProperty ("color");
		SerializedProperty materialProperty = serializedObject.FindProperty ("material");
		SerializedProperty raycastTargetProperty = serializedObject.FindProperty ("raycastTarget");

		GUILayout.Space (30f);

		#region Character
		GUILayout.Label ("Character");
		GUILayout.Space (10f);
		GUILayout.BeginHorizontal ();
		GUILayout.Space (30f);

		GUILayout.BeginVertical ();

		GUILayout.BeginHorizontal ();
		EditorGUILayout.PropertyField (fontProperty, true);
		if (GUILayout.Button ("Apply All")) {
			ApplyFont ();
		}
		GUILayout.EndHorizontal ();

		GUILayout.BeginHorizontal ();
		EditorGUILayout.PropertyField (fontStyleProperty, true);
		if (GUILayout.Button ("Apply All")) {
			ApplyFontStyle ();
		}
		GUILayout.EndHorizontal ();

		GUILayout.BeginHorizontal ();
		EditorGUILayout.PropertyField (fontSizeProperty, true);
		if (GUILayout.Button ("Apply All")) {
			ApplyFontSize ();
		}
		GUILayout.EndHorizontal ();

		GUILayout.BeginHorizontal ();
		EditorGUILayout.PropertyField (fontSpacingProperty, true);
		if (GUILayout.Button ("Apply All")) {
			ApplySpacing ();
		}
		GUILayout.EndHorizontal ();

		GUILayout.BeginHorizontal ();
		EditorGUILayout.PropertyField (richTextProperty, true);
		if (GUILayout.Button ("Apply All")) {
			ApplyRichText ();
		}
		GUILayout.EndHorizontal ();

		GUILayout.EndVertical ();

		GUILayout.EndHorizontal ();
		#endregion


		#region Paragraph
		GUILayout.Label ("Paragraph");
		GUILayout.Space (10f);
		GUILayout.BeginHorizontal ();
		GUILayout.Space (30f);

		GUILayout.BeginVertical();

		GUILayout.BeginHorizontal ();
		EditorGUILayout.PropertyField (textAlignmentProperty, true);
		if (GUILayout.Button ("Apply All")) {
			ApplyAlignment ();
		}
		GUILayout.EndHorizontal ();

		GUILayout.BeginHorizontal ();
		EditorGUILayout.PropertyField (alignByGeoProperty, true);
		if (GUILayout.Button ("Apply All")) {
			ApplyAlignByGeo ();
		}
		GUILayout.EndHorizontal ();

		GUILayout.BeginHorizontal ();
		EditorGUILayout.PropertyField (horizontalWrapProperty, true);
		if (GUILayout.Button ("Apply All")) {
			ApplyHorizontalWrap ();
		}
		GUILayout.EndHorizontal ();

		GUILayout.BeginHorizontal ();
		EditorGUILayout.PropertyField (verticalWrapProperty, true);
		if (GUILayout.Button ("Apply All")) {
			ApplyVertiacalWrap ();
		}
		GUILayout.EndHorizontal ();

		GUILayout.BeginHorizontal ();
		EditorGUILayout.PropertyField (bestFitProperty, true);
		if (GUILayout.Button ("Apply All")) {
			ApplyBestFit ();
		}
		GUILayout.EndHorizontal ();



		GUILayout.EndVertical();


		GUILayout.EndHorizontal();
		#endregion


		#region other
		GUILayout.BeginHorizontal ();
		EditorGUILayout.PropertyField (colorProperty, true);
		if (GUILayout.Button ("Apply All")) {
			ApplyColor ();
		}
		GUILayout.EndHorizontal ();

		GUILayout.BeginHorizontal ();
		EditorGUILayout.PropertyField (materialProperty, true);
		if (GUILayout.Button ("Apply All")) {
			ApplyMaterial ();
		}
		GUILayout.EndHorizontal ();

		GUILayout.BeginHorizontal ();
		EditorGUILayout.PropertyField (raycastTargetProperty, true);
		if (GUILayout.Button ("Apply All")) {
			ApplyRaycastTarget ();
		}
		GUILayout.EndHorizontal ();

		#endregion
	}

	#endregion

	#region function

	void ApplyFont () {
		for (int i = 0; i < allTexts.Length; i++) {
			if (allTexts [i] != null) {
				allTexts [i].font = font;
			}
		}
	}

	void ApplyFontSize () {
		for (int i = 0; i < allTexts.Length; i++) {
			if (allTexts [i] != null) {
				allTexts [i].fontSize = (int)fontSize;
			}
		}
	}

	void ApplyFontStyle () {
		for (int i = 0; i < allTexts.Length; i++) {
			if (allTexts [i] != null) {
				allTexts [i].fontStyle = fontStyle;
			}
		}
	}
	void ApplySpacing () {
		for (int i = 0; i < allTexts.Length; i++) {
			if (allTexts [i] != null) {
				allTexts [i].lineSpacing = fontSpacing;
			}
		}
	}
	void ApplyRichText () {
		for (int i = 0; i < allTexts.Length; i++) {
			if (allTexts [i] != null) {
				allTexts [i].supportRichText = richText;
			}
		}
	}
	void ApplyAlignment () {
		for (int i = 0; i < allTexts.Length; i++) {
			if (allTexts [i] != null) {
				allTexts [i].alignment = textAlignment;
			}
		}
	}

	void ApplyAlignByGeo () {
		for (int i = 0; i < allTexts.Length; i++) {
			if (allTexts [i] != null) {
				allTexts [i].alignByGeometry = alignByGeo;
			}
		}
	}

	void ApplyHorizontalWrap () {
		for (int i = 0; i < allTexts.Length; i++) {
			if (allTexts [i] != null) {
				allTexts [i].horizontalOverflow = horizontalWrap;
			}
		}
	}
	void ApplyVertiacalWrap () {
		for (int i = 0; i < allTexts.Length; i++) {
			if (allTexts [i] != null) {
				allTexts [i].verticalOverflow = verticalWrap;
			}
		}
	}

	void ApplyBestFit () {
		for (int i = 0; i < allTexts.Length; i++) {
			if (allTexts [i] != null) {
				allTexts [i].resizeTextForBestFit = bestFit;
			}
		}
	}


	void ApplyColor () {
		for (int i = 0; i < allTexts.Length; i++) {
			if (allTexts [i] != null) {
				allTexts [i].color = color;
			}
		}
	}


	void ApplyMaterial () {
		for (int i = 0; i < allTexts.Length; i++) {
			if (allTexts [i] != null) {
				allTexts [i].material = material;
			}
		}
	}

	void ApplyRaycastTarget () {
		for (int i = 0; i < allTexts.Length; i++) {
			if (allTexts [i] != null) {
				allTexts [i].raycastTarget = raycastTarget;
			}
		}
	}
	#endregion


	private void FindAllTexts () {
		allTexts = SceneAsset.FindObjectsOfType<Text> ();
	}

	

}
