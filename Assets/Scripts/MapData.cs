using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MapsData {
	public string fileName;
	public char emptyChar;
	public char blockChar;
	public char exitChar;
	public char playerChar;
	public int currentMap;
	public Vector2 playerPos;
	public List<MapData> maps = new List<MapData>();
	
	public MapData mapData
	{
		get
		{
			return maps[currentMap];
		}
	}
	
	public int width
	{
		get
		{
			return mapData.map.Count;
		}
	}	

	public int height
	{
		get
		{
			return mapData.map[0].Count;
		}
	}

	public char tile (Vector2 dir)
	{
		Vector2 target = new Vector2(playerPos.x + dir.x, playerPos.y - dir.y);
		return mapData.map[(int)target.x][(int)target.y];
	}

	public void goTo (Vector2 dir)
	{
		playerPos = new Vector2(playerPos.x + dir.x, playerPos.y - dir.y);
	}

	public void loadMap(int map)
	{
		currentMap = map;
		playerPos = maps[map].startPos;
	}
}


public class MapData {
	public Vector2 startPos;
	public List<List<char>> map = new List<List<char>>();
}