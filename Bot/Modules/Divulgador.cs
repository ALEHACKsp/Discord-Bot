using System.Linq;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using DPP_Bot.Core.Services;

namespace DPP_Bot.Modules
{
    public class Divulgador : ModuleBase<SocketCommandContext>
    {
        private readonly SelfRoles _selfRoles = new SelfRoles();

        [Command("Divulgador",RunMode = RunMode.Async)]
        [Summary("Retorna a contagem de convites para o usuário")]
        public async Task ContadorDeConvites(IGuildUser user = null)
        {
            if (user == null)
            {
                var invites = await Context.Guild.GetInvitesAsync();
                var func = invites.FirstOrDefault(x => x.Inviter.Id == Context.User.Id);

                if (func != null)
                {
                    var valor = func.Uses;
                    //await ReplyAsync(valor.ToString());

                    if (valor > 100)
                    {
                        await ReplyAsync($"{Context.User.Mention} você convidou ``{valor}`` pessoas para o servidor!\n" +
                                         "Parabéns você chegou ao LV 20 de divulgador, e obteve acesso ao chat ``divulgador-lv-20``\n" +
                                         "Você é incrível :yum:, você conseguiu");
                        await _selfRoles.AddOrDellRole(Context.User, Context.Channel, "Divulgador [LV. 20]", "Divulgador [LV. 5]");
                    }
                    else if (valor > 50)
                    {
                        await ReplyAsync($"{Context.User.Mention} você convidou ``{valor}`` pessoas para o servidor!\n" +
                                         "Verifique seus cargos\n" +
                                         "Você está quase lá... Falta pouco");
                        await _selfRoles.AddOrDellRole(Context.User, Context.Channel, "Divulgador [LV. 10]", "Divulgador [LV. 5]");
                    }
                    else if (valor > 25)
                    {
                        await ReplyAsync($"{Context.User.Mention} você convidou ``{valor}`` pessoas para o servidor!\n" +
                                         "Verifique seus cargos\n" +
                                         "Não desista");
                        await _selfRoles.AddOrDellRole(Context.User, Context.Channel, "Divulgador [LV. 5]", "Divulgador [LV. 1]");
                    }
                    else if (valor >= 1)
                    {
                        await ReplyAsync($"{Context.User.Mention} você convidou ``{valor}`` pessoas para o servidor!\n" +
                                         "Verifique seus cargos\n" +
                                         "Continue assim e será recompensado");
                        await _selfRoles.AddOrDellRole(Context.User, Context.Channel, "Divulgador [LV. 1]");
                    }
                    else
                    {
                        await ReplyAsync($"{Context.User.Mention} você ainda não convidou ninguém :pensive:");
                    }
                }
                else
                {
                    await ReplyAsync($"{Context.User.Mention} Não encontrei nenhum convite gerado por você :confused:");
                }
            }
            else
            {
                var userInvites = await user.Guild.GetInvitesAsync();
                var funcUser = userInvites.FirstOrDefault(x => x.Inviter.Id == user.Id);

                if (funcUser != null)
                {
                    var valor = funcUser.Uses;
                    await ReplyAsync($"{Context.User.Mention}, o usuário {user.Mention} convidou ``{valor}`` pessoas para o servidor!");
                }
                else
                {
                    await ReplyAsync($"{Context.User.Mention}, não encontrei nenhum convite gerado pelo usuário mencionado :confused:");
                }
                
            }
        }
    }
}
