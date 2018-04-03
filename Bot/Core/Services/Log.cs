using System;
using Discord.Commands;
using Discord.WebSocket;
using DPP_Bot.Core.Configs;

namespace DPP_Bot.Core.Services
{
    class Log
    {
        private static SocketTextChannel _channel;
        internal static void CmdLog(SocketCommandContext context, SocketMessage s)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write($"{DateTime.Now:HH:mm:ss tt} [ Comando] {context.Guild.Name}/{context.Channel.Name}/{context.User.Username}: ");

            Console.ForegroundColor = ConsoleColor.White;
            Console.Write(s + "\n");

            _msglog(context,s);
        }

        private static async void _msglog(SocketCommandContext context, SocketMessage s)
        {
            _channel = Global.Client.GetGuild(Config.Bot.IdServer).GetTextChannel(Config.Bot.IdChatLog);

            String comando = s.ToString().Remove(0, Config.Bot.CmdPrefix.Length).ToLower();

            if (comando.StartsWith("ban") || comando.StartsWith("mute") || comando.StartsWith("desmutar") || comando.StartsWith("kick") || comando.StartsWith("tirar warns") || comando.StartsWith("warn") && !comando.StartsWith("warns"))
            {
                return;
            }

            var builder = EmbedHandler.CmdLogEmbed(context, s);
            await _channel.SendMessageAsync("", false, builder);
        }
    }
}
