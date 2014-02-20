using GuildChatPrototype.Base;
using GuildChatPrototype.Factories;
using GuildChatPrototype.GuildStuff;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuildChatPrototype
{
	//This will demo how the classes are created and handled
	public class DemoMain
	{
		//This will demo how the classes are created and handled
		public ChannelManager ChannelMngr;
		public Guild TestGuild;

		public DemoMain()
		{
			this.ChannelMngr = new ChannelManager();
			//Generate a new guild with it's own guild chat.
			this.TestGuild = new Guild(ChannelMngr);
		}
	}
}
