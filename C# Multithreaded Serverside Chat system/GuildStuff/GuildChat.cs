using GuildChatPrototype.Base;
using GuildChatPrototype.Factories;
using GuildChatPrototype.PlayerStuff;
using Photon.SocketServer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GuildChatPrototype.Chat
{
	public class GuildChat : ChatChannel<Player>
	{
		private ReaderWriterLockSlim ListenersLock;
		private ChannelManager MessageHandler;

		public GuildChat(List<Player> listeners, ChannelType chanType, int uID, ReaderWriterLockSlim lockObject, ChannelManager channelFactoryReference)
			: base(listeners, chanType, uID)
		{
			ListenersLock = lockObject;
			this.MessageHandler = channelFactoryReference;

			//Handles some stuff in base too
		}

		//Contains methods related to ChatChannel implementation
		#region ChatChannel inherited methods

		#region Message Handling
		public override void ForwardChatMessage(Player sentFrom, string message, List<Player> to)
		{
			//Configure the final message
			message = sentFrom.playerName + ": " + message;
			//Queue the message item in the Handler (the handler handles the threaded implementation; don't worry.
			this.MessageHandler.QueueMessage(new ChatMessage<Player>(this.Listeners, message, this.ListenersLock));
		}
		#endregion

		public override bool SubscribeTo(Player chatter)
		{
			//Very important that this data is locked when adding or removing as a seperate thread will be handling chat
			this.ListenersLock.EnterWriteLock();
			try
			{
				this.Listeners.Add(chatter);
			}
			finally
			{
				this.ListenersLock.ExitWriteLock();
			}

			//The bool return seems pointless now but it may come in handy if we ever want to limit the amount in a
			//given chat channel
			return true;
		}

		public override bool UnsubscribeFrom(Player chatter)
		{
			//Very important that this data is locked when adding or removing as a seperate thread will be handling chat
			this.ListenersLock.EnterWriteLock();
			try
			{
				//Returns if a player was removed from the list
				return this.Listeners.Remove(chatter);
			}
			finally
			{
				this.ListenersLock.ExitWriteLock();
			}
		}
		#endregion
	}
}
