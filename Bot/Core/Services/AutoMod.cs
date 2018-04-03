using System;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using DPP_Bot.Core.Configs;

namespace DPP_Bot.Core.Services
{
    public class AutoMod
    {
        internal Task AutoModInvite(SocketCommandContext context)
        {
            Task.Run(async () =>        // Cria uma tarefa, e não travar o programa, até a conclusão
            {
                var iuser = (IGuildUser)context.User;   //  Faz o cast do usuário, para IGuildUser, que será ultilizado mais para frente

                if (iuser.GuildPermissions.ManageChannels || iuser.GuildPermissions.ManageRoles)    // Verifica se é da staff
                {
                    return; //  Se for, retorna... quem é da staff pode mandar links de servidores né kkkkk
                }

                if (context.Channel.Id == Config.Bot.IdChatConvites) // Verifica se a menssagem foi enviada no canal de convites
                {
                    return; //  Retorna. É permitido enviar convites dentro dessa sala...
                }

                await context.Message.DeleteAsync();

                var userAccount = Core.UserAccounts.UserAccounts.GetAccount(context.User);

                var logembed = EmbedHandler.LogEmbed("Warn", Global.Client.CurrentUser, iuser); //  Cria a embed de warn

                userAccount.NumberOfWarnings++;
                Core.UserAccounts.UserAccounts.SaveAccounts();

                switch (userAccount.NumberOfWarnings)   // De acordo com o número de warns, ele entra em um "caso"
                {
                    case 1:                             //  Caso usuário tiver 1 warn...
                        await context.Channel.SendMessageAsync
                            ($"{context.User.Mention}, Você não pode divulgar servidores aqui, e por isso acaba de tomar seu primeiro **warn**, cuidado, se levar mais 2, será banido.");
                        break;

                    case 2:                             //  Caso tiver 2...
                        await context.Channel.SendMessageAsync
                            ($"{context.User.Mention}, Você não pode divulgar servidores aqui;\nCuidado você já tem `2 warn`, no próximo, será banido...");
                        break;

                    case 3:                             // Caso tiver 3
                        const string razão = "Ter 2 warns, e completar 3 ao divulgar servidor.";

                        var warnlogbanembed = EmbedHandler.LogEmbed("banwarn", Global.Client.CurrentUser,iuser,razão);      //  Cria o BanWarn
                        var warnBanEmbed = EmbedHandler.BanEmbed(Global.Client.CurrentUser,iuser, razão, false);            //  Embed que deverá sair no chat onde foi enviado o link do invite
                        var pv = await context.User.GetOrCreateDMChannelAsync();                                            //  Cria uma variavel para a msg do privado
                        var pvemb = EmbedHandler.PvBanEmbed(Global.Client.CurrentUser,iuser, razão);                        //  Cria a embed que será enviada no privado
                        var m = await context.Channel.SendMessageAsync(context.User.Mention, false, warnBanEmbed);          //  Cria uma variável para a msg, e envia a msg onde o link foi enviado   
                                                                                                                            
                        try                                                                                                 //  Tenta enviar a msg no privado
                        {
                            await pv.SendMessageAsync("", false, pvemb);                                                    //  Envia a msg no privado
                        }
                        catch (Exception e)                                                                                 // Caso dê algum erro
                        {
                            await context.Channel.SendMessageAsync
                            ($"Erro ao enviar msg no privado do usuário banido: ```{e}```");                                //  Envia essa msg
                            throw;
                        }

                        await iuser.Guild.AddBanAsync(context.User, 0, razão);                                              //  Cria o ban no discord
                        await Task.Delay(10000);                                                                            //  Espera 10 segundos
                        await m.DeleteAsync();                                                                              //  Deleta a msg

                        await Global.Client.GetGuild(Config.Bot.IdServer).GetTextChannel(Config.Bot.IdChatLog)
                            .SendMessageAsync("", false, warnlogbanembed);                                                  //  Envia a msg para o log
                        break;                                                                                              //  Sai do switch
                }

                if (userAccount.NumberOfWarnings != 3)  // Se o usuário não tiver 3 warns, é enviado o "warnlogembed" para o log, do contrário, envia o "logembed" que está ali em cima
                {
                    await Global.Client.GetGuild(Config.Bot.IdServer).GetTextChannel(Config.Bot.IdChatLog).SendMessageAsync("", false, logembed);
                }
            });
            return Task.CompletedTask;
        }
    }
}
