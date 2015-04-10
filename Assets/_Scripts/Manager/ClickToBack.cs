using UnityEngine;
using System.Collections;

public class ClickToBack : MonoBehaviour {
    public string nameToNavigate;
	void Update () {
        if (Input.GetKeyDown(KeyCode.Escape) && nameToNavigate != "OUT")
            Application.LoadLevel(nameToNavigate);
        if (Input.GetKeyDown(KeyCode.Escape) && nameToNavigate == "OUT")
            Application.Quit();
	}
}
