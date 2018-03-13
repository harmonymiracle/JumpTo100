using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;


public class LocalizationManager : MonoBehaviour {

	public static LocalizationManager instance;

	private Dictionary<string, string> localizedText;

	private bool isReady = false;
	private string missingTextString = "missing";


	void Awake () {
		if (instance == null) {
			instance = this;
		} else if (instance != this){
			Destroy (gameObject);
		}

		DontDestroyOnLoad (gameObject);
	}

	public void LoadLocalizedText (string fileName) {
		localizedText = new Dictionary<string, string> ();
		string filePath = Path.Combine (Application.streamingAssetsPath, fileName);

		if (File.Exists (filePath)) {
			string dataAsJson = File.ReadAllText (filePath);
			LocalizationData loadedData = JsonUtility.FromJson<LocalizationData> (dataAsJson);

			// 因为 Unity 不支持 无序 字典等结构的 序列化，因此我们需要将其 转化为数组，使其能够序列化。
			for (int i = 0; i < loadedData.items.Length; i++) {
				localizedText.Add (loadedData.items [i].key, loadedData.items [i].value);

			}

			Debug.Log ("Data loaded, dictiory Contains：" + localizedText.Count);

		} else {
			Debug.LogError ("cannot find file!");
		}

		isReady = true;
	}

	public string GetLocalizedValue (string key) {
		string result = missingTextString;
		if (localizedText.ContainsKey (key)) {
			result = localizedText [key];
		}

		return result;
	}

	public bool GetIsReady () {
		return isReady;
	}


}
// class















