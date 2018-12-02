using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reload : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("RELOAD");
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("RELOAD TRIGGER");
    }
}
