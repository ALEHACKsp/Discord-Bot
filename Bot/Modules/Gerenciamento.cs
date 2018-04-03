using System;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using Discord.WebSocket;
using DPP_Bot.Core.Configs;
using DPP_Bot.Core.DB;
using DPP_Bot.Core.Services;
using DPP_Bot.Core.UserAccounts;

namespace DPP_Bot.Modules
{
    public class Gerenciamento : ModuleBase<SocketCommandContext>
    {
        private readonly DbMisc _dbMisc = new DbMisc();

        //  Limpar
        [Command("Limpar", RunMode = RunMode.Async)]
        [Summary("Deleta uma quantidade específica de mensagens.")]
        [RequireUserPermission(GuildPermission.ManageMessages)]
        [RequireBotPermission(ChannelPermission.ManageMessages)]
        internal async Task LimparChat(uint amount)
        {
            if (amount > 0)
            {
                var messages = await Context.Channel.GetMessagesAsync((int)amount + 1).Flatten();

                await Context.Channel.DeleteMessagesAsync(messages);
                const int delay = 5000;
                var m = await ReplyAsync(
                    $"Limpeza concluída. _Esta mensagem será eliminada em {delay / 1000} segundos._");
                await Task.Delay(delay);
                await m.DeleteAsync();
            }
            else
            {
                await Context.Channel.SendMessageAsync(
                    "Você precisa mensurar um número maior que _0_, para serem deletadas");
            }
        }

        //  Mute
        [Command("Mute", RunMode = RunMode.Async)]
        [RequireBotPermission(GuildPermission.MuteMembers)]
        [RequireUserPermission(GuildPermission.MuteMembers)]
        internal async Task Muteuser(IGuildUser user = null)
        {
            if (user == null)
            {
                await Context.Channel.SendMessageAsync($"{Context.User.Mention} você precisa mencionar quem levará o mute.");
                return;
            }
            if (user.GuildPermissions.ManageChannels || user.GuildPermissions.ManageRoles)
            {
                await Context.Channel.SendMessageAsync($"{Context.User.Mention} você não pode mutar um membro da Staff.");
                return;
            }

            var userAccount = UserAccounts.GetAccount((SocketUser)user);
            userAccount.IsMuted = true;
            UserAccounts.SaveAccounts();

            var m = await Context.Channel.SendMessageAsync(
                $"Usuário {user.Mention} foi mutado por {Context.User.Mention}");
            await Task.Delay(2000);
            await m.DeleteAsync();

            //  Cria e envia o Log de mute
            var logEmbed = EmbedHandler.LogEmbed("Mute", Context.User, user);
            await Global.Client.GetGuild(Config.Bot.IdServer).GetTextChannel(Config.Bot.IdChatLog).SendMessageAsync("", false, logEmbed);

        }

        //  UnMute
        [Command("Desmutar", RunMode = RunMode.Async)]
        [RequireBotPermission(GuildPermission.MuteMembers)]
        [RequireUserPermission(GuildPermission.MuteMembers)]
        internal async Task DesMuteuser(IGuildUser user = null)
        {
            if (user == null)
            {
                await Context.Channel.SendMessageAsync($"{Context.User.Mention} você precisa mencionar quem será desmutado.");
                return;
            }

            var userAccount = UserAccounts.GetAccount((SocketUser)user);
            userAccount.IsMuted = false;
            UserAccounts.SaveAccounts();

            var m = await Context.Channel.SendMessageAsync(
                $"Usuário {user.Mention} foi desmutado por {Context.User.Mention}");
            await Task.Delay(2000);
            await m.DeleteAsync();

            //  Cria e envia o Log de unmute
            var logEmbed = EmbedHandler.LogEmbed("UnMute", Context.User, user);
            await Global.Client.GetGuild(Config.Bot.IdServer).GetTextChannel(Config.Bot.IdChatLog).SendMessageAsync("", false, logEmbed);

        }
        //  Kick
        [Command("Kick",RunMode = RunMode.Async)]
        [RequireUserPermission(GuildPermission.KickMembers)]
        [RequireBotPermission(GuildPermission.KickMembers)]
        internal async Task KickUser(IGuildUser user = null, string razão = "Razão não específica")
        {
            if (user == null)
            {
                await Context.Channel.SendMessageAsync($"{Context.User.Mention} você precisa mencionar quem será kickado.");
                return;
            }

            if (user.GuildPermissions.ManageChannels || user.GuildPermissions.ManageRoles)
            {
                await Context.Channel.SendMessageAsync($"{Context.User.Mention} você não pode kickar um Administrador.");
                return;
            }

            //  Cria e envia o Log de unmute
            var logEmbed = EmbedHandler.LogEmbed("Kick", Context.User, user);
            await Global.Client.GetGuild(Config.Bot.IdServer).GetTextChannel(Config.Bot.IdChatLog).SendMessageAsync("", false, logEmbed);

            // Kicka
            await user.KickAsync(razão);
        }

        //  Ban
        [Command("Ban", RunMode = RunMode.Async)]
        //[RequireUserPermission(GuildPermission.BanMembers)]
        [RequireBotPermission(GuildPermission.BanMembers)]
        internal async Task BanUser(IGuildUser user = null,[Remainder] string razão = "Razão não específica")
        {
            if (user == null)   // Verifica se o usuário mencionou alguém
            {
                await Context.Channel.SendMessageAsync($"{Context.User.Mention} você precisa mencionar quem será banido."); // Envia essa msg
                return; //  Caso não, retorna do método
            }
            
            var iuser = (IGuildUser)Context.User;
            if (!iuser.GuildPermissions.BanMembers) //  Verifica se o usuário tem permissão de banir, se tiver executa as ações a baixo
            {
                // Joke Ban
                var embUser = EmbedHandler.BanEmbed(Context.User, user, razão, true);
                var mEmbUser = await Context.Channel.SendMessageAsync(user.Mention, false, embUser);
                await Task.Delay(10000);
                await mEmbUser.DeleteAsync();
                return; //  Como o usuário não tem permissão de banir, ele retorna, para impedir o ban
            }

            //  Caso for alguém com permissão para banir... percorre o resto do código
            if (user.GuildPermissions.ManageChannels || user.GuildPermissions.ManageRoles)  // Verifica se o target do ban é alguém da staff
            {
                await Context.Channel.SendMessageAsync($"{Context.User.Mention} você não pode banir um membro da Staff."); // Envia essa msg
                return; // Caso for, retorna do método, para impedir o ban
            }

            //  Cria e envia o motivo do ban, pro usuário, via msg privada
            try
            {
                var pv = await user.GetOrCreateDMChannelAsync();
                var pvemb = EmbedHandler.PvBanEmbed(Context.User, user, razão);
                await pv.SendMessageAsync("", false, pvemb);
            }
            catch (Exception e) //  Erro ao tentar enviar msg privada
            {
                await Context.Channel.SendMessageAsync($"{Context.User} Erro: Não foi possível enviar o motivo do ban no privado do usuário {user.Mention}\n" +
                                                       $"```{e}```");
            }

            //  Cria e envia o Log de Ban <msg que irá pro chat de log>
            var logEmbed = EmbedHandler.LogEmbed("Ban", Context.User, user, razão);     // Cria a msg usando o EmbedHandler
            await Global.Client.GetGuild(Config.Bot.IdServer).GetTextChannel(Config.Bot.IdChatLog).SendMessageAsync("", false, logEmbed);   // Envia ela para o log

            //  Msg que irá aparecer no chat geral
            var embed = EmbedHandler.BanEmbed(Context.User, user, razão, false);        //  Cria a msg usando o EmbedHandler
            var m = await Context.Channel.SendMessageAsync(user.Mention, false, embed); //  Envia a msg, e atribui uma variavel para ela
            await user.Guild.AddBanAsync(user,0, razão);                                //  Dá ban no usuário
            await Context.Message.DeleteAsync();                                        //  Deleta comando<Msg com o comando> de ban, enviada pelo adm
            await Task.Delay(10000);                                                    //  Define um atraso de 10 segundos
            await m.DeleteAsync();                                                      //  Após o atraso, apaga o Embed do ban, lá no chat aonde o comando foi executado
        }

        // Warn
        [Command("Warn",RunMode = RunMode.Async)]
        [RequireUserPermission(GuildPermission.BanMembers)]
        [RequireBotPermission(GuildPermission.BanMembers)]
        internal async Task WarnUser(IGuildUser user = null, string razão = "Levou 3 warns")

        {
            if (user == null)   // Verifica se o usuário mencionou alguém
            {
                await Context.Channel.SendMessageAsync($"{Context.User.Mention} você precisa mencionar quem levará o warn.");   //  Envia essa msg
                return; //  Caso não, retorna do método
            }

            if (user.GuildPermissions.ManageChannels || user.GuildPermissions.ManageRoles)  // Verifica se oo usuário target é da staff
            {
                await Context.Channel.SendMessageAsync($"{Context.User.Mention} você não pode dar warn em um Staff.");  //  Envia essa msg
                return; // Caso for da staff, retorna do método
            }

            var userAccount = UserAccounts.GetAccount((SocketUser)user);
            var logembed = EmbedHandler.LogEmbed("Warn",Context.User,user);

            userAccount.NumberOfWarnings++;
            UserAccounts.SaveAccounts();

            switch (userAccount.NumberOfWarnings)
            {
                case 1:
                    await Context.Channel.SendMessageAsync
                        ($"{user.Mention}, você acaba de tomar seu primeiro **warn**, cuidado, se levar mais 2, será banido.");
                    break;

                case 2:
                    await Context.Channel.SendMessageAsync
                        ($"{user.Mention}, você já tem `2 warn`, no próximo, você será banido...");
                    break;

                case 3:
                    await Context.Channel.SendMessageAsync($"O usuário {user.Mention} acaba de levar `3 warn`, e foi banido.");

                    var warnlogbanembed = EmbedHandler.LogEmbed("Ban Warn", Context.User, user, razão);

                    var embed = EmbedHandler.BanEmbed(Context.User, user, razão, false);
                    var pv = await user.GetOrCreateDMChannelAsync();
                    var pvemb = EmbedHandler.PvBanEmbed(Context.User, user, razão);
                    var m = await Context.Channel.SendMessageAsync(user.Mention, false, embed);

                    await user.Guild.AddBanAsync(user, 0, razão);
                    await pv.SendMessageAsync("", false, pvemb);
                    await Task.Delay(10000);
                    await m.DeleteAsync();

                    await Global.Client.GetGuild(Config.Bot.IdServer).GetTextChannel(Config.Bot.IdChatLog).SendMessageAsync("", false, warnlogbanembed);
                    break;
            }

            if (userAccount.NumberOfWarnings != 3)  // Se o usuário não tiver 3 warns, é enviado o "warnlogembed" para o log, do contrário, envia o "logembed" que está ali em cima
            {
                await Global.Client.GetGuild(Config.Bot.IdServer).GetTextChannel(Config.Bot.IdChatLog).SendMessageAsync("", false, logembed);
            }
        }

        // Diz quantos warns, você tem
        [Command("Warns")]
        internal async Task Warns(IGuildUser user = null)
        {
            if (user == null)
            {
                var userAccount = UserAccounts.GetAccount(Context.User);
                await Context.Channel.SendMessageAsync($"{Context.User.Mention}Você tem `{userAccount.NumberOfWarnings} warn`");
            }
            else
            {
                var userAccount = UserAccounts.GetAccount((SocketUser)user);
                await Context.Channel.SendMessageAsync($"{user.Mention} tem `{userAccount.NumberOfWarnings} warn`");
            }
        }

        [Command("Tirar Warns")]
        [RequireUserPermission(GuildPermission.BanMembers)]
        internal async Task TirarWarn(IGuildUser user = null)
        {
            if (user == null)
            {
                await Context.Channel.SendMessageAsync($"{Context.User.Mention} você precisa mencionar quem terá os warns removidos.");
                return;
            }

            var userAccount = UserAccounts.GetAccount((SocketUser)user);

            while (userAccount.NumberOfWarnings > 0)
            {
                userAccount.NumberOfWarnings--;
                UserAccounts.SaveAccounts();
            }
            var warnlogembed = EmbedHandler.LogEmbed("Tirar Warns",Context.User,user);
            await Global.Client.GetGuild(Config.Bot.IdServer).GetTextChannel(Config.Bot.IdChatLog).SendMessageAsync("", false, warnlogembed);

            await Context.Channel.SendMessageAsync($"Agora {user.Mention} tem `{userAccount.NumberOfWarnings} warn`");
        }
    }
}
