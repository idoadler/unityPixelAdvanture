using UnityEngine;
using System.Collections;

public class MovePlayer : MonoBehaviour {

	public bool pause;
	public MapsData mapData;
	public float stepTime;

	public Sprite up;
	public Sprite down;
	public Sprite right;

	private int size;
	private int stepsLeft = 0;
	private Vector2 dir = Vector2.zero;
	private bool finishedLevel = false;

	private SpriteRenderer spriteRenderer;

	// Use this for initialization
	void Start () 
	{
		spriteRenderer = GetComponent<SpriteRenderer>();
		size = (int)down.bounds.size.x;
		stepsLeft = size;
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
			stepsLeft = size;
			dir = Vector2.zero;
			if (finishedLevel)
			{
				SetTiles.nextMap();
				finishedLevel = false;
			}
		}
	}
}
