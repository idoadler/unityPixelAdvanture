using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SetTiles : MonoBehaviour {
	static private SetTiles instance;

	public string mapFiles;
	//public Vector2 maxSize; TODO:IMPLEMENT
	public GameObject Empty;
	public GameObject Block;
	public GameObject Exit;
	public GameObject Player;

	//private Vector2 blockSize; TODO:IMPLEMENT
	//private GameObject[][] blocks; TODO:IMPLEMENT
	private MapsData mapsData;
	private GameObject mainPlayer;

	// Use this for initialization
	void Start () {
		instance = this;
		mapsData = ReadTiles.LoadMap(mapFiles);
		mapsData.loadMap(0);
		mainPlayer = Instantiate(Player) as GameObject;
		Camera.main.transform.position = new Vector3 (mainPlayer.transform.position.x, mainPlayer.transform.position.y, Camera.main.transform.position.z);
		Camera.main.transform.parent = mainPlayer.transform;
		DrawMap();
	}

	private void DrawMap()
	{
		Vector3 size = Empty.GetComponent<SpriteRenderer>().bounds.size;

		for (int x = 0; x < mapsData.width; x++)
		{
			for (int y = 0; y < mapsData.height; y++)
			{
				GameObject prefab;
				if (mapsData.mapData.map[x][y] == mapsData.emptyChar)
				{
					prefab = Empty;
				}
				else if (mapsData.mapData.map[x][y] == mapsData.exitChar)
				{
					prefab = Exit;
				}
				else
				{
					prefab = Block;
				}
				GameObject instance = Instantiate(prefab, new Vector3(size.x*x,-size.y*y,0) ,Quaternion.identity) as GameObject;
				instance.transform.parent = transform;
			}
		}

		mainPlayer.transform.position = new Vector3(size.x*mapsData.playerPos.x,-size.y*mapsData.playerPos.y,0);
		mainPlayer.GetComponent<MovePlayer>().mapData = mapsData;
	}

	static public void nextMap()
	{
		instance.mapsData.loadMap((instance.mapsData.currentMap+1)%instance.mapsData.maps.Count);

		var children = new List<GameObject>();
		foreach (Transform child in instance.transform) children.Add(child.gameObject);
		children.ForEach(child => Destroy(child));

		instance.DrawMap();
	}
}
