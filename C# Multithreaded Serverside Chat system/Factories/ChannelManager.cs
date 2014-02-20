using GuildChatPrototype.Base;
using GuildChatPrototype.Chat;
using GuildChatPrototype.PlayerStuff;
using GuildChatPrototype.Threading;
using Photon.SocketServer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Channels;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GuildChatPrototype.Factories
{
	public class ChannelManager
	{
		int uniqueGenerator;
		private Thread ChannelHandlingThread;
		private ChatThread chatThread;

		public ChannelManager()
		{
			//Start the unique value
			uniqueGenerator = 0;
			chatThread = new ChatThread();
			this.ChannelHandlingThread = new Thread(new ThreadStart(chatThread.ThreadedChatMessageHandler));
			this.ChannelHandlingThread.Start();
		}

		//Many channels may need to have overloaded methods of this. It was a design oversight that prevented me from making this generic.
		public ChatChannel<Player> generateChatChannel(ChannelType channelType, List<Player> listeners, ReaderWriterLockSlim specificListenersLock)
		{
			switch (channelType)
			{
				case ChannelType.GuildChat:
					//Return a new GuildChat with a new uniqueID and also increment the uniqueID value
					return new GuildChat(listeners, channelType, uniqueGenerator++, specificListenersLock, this);
				default:
					return null;
			}
		}

		public void QueueMessage(ChatMessage<Player> message)
		{
			this.chatThread.QueueMessageItem(message);
		}
	}
}
