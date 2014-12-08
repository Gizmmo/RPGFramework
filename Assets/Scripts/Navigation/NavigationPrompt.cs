using UnityEngine;
using System.Collections;

public class NavigationPrompt : MonoBehaviour {

	private bool showDialog;


	void OnCollisionEnter2D(Collision2D col) {
		if (NavigationManager.CanNavigate(this.tag)) showDialog = true;
	}
	

	void OnCollisionExit2D (Collision2D xol) {
		showDialog = false;
	}

	void OnGUI() {
		if (showDialog) {
			//layout start
			GUI.BeginGroup(new Rect(Screen.width / 2 - 150, 50, 300, 250));

			//The menu background box
			GUI.Box (new Rect(0, 0, 300, 250), "");

			//Information text
			GUI.Label (new Rect(15, 10, 300, 68), "Do you want to travel to " + NavigationManager.GetRouteInfo(this.tag) + "?");

			//Plater wants to leave this location
			if (GUI.Button (new Rect(55, 100, 180, 40), "Travel")) {
				showDialog = false;
				NavigationManager.NavigateTo(this.tag);
			}

			//Player wans to stay at this location
			if (GUI.Button (new Rect(55, 150, 180, 40), "Stay")) {
				showDialog = false;
			}

			//layout end
			GUI.EndGroup();
		}
	}
}
