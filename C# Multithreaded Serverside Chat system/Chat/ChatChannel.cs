using GuildChatPrototype.Base;
using Photon.SocketServer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuildChatPrototype.Chat
{
	//IChatChannel provides all the functionality of a channel as well as a provides methods that can be used to
	//subscribe and unsubscribe from a given channel and MessageForwarding.
	public abstract class ChatChannel<ListenerType> : IChannel<ListenerType>
	{
		//Reference to the chatters listening to the channel
		protected List<ListenerType> Listeners;

		public ChatChannel(List<ListenerType> listeners, ChannelType chanType, int uID)
		{
			//set a reference to the list of listeners.
			this.Listeners = listeners;

			//Set the properties
			this._uniqueChannelID = uID;
			this._channelType = chanType;
		}

		//Abstract implementable method for forwarding a chat method
		public abstract void ForwardChatMessage(ListenerType sentFrom, string message, List<ListenerType> to);

		protected ChannelType _channelType;
		public ChannelType channelType
		{
			get { return this._channelType; }
			//keep these private and let them be set in this baseclass.
			private set { this._channelType = value; }
		}

		protected int _uniqueChannelID;
		public int uniqueChannelID
		{
			get { return _uniqueChannelID; }
			//keep these private and let them be set in this baseclass.
			private set { this._uniqueChannelID = value; }
		}

		public abstract bool SubscribeTo(ListenerType chatter);

		public abstract bool UnsubscribeFrom(ListenerType chatter);
	}
}
