using UnityEngine;
using Mirror;
using System.Collections.Generic;

/*
	Documentation: https://mirror-networking.com/docs/Guides/NetworkBehaviour.html
	API Reference: https://mirror-networking.com/docs/api/Mirror.NetworkBehaviour.html
*/

public class CameraFollow : NetworkBehaviour

{
	// Start is called before the first frame update
	public GameObject CameraToFollow;
	public float distanceBack = 6;

	
	private void LateUpdate()
	{
		RpcDestroyCam();
		CmdDestroyCam();
		
	}
	[ClientRpc]
	private void RpcDestroyCam()
	{
		if (!base.hasAuthority)

			//destory cam if not local player!
			Object.Destroy(CameraToFollow);

		return;
	}
	[Command]
	private void CmdDestroyCam()
	{
		if (!base.hasAuthority)

			//destory cam if not local player!
			Object.Destroy(CameraToFollow);

		return;
	}
}