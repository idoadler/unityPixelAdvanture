using UnityEngine;
using System.Collections;

public class MovePlayer : MonoBehaviour {

	public bool pause;
	public MapsData mapData;
	public float stepTime;

	public Sprite up;
	public Sprite down;
	public Sprite right;

	private int stepsLeft;
	private Vector2 dir;
	private bool finishedLevel;
	public CameraPixelFollow cameraFollow;

	private SpriteRenderer spriteRenderer;

	// Use this for initialization
	void Awake () 
	{
		//cameraFollow = GetComponent<CameraPixelFollow>();
		spriteRenderer = GetComponent<SpriteRenderer>();
	}

	public void init (Vector3 position, MapsData mapsData)
	{
		stepsLeft = (int)down.bounds.size.x;
		dir = Vector2.zero;
		finishedLevel = false;
		transform.position = position;
		cameraFollow.init(position);
		mapData = mapsData;
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (!pause)
		{
			if (dir == Vector2.zero)
			{
				if (Input.GetKey(KeyCode.UpArrow))
				{
					dir = Vector2.up;
					spriteRenderer.sprite = up;
					transform.localScale = Vector3.one;
				}
				else if (Input.GetKey(KeyCode.DownArrow))
				{
					dir = -Vector2.up;
					spriteRenderer.sprite = down;
					transform.localScale = Vector3.one;
				}
				else if (Input.GetKey(KeyCode.RightArrow))
				{
					dir = Vector2.right;
					spriteRenderer.sprite = right;
					transform.localScale = Vector3.one;
				}
				else if (Input.GetKey(KeyCode.LeftArrow))
				{
					dir = -Vector2.right;
					spriteRenderer.sprite = right;
					transform.localScale = new Vector3(-1,1,1);
				}

				if (dir != Vector2.zero)
				{
					if (mapData.tile(dir) != mapData.blockChar)
					{
						if (mapData.tile(dir) == mapData.exitChar)
							finishedLevel = true;
						mapData.goTo(dir);
						step();
					}
					else
					{
						dir = Vector2.zero;
					}
				}
			}
		}
	}

	void step()
	{
		if (stepsLeft > 0)
		{
			stepsLeft--;
			transform.position = new Vector2(transform.position.x + dir.x, transform.position.y + dir.y);
			Invoke("step",stepTime);
		}
		else
		{
			stepsLeft = (int)down.bounds.size.x;
			if (finishedLevel)
			{
				SetTiles.nextMap();
			}
			else
			{
				cameraFollow.step(dir, transform.position);
			}
			dir = Vector2.zero;
		}
	}

	private int cameraSteps = 0;
	void cameraStep(Vector2 dir)
	{
		if (cameraSteps > 0)
		{
			cameraSteps--;
			Camera.main.transform.position = new Vector3 (Camera.main.transform.position.x + dir.x, Camera.main.transform.position.y + dir.y, Camera.main.transform.position.z);
			Invoke("cameraStep",stepTime/2);
		}
	}
}
