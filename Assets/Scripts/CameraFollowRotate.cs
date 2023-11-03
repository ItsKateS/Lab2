using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowRotate : MonoBehaviour
{
	[SerializeField]
	private Transform target;

	private Vector3 offsetPosition = new Vector3(0, 3, -7);

	private Space offsetPositionSpace = Space.Self;

	private bool lookAt = true;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void LateUpdate()
	{
		Refresh();
	}

	public void Refresh()
	{
		if (target == null)
		{
			Debug.LogWarning("Missing target ref !", this);

			return;
		}

		if (offsetPositionSpace == Space.Self)
		{
			transform.position = target.TransformPoint(offsetPosition);
		}
		else
		{
			transform.position = target.position + offsetPosition;
		}

		if (lookAt)
		{
			transform.LookAt(target);
		}
		else
		{
			transform.rotation = target.rotation;
		}
	}
}
