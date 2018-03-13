using UnityEngine;

public class Settings : MonoBehaviour {

	public GameObject settingsPanel;

	private bool isUse = false;

	public void ToggleSettingsPanel () {
//		if (isUse) {
//			settingsPanel.SetActive (false);
//		} else {
//			settingsPanel.SetActive (true);
//		}
		isUse = !isUse;
		print ("Toggle settings panel");
	}

}
