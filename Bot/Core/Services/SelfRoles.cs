using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using Discord.Rest;
using Discord.WebSocket;

namespace DPP_Bot.Core.Services
{
    public class SelfRoles : ModuleBase<SocketCommandContext>
    {
        internal string IdDoEmbed;
        internal string Arquivo = "Resources/SelfRolesMsgID.txt";

        [Command("Criar registro", RunMode = RunMode.Async)]
        [RequireUserPermission(GuildPermission.Administrator)]
        public async Task CriarRegistro()
        {
            const int delay = 2000;
            //var criando = await Context.Channel.SendMessageAsync("Criando... Por favor aguarde");
            var embed = new EmbedBuilder();
            embed.WithColor(new Color(74, 144, 226));
            embed.WithTitle("Registrando-se");
            embed.WithDescription("Olá, sou o bot do **<Nome Do Servidor>** e irei ajudar-lhe a ganhar os seus cargos no servidor.\n" +
                                  "Para isso será necessário que você use as reações a baixo, conforme a legenda ao lado do emote.");

            embed.AddField((efb) =>
            {
                efb.Name = "Jogos:";
                efb.IsInline = true;
                efb.Value = "<:warframe:414175161630326785> - **WarFrame**\n" +
                            "<:paladins:414175183264284672> - **Paladins**\n" +
                            "<:knivesout:414175189912518665> - **Knives Out**\n" +
                            "<:Fortnite:414175147210309644> - **Fortnite**\n" +
                            "<:pubg:414175164847095819> - **PUBG**\n" +
                            "<:lol:414175181632962591> - **League**\n" +
                            "<:gta:414175160342413335> - **GTA**\n" +
                            "<:cs:414175147646255105> - **CS**\n" +
                            "<:ow:414215183297282048> - **Overwatch**\n";
            });
            RestUserMessage msg = await Context.Channel.SendMessageAsync("", embed: embed);
            await msg.AddReactionAsync(Emote.Parse("<:warframe:414175161630326785>"));
            await Task.Delay(delay);
            await msg.AddReactionAsync(Emote.Parse("<:paladins:414175183264284672>"));
            await Task.Delay(delay);
            await msg.AddReactionAsync(Emote.Parse("<:knivesout:414175189912518665>"));
            await Task.Delay(delay);
            await msg.AddReactionAsync(Emote.Parse("<:Fortnite:414175147210309644>"));
            await Task.Delay(delay);
            await msg.AddReactionAsync(Emote.Parse("<:pubg:414175164847095819>"));
            await Task.Delay(delay);
            await msg.AddReactionAsync(Emote.Parse("<:lol:414175181632962591>"));
            await Task.Delay(delay);
            await msg.AddReactionAsync(Emote.Parse("<:gta:414175160342413335>"));
            await Task.Delay(delay);
            await msg.AddReactionAsync(Emote.Parse("<:cs:414175147646255105>"));
            await Task.Delay(delay);
            await msg.AddReactionAsync(Emote.Parse("<:ow:414215183297282048>"));

            var msgid = await Context.Channel.SendMessageAsync(msg.Id.ToString());

            if (File.Exists(Arquivo))
            {
                File.Delete(Arquivo);
            }

            if (!File.Exists(Arquivo))
            {
                using (StreamWriter sw = File.CreateText(Arquivo))
                {
                    sw.WriteLine(msg.Id.ToString());
                }
            }

            await Context.Message.DeleteAsync();
            await Task.Delay(5000);
            await msgid.DeleteAsync();


        }
        internal async Task AoRemoverReact(Cacheable<IUserMessage, ulong> msg, ISocketMessageChannel channel, SocketReaction reaction)
        {
            if (reaction.User.Value.IsBot) return;
            if (reaction.MessageId.ToString() != IdDoEmbed)
            {
                GetId();
            }

            if (reaction.MessageId.ToString() != IdDoEmbed) return;

            switch (reaction.Emote.Name)
            {
                case "warframe":
                    await AddOrDellRole(reaction.User, channel, "", "Warframe");
                    break;
                case "paladins":
                    await AddOrDellRole(reaction.User, channel, "", "Paladins");
                    break;
                case "knivesout":
                    await AddOrDellRole(reaction.User, channel, "", "Knives Out");
                    break;
                case "Fortnite":
                    await AddOrDellRole(reaction.User, channel, "", "Fortnite");
                    break;
                case "pubg":
                    await AddOrDellRole(reaction.User, channel, "", "PUBG");
                    break;
                case "lol":
                    await AddOrDellRole(reaction.User, channel, "", "League Of Legends");
                    break;
                case "gta":
                    await AddOrDellRole(reaction.User, channel, "" , "Gta V");
                    break;
                case "cs":
                    await AddOrDellRole(reaction.User, channel, "", "CS:GO");
                    break;
                case "ow":
                    await AddOrDellRole(reaction.User, channel, "", "Overwatch");
                    break;
            }
        }

        internal async Task AoAddReact(Cacheable<IUserMessage, ulong> msg, ISocketMessageChannel channel, SocketReaction reaction)
        {

            if (reaction.User.Value.IsBot) return;

            if (reaction.MessageId.ToString() != IdDoEmbed)
            {
                GetId();
            }

            if (reaction.MessageId.ToString() != IdDoEmbed) return;

            switch (reaction.Emote.Name)
            {
                case "warframe":
                    await AddOrDellRole(reaction.User, channel, "Warframe");
                    break;
                case "paladins":
                    await AddOrDellRole(reaction.User, channel, "Paladins");
                    break;
                case "knivesout":
                    await AddOrDellRole(reaction.User, channel, "Knives Out");
                    break;
                case "Fortnite":
                    await AddOrDellRole(reaction.User, channel, "Fortnite");
                    break;
                case "pubg":
                    await AddOrDellRole(reaction.User, channel, "PUBG");
                    break;
                case "lol":
                    await AddOrDellRole(reaction.User, channel, "League Of Legends");
                    break;
                case "gta":
                    await AddOrDellRole(reaction.User, channel, "Gta V");
                    break;
                case "cs":
                    await AddOrDellRole(reaction.User, channel, "CS:GO");
                    break;
                case "ow":
                    await AddOrDellRole(reaction.User, channel, "Overwatch");
                    break;
            }
        }

        //internal async Task

        public async Task AddOrDellRole(Optional<IUser> user, ISocketMessageChannel channel, string addrole = null, string dellrole = null)
        {
            //string addmsg = "";
            //string dellmsg = "";

            if (!string.IsNullOrEmpty(addrole))
            {
                var varaddrole = ((SocketGuildUser)user).Guild.Roles.Where(has => has.Name.ToUpper() == addrole.ToUpper());
                await ((SocketGuildUser)user).AddRolesAsync(varaddrole);
                //addmsg = $" <:Mais:414206390773743667>  `{addrole}`";
            }

            if (!string.IsNullOrEmpty(dellrole))
            {
                var vardellrole = ((SocketGuildUser)user).Guild.Roles.Where(has => has.Name.ToUpper() == dellrole.ToUpper());
                await ((SocketGuildUser)user).RemoveRolesAsync(vardellrole);
                //dellmsg = $" <:Menos:414206390970744833>  `{dellrole}`";
            }

            //var m = await channel.SendMessageAsync($"{user.Value.Mention}:{addmsg}{dellmsg}");
            //await Task.Delay(1500);
            //await m.DeleteAsync();

            // Isso tudo que está comentado, é em relão a uma notificação que ele dava, ao adicionar o cargo para a pessoa
            // porém, como era muita gente, eu comentei o código, pra ele não rodar...
        }

        private void GetId()
        {
            using (StreamReader sr = File.OpenText(Arquivo))
            {
                // Isso é para percorrer o arquivo de texto que tem a ID da menssagem do sistema de registro por emoji...
                IdDoEmbed = null;
                string s;
                while ((s = sr.ReadLine()) != null)
                {
                    //Console.WriteLine($"Carregado ID Registro: {s}");
                    IdDoEmbed = s;
                }
            }
        }
    }
}
