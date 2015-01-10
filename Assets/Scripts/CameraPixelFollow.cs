using UnityEngine;
using System.Collections;

public class CameraPixelFollow : MonoBehaviour {

	// Move Camera by Pixels
	// Move Camera if player moved 7 up or 9 left
	// BEWARE - left is not fixed, maybe need to scale image?

	public int maxUp = 7;
	public int maxRight = 9;
	public float transitionSpeed = 0.5f;

	private int stepsRight = 0;
	private int stepsUp = 0;

	private int cameraSteps;

	private bool isInTransition = false;
	private Vector3 targetPosition;
	private Vector3 targetDir;
	void Update()
	{
		if (isInTransition)
		{
			if(Vector3.Distance(Camera.main.transform.position,targetPosition) >= 1)
			{
				Camera.main.transform.position += targetDir * transitionSpeed * Time.deltaTime;
			}
			else
			{
				Camera.main.transform.position = targetPosition;
				isInTransition = false;
			}
		}
	}

	public void init (Vector3 position)
	{
		targetPosition = new Vector3 (position.x, position.y, Camera.main.transform.position.z);
		Camera.main.transform.position = targetPosition;
		stepsRight = 0;
		stepsUp = 0;
	}

	public void step(Vector2 dir, Vector3 position)
	{
		stepsRight += (int)dir.x;
		stepsUp += (int)dir.y;
		print (stepsRight + "," + stepsUp);

		if(Mathf.Abs(stepsRight) >= maxRight)
		{
			stepsRight = 0;
			targetPosition = new Vector3 (position.x, targetPosition.y, targetPosition.z);
			isInTransition = true;
			targetDir = (targetPosition-Camera.main.transform.position).normalized;
		}
		
		if(Mathf.Abs(stepsUp) >= maxUp)
		{
			stepsUp = 0;
			targetPosition = new Vector3 (targetPosition.x, position.y, targetPosition.z);
			isInTransition = true;
			targetDir = (targetPosition-Camera.main.transform.position).normalized;
		}
	}

}
