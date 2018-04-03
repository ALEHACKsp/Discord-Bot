using System;
using System.Threading.Tasks;
using Discord;
using Discord.WebSocket;

namespace DPP_Bot.Core.Services
{
    public class BoasVindas
    {
        internal Task NovoMembro(SocketGuildUser user)
        {
            Task.Run(async () =>
            {
                try
                {
                    Global.MembrosNaUltimaHora++;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    throw;
                }

                var canal = (SocketTextChannel)Global.Client.GetChannel(Configs.Config.Bot.IdChatGeral);  // Chat geral (boas vindas)

                var userAccount = Core.UserAccounts.UserAccounts.GetAccount(user);

                while (userAccount.NumberOfWarnings > 0)
                {
                    userAccount.NumberOfWarnings--;
                    Core.UserAccounts.UserAccounts.SaveAccounts();
                }

                var embed = EmbedHandler.BoasVindasEmbed(user);     // Embed boas vindas

                // Log Entrou
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.Write($"{DateTime.Now:HH:mm:ss tt} [  Evento]  Entrou: ");
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine($"{user.Username} | ID: {user.Id}");

                // Chat LOG Servidor
                var canallog = (SocketTextChannel)Global.Client.GetChannel(Configs.Config.Bot.IdChatLog);
                var logembedbuilder = EmbedHandler.BoasVindasLogEmbed(user);
                await canallog.SendMessageAsync("", false, logembedbuilder);

                // Tenta enviar mensagem de boas vindas no privado
                try
                {
                    // Gera mensagem de boas vindas no privado
                    var pv = await user.GetOrCreateDMChannelAsync();
                    var pvembed = EmbedHandler.PvBoasVindasEmbed(user);
                    await pv.SendMessageAsync("", false, pvembed); //Envia mensagem de boas vindas no privado
                }
                catch (Exception)
                {
                    var pverro = await canal.SendMessageAsync(
                        $"{user.Mention} Não foi possível enviar minha mensagem de boas vindas no privado :tired_face: ");
                    await Task.Delay(5000);
                    await pverro.DeleteAsync();
                    throw;
                }


                // Envia mensagem de boas vindas no canal de boas vindas
                var m = await canal.SendMessageAsync(user.Mention, false, embed);
                await Task.Delay(10000);
                await m.DeleteAsync();
            });
            return Task.CompletedTask;
        }
    }
}
