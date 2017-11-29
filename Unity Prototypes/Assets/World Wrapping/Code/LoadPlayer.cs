using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadPlayer : MonoBehaviour {

    public GameObject Player;
    public Transform Position;

	// Use this for initialization
	void Start () {
        Instantiate(Player, Position.position, Quaternion.identity);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
