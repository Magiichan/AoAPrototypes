using Photon.SocketServer;
using PhotonHostRuntimeInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuildChatPrototype.PlayerStuff
{
	public class Player : PeerBase
	{
		//Demo value
		public string playerName;

		public Player(IRpcProtocol protocol, IPhotonPeer unmanagedPeer)
			: base(protocol, unmanagedPeer)
		{
			//Nothing, this is demo code.
		}

		//Not implementing this because it's demo code
		protected override void OnDisconnect(PhotonHostRuntimeInterfaces.DisconnectReason reasonCode, string reasonDetail)
		{
			throw new NotImplementedException();
		}

		protected override void OnOperationRequest(OperationRequest operationRequest, SendParameters sendParameters)
		{
			throw new NotImplementedException();
		}
	}
}
