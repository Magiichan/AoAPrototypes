using GuildChatPrototype.Base;
using GuildChatPrototype.Bases;
using GuildChatPrototype.Chat;
using GuildChatPrototype.Factories;
using GuildChatPrototype.PlayerStuff;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GuildChatPrototype.GuildStuff
{
	public class Guild : IChat<Player>
	{
		public List<Player> GuildPlayers;

		private ReaderWriterLockSlim GuildPlayersLockObject;

		private ChatChannel<Player> _Chat;
		public ChatChannel<Player> Chat
		{
			get { return _Chat; }
			private set { this._Chat = value; }
		}

		//This is a demo
		public Guild(ChannelManager ChannelMngr)
		{
			this.Chat = ChannelMngr.generateChatChannel(ChannelType.GuildChat, this.GuildPlayers, GuildPlayersLockObject);
			this.GuildPlayers = new List<Player>(10);
		}

		//Down here you'd add some methods that add and remove players from the list
		//THIS IS A DEMO THOUGH

	}
}
