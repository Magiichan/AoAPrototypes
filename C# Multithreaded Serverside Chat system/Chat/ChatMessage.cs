using Photon.SocketServer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GuildChatPrototype.Chat
{
	//This class will be the data queue'd up to the ChatThread to be processed
	public class ChatMessage<PeerBaseType> : Message where PeerBaseType : PeerBase
	{
		//Reference to the ChatMessages needed data and a lock object so it can be iterated with thread safety in mind.
		public List<PeerBaseType> Listeners;
		public string Message;
		public ReaderWriterLockSlim ListenerLock;

		public ChatMessage(List<PeerBaseType> listeners, string message, ReaderWriterLockSlim lockObject)
		{
			this.ListenerLock = lockObject;
			this.Listeners = listeners;
			this.Message = message;
		}

		public override void Handle()
		{
			this.ListenerLock.EnterReadLock();
			try
			{
				foreach (PeerBase peer in Listeners)
				{
					//In this demo the message is actually sent back to us.
					//This costs B(1) more bandwidth BUT reduces the amount of operations by n because otherwise we'd need to compare values at some point
					//This is where we'd send the peer and Event about the message.
				}
			}
			finally
			{
				this.ListenerLock.ExitReadLock();
			}
		}
	}
}
