using UnityEngine;
using System.Collections;

public class SetPosition : MonoBehaviour {

    public GameObject destination;

	// Use this for initialization
	void Start () {
        this.transform.position = destination.transform.position;
	}
	

}
