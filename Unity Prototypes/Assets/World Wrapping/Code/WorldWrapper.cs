using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldWrapper : MonoBehaviour {

    public bool UniformSize = false;
    public Vector3 WorldSize = Vector3.one;
    private Vector3 LastSize = Vector3.one;

    public bool IsWireframe = true;
    public Color DebugColor = new Color(1, 0, 0, 0.5F);

    public int Itterations = 1;

    private bool CapturedWorld = false;
    private GameObject[] World;

    private GameObject[] CloneWorlds;

    public GameObject Player = null;

    void Awake () {
        World = Capture();
        CloneWorlds = Clone();
	}
	
	// Update is called once per frame
	void Update () {
        if (Player == null)
        {
            Player = GameObject.FindGameObjectsWithTag("Player")[0];
        }
        if (Player.transform.position.x > WorldSize.x / 2)
            Player.transform.position = new Vector3(WorldSize.x / -2, Player.transform.position.y, Player.transform.position.z);
        if (Player.transform.position.x < WorldSize.x / -2)
            Player.transform.position = new Vector3(WorldSize.x / 2, Player.transform.position.y, Player.transform.position.z);

        if (Player.transform.position.y > WorldSize.y / 2)
            Player.transform.position = new Vector3(Player.transform.position.x, WorldSize.y / -2, Player.transform.position.z);
        if (Player.transform.position.y < WorldSize.y / -2)
            Player.transform.position = new Vector3(Player.transform.position.x, WorldSize.y / 2, Player.transform.position.z);

        if (Player.transform.position.z > WorldSize.z / 2)
            Player.transform.position = new Vector3(Player.transform.position.x, Player.transform.position.y, WorldSize.z / -2);
        if (Player.transform.position.z < WorldSize.z / -2)
            Player.transform.position = new Vector3(Player.transform.position.x, Player.transform.position.y, WorldSize.z / 2);
    }

    GameObject[] Capture()
    {
        List<GameObject> contents = new List<GameObject>();
        Collider[] cols = Physics.OverlapBox(transform.position, WorldSize / 2);
        //Debug.Log(cols.Length);
        foreach (Collider col in cols)
        {
            contents.Add(col.gameObject);
        }
        CapturedWorld = true;
        return contents.ToArray();
    }

    GameObject[] Clone()
    {
        List<GameObject> clones = new List<GameObject>();
        Vector3 origin = transform.position;
        int size = Itterations;
        float offX = WorldSize.x;
        float offY = WorldSize.y;
        float offZ = WorldSize.z;
        for (int i = -size; i <= size; i++)
        {
            for (int j = -size; j <= size; j++)
            {
                for (int k = -size; k <= size; k++)
                {
                    if (i == 0 && j == 0 && k == 0)
                    {
                    }
                    else
                    {
                        foreach (GameObject obj in World)
                        {
                            Quaternion rot = obj.transform.rotation;
                            clones.Add(Instantiate(obj, origin + obj.transform.position + new Vector3(i * offX, j * offY, k * offZ), rot));
                        }
                    }
                }
            }
            //foreach (GameObject obj in World)
            //{
            //    Quaternion rot = obj.transform.rotation;
            //    //top
            //    clones.Add(Instantiate(obj, origin + obj.transform.position + new Vector3(offX, offY, offZ), rot));
            //    clones.Add(Instantiate(obj, origin + obj.transform.position + new Vector3(offX, offY, 0), rot));
            //    clones.Add(Instantiate(obj, origin + obj.transform.position + new Vector3(0, offY, offZ), rot));
            //    clones.Add(Instantiate(obj, origin + obj.transform.position + new Vector3(offX, offY, -offZ), rot));
            //    clones.Add(Instantiate(obj, origin + obj.transform.position + new Vector3(-offX, offY, -offZ), rot));
            //    clones.Add(Instantiate(obj, origin + obj.transform.position + new Vector3(0, offY, -offZ), rot));
            //    clones.Add(Instantiate(obj, origin + obj.transform.position + new Vector3(-offX, offY, offZ), rot));
            //    clones.Add(Instantiate(obj, origin + obj.transform.position + new Vector3(-offX, offY, 0), rot));
            //    clones.Add(Instantiate(obj, origin + obj.transform.position + new Vector3(0, offY, 0), rot));
            //    //bottom
            //    clones.Add(Instantiate(obj, origin + obj.transform.position + new Vector3(offX, -offY, offZ), rot));
            //    clones.Add(Instantiate(obj, origin + obj.transform.position + new Vector3(offX, -offY, 0), rot));
            //    clones.Add(Instantiate(obj, origin + obj.transform.position + new Vector3(0, -offY, offZ), rot));
            //    clones.Add(Instantiate(obj, origin + obj.transform.position + new Vector3(offX, -offY, -offZ), rot));
            //    clones.Add(Instantiate(obj, origin + obj.transform.position + new Vector3(-offX, -offY, -offZ), rot));
            //    clones.Add(Instantiate(obj, origin + obj.transform.position + new Vector3(0, -offY, -offZ), rot));
            //    clones.Add(Instantiate(obj, origin + obj.transform.position + new Vector3(-offX, -offY, offZ), rot));
            //    clones.Add(Instantiate(obj, origin + obj.transform.position + new Vector3(-offX, -offY, 0), rot));
            //    clones.Add(Instantiate(obj, origin + obj.transform.position + new Vector3(0, -offY, 0), rot));
            //    //right
            //    clones.Add(Instantiate(obj, origin + obj.transform.position + new Vector3(offX, 0, offZ), rot));
            //    clones.Add(Instantiate(obj, origin + obj.transform.position + new Vector3(offX, 0, -offZ), rot));
            //    clones.Add(Instantiate(obj, origin + obj.transform.position + new Vector3(offX, 0, 0), rot));
            //    //left
            //    clones.Add(Instantiate(obj, origin + obj.transform.position + new Vector3(-offX, 0, offZ), rot));
            //    clones.Add(Instantiate(obj, origin + obj.transform.position + new Vector3(-offX, 0, -offZ), rot));
            //    clones.Add(Instantiate(obj, origin + obj.transform.position + new Vector3(-offX, 0, 0), rot));
            //    //front
            //    clones.Add(Instantiate(obj, origin + obj.transform.position + new Vector3(0, 0, offZ), rot));
            //    //back
            //    clones.Add(Instantiate(obj, origin + obj.transform.position + new Vector3(0, 0, -offZ), rot));

            //}
        }
        foreach (GameObject obj in clones)
        {
            obj.transform.parent = transform;
        }
        return clones.ToArray();
    }

    void OnDisable()
    {
        foreach (GameObject obj in CloneWorlds)
        {
            Destroy(obj);
        }
    }

    void OnDrawGizmos() {
        Gizmos.color = DebugColor;
        if (IsWireframe)
            Gizmos.DrawWireCube(transform.position, WorldSize);
        else
            Gizmos.DrawCube(transform.position, WorldSize);
    }

    void OnValidate() {
        if (UniformSize)
        {
            if (LastSize.x != WorldSize.x)
                WorldSize = new Vector3(WorldSize.x, WorldSize.x, WorldSize.x);
            if (LastSize.y != WorldSize.y)
                WorldSize = new Vector3(WorldSize.y, WorldSize.y, WorldSize.y);
            if (LastSize.z != WorldSize.z)
                WorldSize = new Vector3(WorldSize.z, WorldSize.z, WorldSize.z);
        }
        LastSize = WorldSize;
    }
}
