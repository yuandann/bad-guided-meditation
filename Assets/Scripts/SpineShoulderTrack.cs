using UnityEngine;
using System.Collections;
using FMODUnity;

public class SpineShoulderTrack : MonoBehaviour 
{
	private int playerIndex = 0;

	private KinectInterop.JointType joint = KinectInterop.JointType.SpineShoulder;

	public Vector3 jointPosition { get; private set; }

	public BreathingTracker breathingTracker;

	public StudioEventEmitter introEmitter;

	void Update() 
	{
		KinectManager manager = KinectManager.Instance;

		var fmodSystem = RuntimeManager.StudioSystem;
		fmodSystem.getParameterByName("isTracking", out float f, out float isTrackingValue);

		if (manager && manager.IsInitialized())
		{
			if(manager.IsUserDetected(playerIndex))
			{
				long userId = manager.GetUserIdByIndex(playerIndex);
				introEmitter.Play();
				if (isTrackingValue == 1)
					breathingTracker.enabled = true;

				if (manager.IsJointTracked(userId, (int)joint))
				{
					// output the joint position for easy tracking
					Vector3 jointPos = manager.GetJointPosition(userId, (int)joint);
					jointPosition = jointPos;
				}
			}
		}

		

	
	}

}
