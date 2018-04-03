using System.Linq;
using System.Threading.Tasks;
using Discord.Commands;
using Discord;

namespace DPP_Bot.Modules
{
    public class Testes : ModuleBase<SocketCommandContext>
    {
        [Command("Debug Repetir")]
        [RequireBotPermission(ChannelPermission.SendMessages)]
        public async Task Repetir([Remainder] string msg = null)
        {

            if (msg == null)
            {
                await Context.Channel.SendMessageAsync($"{Context.User.Mention}: Você precisa me dizer algo para repetir");
                return;
            }

            var embed = new EmbedBuilder();
            embed.WithTitle("Debug Repetir");
            embed.WithDescription($"```{msg}```");
            embed.WithColor(new Color(74, 144, 226));

            await Context.Channel.SendMessageAsync("", false, embed);
        }
    }
}
