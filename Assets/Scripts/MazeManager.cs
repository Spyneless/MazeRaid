using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeManager : MonoBehaviour {

    public int Width;
    public int Height;
    public int SegmentHeight;
    public int SegmentWidth;

    public GameObject tile;

    private SegmentManager[][] grid;// = new TileScript[width][];

	// Use this for initialization
	void Start () {
        //for x*y 
        //generate segment
        //set parameters

        //for x*y
        //set links for segments

        //generate mazepattern between segments
            //--number predecessr and distance to origin
            //--keep track of connections
        //(optional) add few more passages

        //for x*y
        //call setup of the segments

        //for x*y
        //break through boundaries between segments
    }

	// Update is called once per frame
	void Update () {
		
	}
}
