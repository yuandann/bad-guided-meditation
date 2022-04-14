using UnityEngine;
using System.Collections;

public class SpineShoulderTrack : MonoBehaviour 
{
	private int playerIndex = 0;

	private KinectInterop.JointType joint = KinectInterop.JointType.SpineShoulder;

	public Vector3 jointPosition { get; private set; }

	public BreathingTracker breathingTracker;

	void Start()
	{
		
	}


	void Update() 
	{

		// get the joint position
		KinectManager manager = KinectManager.Instance;

		if(manager && manager.IsInitialized())
		{
			if(manager.IsUserDetected(playerIndex))
			{
				long userId = manager.GetUserIdByIndex(playerIndex);
				breathingTracker.enabled = true;

				if(manager.IsJointTracked(userId, (int)joint))
				{
					// output the joint position for easy tracking
					Vector3 jointPos = manager.GetJointPosition(userId, (int)joint);
					jointPosition = jointPos;
				}
			}
		}

	}

}
