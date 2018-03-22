using System;
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
            {
                for (int i = 0; i < 4; i++)
                    Debug.Log(grid[x][z].name + " connection " + i + " = " + grid[x][z].connectionValues[i]);
            }

        for (int x = 0; x < width; x++)
            for (int z = 0; z < height; z++)
                grid[x][z].SetWalls();
}

    private void GenerateMazePattern()
    {
        SortedDictionary<int, TileScript> mazePathsTo = new SortedDictionary<int, TileScript>();
        SortedDictionary<int, TileScript> mazePathsFrom = new SortedDictionary<int, TileScript>();

        TileScript source = grid[0][0];
        source.Discovered = true;

        //add north and east connection
        mazePathsFrom.Add(source.connectionValues[0], source);
        mazePathsFrom.Add(source.connectionValues[1], source);
        mazePathsTo.Add(source.connectionValues[0], source.connections[0]);
        mazePathsTo.Add(source.connectionValues[1], source.connections[1]);

        int it = 0;

        while(mazePathsTo.Count != 0)
        {
            foreach(KeyValuePair<int, TileScript> kvp in mazePathsFrom)
            {
                Debug.Log("iteration " + it + "   key-" + kvp.Key + " " + kvp.Value.name + " -> " + mazePathsTo[kvp.Key] );
                it++;
            }
            Debug.Log("---");

            TileScript from = null;
            TileScript to = null;
            int index = 0;

            foreach (KeyValuePair<int, TileScript> kvp in mazePathsFrom)
            {
                from = kvp.Value;
                index = kvp.Key;
                break;
            }
            foreach (KeyValuePair<int, TileScript> kvp in mazePathsTo)
            {
                to = kvp.Value;
                break;
            }

            Debug.Log("OOOOO key-" + index + " " + from.name.ToString() + " -> " + to.name.ToString());
            

            //from = mazePathsFrom[0];
            //TileScript from = mazePathsFrom.Keys[0];
            //to = mazePathsTo.Values[0];
            mazePathsFrom.Remove(index);
            mazePathsTo.Remove(index);

            to.Discovered = true;

            for(int i = 0; i < 4; i++)
            {
                if (to.connectionValues[i] == -1)
                { }
                else
                {
                    TileScript nb = to.connections[i];
                    int toIndex = 0;

                    //get index of "To"'s connection in the neighbour
                    if (nb != null)
                        toIndex = nb.RequestNBNumber(to);

                    if (!nb.Discovered)
                    {
                        //add
                        int newpathvalue = to.connectionValues[i];
                        if (!mazePathsTo.ContainsKey(newpathvalue))
                        {
                            mazePathsTo.Add(newpathvalue, nb);
                            mazePathsFrom.Add(newpathvalue, to);
                        }
                        to.AddValidConnection(nb);
                        nb.AddValidConnection(to);
                        Debug.Log("discovered-" + to + "    new node-" + nb.name);
                    }
                    else if (nb.connections[i] != null && nb.IsConnectedTo(nb.connections[i]))
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
            Debug.Log("//---//");
        }


    }

	// Update is called once per frame
	void Update () {
		
	}
}
