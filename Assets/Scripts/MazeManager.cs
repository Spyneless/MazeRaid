using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeManager : MonoBehaviour {

    public int width;
    public int height;
    public GameObject tile;

    private TileScript[][] grid;// = new TileScript[width][];

	// Use this for initialization
	void Start () {
        grid = new TileScript[width][];
        for (int x = 0; x < width; x++)
        {
            grid[x] = new TileScript[height];
            for (int z = 0; z < height; z++)
            {
                GameObject t = GameObject.Instantiate(tile);
                t.transform.parent = transform;
                t.name = "tile (" + x + "-" + z + ")";
                t.transform.position = new Vector3(x * 1.1f, 0, z * 1.1f);

                TileScript ts = t.GetComponent<TileScript>();
                grid[x][z] = ts;
                ts.coord = new Vector2(x, z);
                ts.MazeDimensions = new Vector2(width, height);
                ts.SetConnections();
            }
        }
        for (int x = 0; x < width; x++)
            for (int z = 0; z < height; z++)
                grid[x][z].SetTileScriptLinks(grid);

        GenerateMazePattern();

        for (int x = 0; x < width; x++)
            for (int z = 0; z < height; z++)
                grid[x][z].SetWalls();
}

    private void GenerateMazePattern()
    {
        SortedList<int, TileScript> mazePathsTo = new SortedList<int, TileScript>();
        SortedList<int, TileScript> mazePathsFrom = new SortedList<int, TileScript>();

        TileScript source = grid[0][0];

        //add north and east connection
        mazePathsFrom.Add(source.connectionValues[0], source);
        mazePathsFrom.Add(source.connectionValues[1], source);
        mazePathsTo.Add(source.connectionValues[0], source.connections[0]);
        mazePathsTo.Add(source.connectionValues[1], source.connections[1]);

        while(mazePathsTo.Count != 0)
        {
            
            TileScript from = mazePathsFrom.Values[0];
            TileScript to = mazePathsTo.Values[0];
            mazePathsFrom.RemoveAt(0);
            mazePathsTo.RemoveAt(0);

            to.Discovered = true;

            for(int i = 0; i < 4; i++)
            {
                if (to.connectionValues[i] == -1)
                    break;
                TileScript nb = to.connections[i];
                if (!nb.Discovered )
                {
                    //add
                    //WHYis it double adding
                    int newpathvalue = to.connectionValues[i];
                    if (!mazePathsTo.ContainsKey(newpathvalue))
                    {
                        mazePathsTo.Add(newpathvalue, nb);
                        mazePathsFrom.Add(newpathvalue, to);
                    }
                }
                else if(nb.connections[i] == from)
                {
                    //don do anything
                }
                else
                {
                    nb.BlockPath(to);
                    //blockpath
                    to.BlockPath(nb);
                }
            }
        }


    }

	// Update is called once per frame
	void Update () {
		
	}
}
