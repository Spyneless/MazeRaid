using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileScript : MonoBehaviour {

    public int width;
    public Vector2 coord;
    public Vector2 MazeDimensions;
    public GameObject wall;
    public int[] connectionValues = new int[4];
    public TileScript[] connections = new TileScript[4];
    public bool Discovered {get; set;}

    public Dictionary<TileScript, int> whichNeighbour;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void SetConnections()
    {
        Discovered = false;
        for(int i = 0; i < 4; i++)
        {
            connectionValues[i] = Random.Range(0, 1000000);
        }

        if (coord.y == MazeDimensions.y - 1)
            connectionValues[0] = -1;
        if (coord.x == MazeDimensions.x -1)
            connectionValues[1] = -1;
        if (coord.y == 0)
            connectionValues[2] = -1;
        if (coord.x == 0)
            connectionValues[3] = -1;
    }

    public void SetWalls()
    {
        if (connectionValues[0] == -1)
            AddWall(0, 0.5f, 90f);
        if (connectionValues[1] == -1)
            AddWall(0.5f, 0, 0);
        if (connectionValues[2] == -1)
            AddWall(0, -0.5f, 90f);
        if (connectionValues[3] == -1)
            AddWall(-0.5f, 0, 0);
    }

    public void AddWall(float x, float z, float rot)
    {
        GameObject w = GameObject.Instantiate(wall);
        w.transform.parent = transform;
        w.transform.position = new Vector3(transform.position.x + x, 0.5f, transform.position.z + z);
        w.transform.Rotate(new Vector3(0, 1, 0), rot);
    }

    public void SetTileScriptLinks(TileScript[][] grid)
    {
        whichNeighbour = new Dictionary<TileScript, int>();

        //north
        if (coord.y != MazeDimensions.y - 1)
        {
            connections[0] = grid[(int)coord.x][(int)coord.y + 1];
            whichNeighbour.Add(connections[0], 0);
        }
        else
            connections[0] = null;

        //east
        if (coord.x != MazeDimensions.x - 1)
        {
            connections[1] = grid[(int)coord.x + 1][(int)coord.y];
            whichNeighbour.Add(connections[1], 1);
        }
        else
            connections[1] = null;

        //south
        if (coord.y != 0)
        { 
            connections[2] = grid[(int)coord.x][(int)coord.y - 1];
            whichNeighbour.Add(connections[2], 2);
        }
        else
            connections[2] = null;

        //west
        if (coord.x != 0)
        {
            connections[3] = grid[(int)coord.x - 1][(int)coord.y];
            whichNeighbour.Add(connections[3], 3);
        }
        else
            connections[3] = null;

    }

    public void BlockPath(TileScript nb)
    {
        connectionValues[whichNeighbour[nb]] = -1;
    }
}
