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
        int id = 0;
        grid = new SegmentManager[Width][];
        for (int x = 0; x < Width; x++)
        {
            grid[x] = new SegmentManager[Height];
            for (int z = 0; z < Height; z++)
            {
                GameObject t = GameObject.Instantiate(tile);
                t.transform.parent = transform;
                t.name = "segment (" + x + "-" + z + ")";
                t.transform.position = new Vector3(x * SegmentWidth * 1.1f, 0, z * SegmentHeight * 1.1f);

                SegmentManager segM = t.GetComponent<SegmentManager>();
                grid[x][z] = segM;
                segM.coord = new Vector2(x, z);
                segM.MazeDimensions = new Vector2(Width, Height);
                segM.width = SegmentWidth;
                segM.height = SegmentHeight;
                segM.SetConnections();

                segM.Origin = -1;
                segM.ID = id;
                id++;
            }
        }

        //for x*y
        //set links for segments
        for (int x = 0; x < Width; x++)
            for (int z = 0; z < Height; z++)
                grid[x][z].SetTileScriptLinks(grid);

        //generate mazepattern between segments
        //--number predecessr and distance to origin
        //--keep track of connections
        //(optional) add few more passages
        GenerateMazePattern();

        //for x*y
        //call setup of the segments

        //for x*y
        //break through boundaries between segments
    }

	// Update is called once per frame
	void Update () {
		
	}
}
