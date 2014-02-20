using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuildChatPrototype.Base
{
	//This enum contains the type of different Channels that can exist
	public enum ChannelType
	{
		GuildChat = 0
	}

	//This interface will be the very base of the channel
	//It will contain no functionality outside of a differeniating value that diserns the type and the unique ID.
	public interface IChannel<ListenerType>
	{
		ChannelType channelType { get; }
		int uniqueChannelID { get; }

		//These interface methods will be used to subscribe and unscribe to the given Channel.
		//The inheirting class will have to implement how that's handle themselves.
		//The reason it's a good idea to let them handle it themselves is suppose it's a guild ChatChannel, it may be a good idea to
		//Send a "has logged in" message to everyone in the channel as opposed to if it was a world zone. You don't need to tell the world you logged in.
		//Nobody wants to pay for that wasted bandwidth.
		bool SubscribeTo(ListenerType chatter);
		bool UnsubscribeFrom(ListenerType chatter);
	}
}
