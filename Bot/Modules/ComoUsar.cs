using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using DPP_Bot.Core.Configs;

namespace DPP_Bot.Modules
{
    [Group("Como Usar")]    // Isso gera um grupo de comandos, tudo que estiver dentro desse grupo
                            // Tem que iniciar com "Como Usar", seguido do comando dentro do grupo
    public class ComoUsar : ModuleBase<SocketCommandContext>
    {
        //Como Usar
        [Command]
        [Summary("Como Usar: Como usar.")]
        [RequireBotPermission(ChannelPermission.SendMessages)]
        public async Task _ComoUsar()
        {
            string _cmd = "Como Usar";              //Comando
            string pcmd = Config.Bot.CmdPrefix + _cmd;


            var eb = new EmbedBuilder()
            {
                Title = "Como usar:",
                Color = new Color(4, 97, 247),
                ThumbnailUrl = (Context.Client.CurrentUser.GetAvatarUrl()),
                Footer = new EmbedFooterBuilder()
                {
                    Text = $"Solicitado por {Context.User.Username}#{Context.User.Discriminator}",
                    IconUrl = (Context.User.GetAvatarUrl())
                }
            };
            eb.AddField((efb) =>
            {
                efb.Name = $":thinking: Qual a função de `{pcmd}`?";
                efb.IsInline = true;
                efb.Value =                         //Para que serve o comando
                    "Esse comando serve para você entender melhor como utilizar outros comandos... \n";
            });
            eb.AddField((efb) =>
            {
                efb.Name = ":thumbsup: Como ultilizar";
                efb.IsInline = true;
                efb.Value =                         //Descrição do comando
                    $"Você deve digitar `{pcmd}`, seguido do comando desejado...";
            });
            eb.AddField((efb) =>
            {
                efb.Name = ":book:Exemplo de uso:";
                efb.IsInline = true;                //Exemplos
                efb.Value = $"`{pcmd} Abraçar`\n" +
                            $"`{pcmd} Repetir`\n" +
                            $"`{pcmd} Escolha`\n";
            });
            await ReplyAsync(Context.User.Mention, false, eb);
        }

        //Oi
        [Command("Oi")]
        [Summary("Como Usar: Oi.")]
        [RequireBotPermission(ChannelPermission.SendMessages)]
        public async Task _ComoUsarOi()
        {
            string _cmd = "Oi";              //Comando
            string pcmd = Config.Bot.CmdPrefix + _cmd;


            var eb = new EmbedBuilder()
            {
                Title = "Como usar:",
                Color = new Color(4, 97, 247),
                ThumbnailUrl = (Context.Client.CurrentUser.GetAvatarUrl()),
                Footer = new EmbedFooterBuilder()
                {
                    Text = $"Solicitado por {Context.User.Username}#{Context.User.Discriminator}",
                    IconUrl = (Context.User.GetAvatarUrl())
                }
            };
            eb.AddField((efb) =>
            {
                efb.Name = $":thinking: Qual a função de `{pcmd}`?";
                efb.IsInline = true;
                efb.Value =                         //Para que serve o comando
                    "O bot irá lhe dar Oi... \n";
            });
            eb.AddField((efb) =>
            {
                efb.Name = ":thumbsup: Como ultilizar";
                efb.IsInline = true;
                efb.Value =                         //Descrição do comando
                    $"Você deve digitar `{pcmd}`...";
            });
            eb.AddField((efb) =>
            {
                efb.Name = ":book:Exemplo de uso:";
                efb.IsInline = true;                //Exemplos
                efb.Value = $"`{pcmd}`\n";
            });
            await ReplyAsync(Context.User.Mention, false, eb);
        }

        //Ajuda
        [Command("Ajuda")]
        [Summary("Como Usar: Ajuda.")]
        [RequireBotPermission(ChannelPermission.SendMessages)]
        public async Task _ComoUsarAjuda()
        {
            string _cmd = "Ajuda";              //Comando
            string pcmd = Config.Bot.CmdPrefix + _cmd;


            var eb = new EmbedBuilder()
            {
                Title = "Como usar:",
                Color = new Color(4, 97, 247),
                ThumbnailUrl = (Context.Client.CurrentUser.GetAvatarUrl()),
                Footer = new EmbedFooterBuilder()
                {
                    Text = $"Solicitado por {Context.User.Username}#{Context.User.Discriminator}",
                    IconUrl = (Context.User.GetAvatarUrl())
                }
            };
            eb.AddField((efb) =>
            {
                efb.Name = $":thinking: Qual a função de `{pcmd}`?";
                efb.IsInline = true;
                efb.Value =                         //Para que serve o comando
                    "O Bot irá lhe enviar uma mensagem privada, contendo todos os comandos até agora... \n";
            });
            eb.AddField((efb) =>
            {
                efb.Name = ":thumbsup: Como ultilizar";
                efb.IsInline = true;
                efb.Value =                         //Descrição do comando
                    $"Você deve digitar `{pcmd}`...";
            });
            eb.AddField((efb) =>
            {
                efb.Name = ":book:Exemplo de uso:";
                efb.IsInline = true;                //Exemplos
                efb.Value = $"`{pcmd}`\n";
            });
            await ReplyAsync(Context.User.Mention, false, eb);
        }

        //Repetir
        [Command("Repetir")]
        [Summary("Como Usar: Repetir.")]
        [RequireBotPermission(ChannelPermission.SendMessages)]
        public async Task _ComoUsarRepetir()
        {
            string _cmd = "Repetir";              //Comando
            string pcmd = Config.Bot.CmdPrefix + _cmd;


            var eb = new EmbedBuilder()
            {
                Title = "Como usar:",
                Color = new Color(4, 97, 247),
                ThumbnailUrl = (Context.Client.CurrentUser.GetAvatarUrl()),
                Footer = new EmbedFooterBuilder()
                {
                    Text = $"Solicitado por {Context.User.Username}#{Context.User.Discriminator}",
                    IconUrl = (Context.User.GetAvatarUrl())
                }
            };
            eb.AddField((efb) =>
            {
                efb.Name = $":thinking: Qual a função de `{pcmd}`?";
                efb.IsInline = true;
                efb.Value =                         //Para que serve o comando
                    "O bot irá repetir a frase que for colocada, após o comando...\n";
            });
            eb.AddField((efb) =>
            {
                efb.Name = ":thumbsup: Como ultilizar";
                efb.IsInline = true;
                efb.Value =                         //Descrição do comando
                    $"Você deve digitar `{pcmd} Frase`...";
            });
            eb.AddField((efb) =>
            {
                efb.Name = ":book:Exemplo de uso:";
                efb.IsInline = true;                //Exemplos
                efb.Value = $"`{pcmd} Oi meu nome é {Context.User.Username}`\n" +
                            $"`{pcmd} Sorvetinho é :top:`\n" +
                            $"`{pcmd} Hai gais is30 on`\n";
            });
            await ReplyAsync(Context.User.Mention, false, eb);
        }

        //Escolha
        [Command("Escolha")]
        [Summary("Como Usar: Escolha.")]
        [RequireBotPermission(ChannelPermission.SendMessages)]
        public async Task _ComoUsarEscolha()
        {
            string _cmd = "Escolha";              //Comando
            string pcmd = Config.Bot.CmdPrefix + _cmd;


            var eb = new EmbedBuilder()
            {
                Title = "Como usar:",
                Color = new Color(4, 97, 247),
                ThumbnailUrl = (Context.Client.CurrentUser.GetAvatarUrl()),
                Footer = new EmbedFooterBuilder()
                {
                    Text = $"Solicitado por {Context.User.Username}#{Context.User.Discriminator}",
                    IconUrl = (Context.User.GetAvatarUrl())
                }
            };
            eb.AddField((efb) =>
            {
                efb.Name = $":thinking: Qual a função de `{pcmd}`?";
                efb.IsInline = true;
                efb.Value =                         //Para que serve o comando
                    "Eu escolho algo, dentro das opções dadas... As opções devem ser separadas por `vírgula`\n";
            });
            eb.AddField((efb) =>
            {
                efb.Name = ":thumbsup: Como ultilizar";
                efb.IsInline = true;
                efb.Value =                         //Descrição do comando
                    $"Você deve digitar `{pcmd} Opções`...";
            });
            eb.AddField((efb) =>
            {
                efb.Name = ":book:Exemplo de uso:";
                efb.IsInline = true;                //Exemplos
                efb.Value = $"`{pcmd} Cachorro Quente,Pizza,Salada`\n" +
                            $"`{pcmd} Sorvete,Batata Frita`\n" +
                            $"`{pcmd} Lolzinho,Dota,Minecraft`\n";
            });
            await ReplyAsync(Context.User.Mention, false, eb);
        }

        //Eu te amo
        [Command("Eu te amo")]
        [Summary("Como Usar: Eu te amo.")]
        [RequireBotPermission(ChannelPermission.SendMessages)]
        public async Task _ComoUsarEuTeAmo()
        {
            string _cmd = "Eu te amo";              //Comando
            string pcmd = Config.Bot.CmdPrefix + _cmd;


            var eb = new EmbedBuilder()
            {
                Title = "Como usar:",
                Color = new Color(4, 97, 247),
                ThumbnailUrl = (Context.Client.CurrentUser.GetAvatarUrl()),
                Footer = new EmbedFooterBuilder()
                {
                    Text = $"Solicitado por {Context.User.Username}#{Context.User.Discriminator}",
                    IconUrl = (Context.User.GetAvatarUrl())
                }
            };
            eb.AddField((efb) =>
            {
                efb.Name = $":thinking: Qual a função de `{pcmd}`?";
                efb.IsInline = true;
                efb.Value =                         //Para que serve o comando
                    "Digo o que sinto por você... \n";
            });
            eb.AddField((efb) =>
            {
                efb.Name = ":thumbsup: Como ultilizar";
                efb.IsInline = true;
                efb.Value =                         //Descrição do comando
                    $"Você deve digitar `{pcmd}`";
            });
            eb.AddField((efb) =>
            {
                efb.Name = ":book:Exemplo de uso:";
                efb.IsInline = true;                //Exemplos
                efb.Value = $"`{pcmd}`\n";
            });
            await ReplyAsync(Context.User.Mention, false, eb);
        }

        //Bom Dia
        [Command("Bom Dia")]
        [Summary("Como Usar: Bom Dia.")]
        [RequireBotPermission(ChannelPermission.SendMessages)]
        public async Task _ComoUsarBomDia()
        {
            string _cmd = "Bom Dia";              //Comando
            string pcmd = Config.Bot.CmdPrefix + _cmd;

            var eb = new EmbedBuilder()
            {
                Title = "Como usar:",
                Color = new Color(4, 97, 247),
                ThumbnailUrl = (Context.Client.CurrentUser.GetAvatarUrl()),
                Footer = new EmbedFooterBuilder()
                {
                    Text = $"Solicitado por {Context.User.Username}#{Context.User.Discriminator}",
                    IconUrl = (Context.User.GetAvatarUrl())
                }
            };
            eb.AddField((efb) =>
            {
                efb.Name = $":thinking: Qual a função de `{pcmd}`?";
                efb.IsInline = true;
                efb.Value =                         //Para que serve o comando
                    "Irei lhe dar bom dia \n";
            });
            eb.AddField((efb) =>
            {
                efb.Name = ":thumbsup: Como ultilizar";
                efb.IsInline = true;
                efb.Value =                         //Descrição do comando
                    $"Você deve digitar `{pcmd}`";
            });
            eb.AddField((efb) =>
            {
                efb.Name = ":book:Exemplo de uso:";
                efb.IsInline = true;                //Exemplos
                efb.Value = $"`{pcmd}`\n";
            });
            await ReplyAsync(Context.User.Mention, false, eb);
        }

        //Boa Noite
        [Command("Boa Noite")]
        [Summary("Como Usar: Boa Noite.")]
        [RequireBotPermission(ChannelPermission.SendMessages)]
        public async Task _ComoUsarBoaNoite()
        {
            string _cmd = "Boa Noite";              //Comando
            string pcmd = Config.Bot.CmdPrefix + _cmd;

            var eb = new EmbedBuilder()
            {
                Title = "Como usar:",
                Color = new Color(4, 97, 247),
                ThumbnailUrl = (Context.Client.CurrentUser.GetAvatarUrl()),
                Footer = new EmbedFooterBuilder()
                {
                    Text = $"Solicitado por {Context.User.Username}#{Context.User.Discriminator}",
                    IconUrl = (Context.User.GetAvatarUrl())
                }
            };
            eb.AddField((efb) =>
            {
                efb.Name = $":thinking: Qual a função de `{pcmd}`?";
                efb.IsInline = true;
                efb.Value =                         //Para que serve o comando
                    "Irei lhe dar boa noite \n";
            });
            eb.AddField((efb) =>
            {
                efb.Name = ":thumbsup: Como ultilizar";
                efb.IsInline = true;
                efb.Value =                         //Descrição do comando
                    $"Você deve digitar `{pcmd}`";
            });
            eb.AddField((efb) =>
            {
                efb.Name = ":book:Exemplo de uso:";
                efb.IsInline = true;                //Exemplos
                efb.Value = $"`{pcmd}`\n";
            });
            await ReplyAsync(Context.User.Mention, false, eb);
        }

        //Ping
        [Command("Ping")]
        [Summary("Como Usar: Ping.")]
        [RequireBotPermission(ChannelPermission.SendMessages)]
        public async Task _ComoUsarPing()
        {
            string _cmd = "Ping";              //Comando
            string pcmd = Config.Bot.CmdPrefix + _cmd;

            var eb = new EmbedBuilder()
            {
                Title = "Como usar:",
                Color = new Color(4, 97, 247),
                ThumbnailUrl = (Context.Client.CurrentUser.GetAvatarUrl()),
                Footer = new EmbedFooterBuilder()
                {
                    Text = $"Solicitado por {Context.User.Username}#{Context.User.Discriminator}",
                    IconUrl = (Context.User.GetAvatarUrl())
                }
            };
            eb.AddField((efb) =>
            {
                efb.Name = $":thinking: Qual a função de `{pcmd}`?";
                efb.IsInline = true;
                efb.Value =                         //Para que serve o comando
                    "O bot irá lhe responder o tempo de resposta, entre eu e você \n";
            });
            eb.AddField((efb) =>
            {
                efb.Name = ":thumbsup: Como ultilizar";
                efb.IsInline = true;
                efb.Value =                         //Descrição do comando
                    $"Você deve digitar `{pcmd}`...";
            });
            eb.AddField((efb) =>
            {
                efb.Name = ":book:Exemplo de uso:";
                efb.IsInline = true;                //Exemplos
                efb.Value = $"`{pcmd}`\n";
            });
            await ReplyAsync(Context.User.Mention, false, eb);
        }

        //Votar
        [Command("Votar")]
        [Summary("Como Usar: Votar.")]
        [RequireBotPermission(ChannelPermission.SendMessages)]
        public async Task _ComoUsarVotar()
        {
            string _cmd = "Votar";              //Comando
            string pcmd = Config.Bot.CmdPrefix + _cmd;

            var eb = new EmbedBuilder()
            {
                Title = "Como usar:",
                Color = new Color(4, 97, 247),
                ThumbnailUrl = (Context.Client.CurrentUser.GetAvatarUrl()),
                Footer = new EmbedFooterBuilder()
                {
                    Text = $"Solicitado por {Context.User.Username}#{Context.User.Discriminator}",
                    IconUrl = (Context.User.GetAvatarUrl())
                }
            };
            eb.AddField((efb) =>
            {
                efb.Name = $":thinking: Qual a função de `{pcmd}`?";
                efb.IsInline = true;
                efb.Value =                         //Para que serve o comando
                    "Você faço uma votação com duas opções, uma positiva, e outra negativa... \n";
            });
            eb.AddField((efb) =>
            {
                efb.Name = ":thumbsup: Como ultilizar";
                efb.IsInline = true;
                efb.Value =                         //Descrição do comando
                    $"Você deve digitar `{pcmd} O que será votado`...";
            });
            eb.AddField((efb) =>
            {
                efb.Name = ":book:Exemplo de uso:";
                efb.IsInline = true;                //Exemplos
                efb.Value = $"`{pcmd} O HyperBot, é muito bom?`\n" +
                            $"`{pcmd} Deveriamos jogar mais uma partida?`\n" +
                            $"`{pcmd} A reunião foi boa?`\n";
            });
            await ReplyAsync(Context.User.Mention, false, eb);
        }

        //Sugestão
        [Command("Sugestão")]
        [Summary("Como Usar: Sugestão.")]
        [RequireBotPermission(ChannelPermission.SendMessages)]
        public async Task _ComoUsarSugestão()
        {
            string _cmd = "Sugestão";              //Comando
            string pcmd = Config.Bot.CmdPrefix + _cmd;

            var eb = new EmbedBuilder()
            {
                Title = "Como usar:",
                Color = new Color(4, 97, 247),
                ThumbnailUrl = (Context.Client.CurrentUser.GetAvatarUrl()),
                Footer = new EmbedFooterBuilder()
                {
                    Text = $"Solicitado por {Context.User.Username}#{Context.User.Discriminator}",
                    IconUrl = (Context.User.GetAvatarUrl())
                }
            };
            eb.AddField((efb) =>
            {
                efb.Name = $":thinking: Qual a função de `{pcmd}`?";
                efb.IsInline = true;
                efb.Value =                         //Para que serve o comando
                    "Lhe dou uma resposta positiva ou negativa, para a sua pergunta. \n";
            });
            eb.AddField((efb) =>
            {
                efb.Name = ":thumbsup: Como ultilizar";
                efb.IsInline = true;
                efb.Value =                         //Descrição do comando
                    $"Você deve digitar `{pcmd} Pergunta`...";
            });
            eb.AddField((efb) =>
            {
                efb.Name = ":book:Exemplo de uso:";
                efb.IsInline = true;                //Exemplos
                efb.Value = $"`{pcmd} Sou uma pessoa legal?`\n" +
                            $"`{pcmd} {Context.User.Username} deveria me pagar uma pizza?`\n" +
                            $"`{pcmd} Lolzinho > Dotta?`\n";
            });
            await ReplyAsync(Context.User.Mention, false, eb);
        }

        //Sobre
        [Command("Sobre")]
        [Summary("Como Usar: Sobre.")]
        [RequireBotPermission(ChannelPermission.SendMessages)]
        public async Task _ComoUsarSobre()
        {
            string _cmd = "Sobre";              //Comando
            string pcmd = Config.Bot.CmdPrefix + _cmd;

            var eb = new EmbedBuilder()
            {
                Title = "Como usar:",
                Color = new Color(4, 97, 247),
                ThumbnailUrl = (Context.Client.CurrentUser.GetAvatarUrl()),
                Footer = new EmbedFooterBuilder()
                {
                    Text = $"Solicitado por {Context.User.Username}#{Context.User.Discriminator}",
                    IconUrl = (Context.User.GetAvatarUrl())
                }
            };
            eb.AddField((efb) =>
            {
                efb.Name = $":thinking: Qual a função de `{pcmd}`?";
                efb.IsInline = true;
                efb.Value =                         //Para que serve o comando
                    "Irei falar um pouco sobre meu criador... \n";
            });
            eb.AddField((efb) =>
            {
                efb.Name = ":thumbsup: Como ultilizar";
                efb.IsInline = true;
                efb.Value =                         //Descrição do comando
                    $"Você deve digitar `{pcmd}`...";
            });
            eb.AddField((efb) =>
            {
                efb.Name = ":book:Exemplo de uso:";
                efb.IsInline = true;                //Exemplos
                efb.Value = $"`{pcmd}`\n";
            });
            await ReplyAsync(Context.User.Mention, false, eb);
        }

        //Aquela Carinha
        [Command("Aquela Carinha")]
        [Summary("Como Usar: Aquela Carinha.")]
        [RequireBotPermission(ChannelPermission.SendMessages)]
        public async Task _ComoUsarAquelaCarinha()
        {
            string _cmd = "Aquela Carinha";              //Comando
            string pcmd = Config.Bot.CmdPrefix + _cmd;

            var eb = new EmbedBuilder()
            {
                Title = "Como usar:",
                Color = new Color(4, 97, 247),
                ThumbnailUrl = (Context.Client.CurrentUser.GetAvatarUrl()),
                Footer = new EmbedFooterBuilder()
                {
                    Text = $"Solicitado por {Context.User.Username}#{Context.User.Discriminator}",
                    IconUrl = (Context.User.GetAvatarUrl())
                }
            };
            eb.AddField((efb) =>
            {
                efb.Name = $":thinking: Qual a função de `{pcmd}`?";
                efb.IsInline = true;
                efb.Value =                         //Para que serve o comando
                    "Aquela Carinha ( ͡° ͜ʖ ͡°)\n";
            });
            eb.AddField((efb) =>
            {
                efb.Name = ":thumbsup: Como ultilizar";
                efb.IsInline = true;
                efb.Value =                         //Descrição do comando
                    $"Você deve digitar `{pcmd}`...";
            });
            eb.AddField((efb) =>
            {
                efb.Name = ":book:Exemplo de uso:";
                efb.IsInline = true;                //Exemplos
                efb.Value = $"`{pcmd}`\n";
            });
            await ReplyAsync(Context.User.Mention, false, eb);
        }

        //Membros
        [Command("Membros")]
        [Summary("Como Usar: Membros.")]
        [RequireBotPermission(ChannelPermission.SendMessages)]
        public async Task _ComoUsarMembros()
        {
            string _cmd = "Membros";              //Comando
            string pcmd = Config.Bot.CmdPrefix + _cmd;

            var eb = new EmbedBuilder()
            {
                Title = "Como usar:",
                Color = new Color(4, 97, 247),
                ThumbnailUrl = (Context.Client.CurrentUser.GetAvatarUrl()),
                Footer = new EmbedFooterBuilder()
                {
                    Text = $"Solicitado por {Context.User.Username}#{Context.User.Discriminator}",
                    IconUrl = (Context.User.GetAvatarUrl())
                }
            };
            eb.AddField((efb) =>
            {
                efb.Name = $":thinking: Qual a função de `{pcmd}`?";
                efb.IsInline = true;
                efb.Value =                         //Para que serve o comando
                    "Lhe respondo a quantidade de membros no servidor... \n";
            });
            eb.AddField((efb) =>
            {
                efb.Name = ":thumbsup: Como ultilizar";
                efb.IsInline = true;
                efb.Value =                         //Descrição do comando
                    $"Você deve digitar `{pcmd}`...";
            });
            eb.AddField((efb) =>
            {
                efb.Name = ":book:Exemplo de uso:";
                efb.IsInline = true;                //Exemplos
                efb.Value = $"`{pcmd}`\n";
            });
            await ReplyAsync(Context.User.Mention, false, eb);
        }

        //Avatar
        [Command("Avatar")]
        [Summary("Como Usar: Avatar.")]
        [RequireBotPermission(ChannelPermission.SendMessages)]
        public async Task _ComoUsarAvatar()
        {
            string _cmd = "Avatar";              //Comando
            string pcmd = Config.Bot.CmdPrefix + _cmd;

            var eb = new EmbedBuilder()
            {
                Title = "Como usar:",
                Color = new Color(4, 97, 247),
                ThumbnailUrl = (Context.Client.CurrentUser.GetAvatarUrl()),
                Footer = new EmbedFooterBuilder()
                {
                    Text = $"Solicitado por {Context.User.Username}#{Context.User.Discriminator}",
                    IconUrl = (Context.User.GetAvatarUrl())
                }
            };
            eb.AddField((efb) =>
            {
                efb.Name = $":thinking: Qual a função de `{pcmd}`?";
                efb.IsInline = true;
                efb.Value =                         //Para que serve o comando
                    "Envio no chat o avatar da pessoa mencionada, possibilitando o salvamento da imagem. \n";
            });
            eb.AddField((efb) =>
            {
                efb.Name = ":thumbsup: Como ultilizar";
                efb.IsInline = true;
                efb.Value =                         //Descrição do comando
                    $"Você deve digitar `{pcmd} @Usuário#0000`...";
            });
            eb.AddField((efb) =>
            {
                efb.Name = ":book:Exemplo de uso:";
                efb.IsInline = true;                //Exemplos
                efb.Value = $"`{pcmd} @{Context.User.Username}#{Context.User.Discriminator}`\n";
            });
            await ReplyAsync(Context.User.Mention, false, eb);
        }

        //Abraçar
        [Command("Abraçar")]
        [Summary("Como Usar: Abraçar.")]
        [RequireBotPermission(ChannelPermission.SendMessages)]
        public async Task _ComoUsarAbraçar()
        {
            string _cmd = "Abraçar";              //Comando
            string pcmd = Config.Bot.CmdPrefix + _cmd;

            var eb = new EmbedBuilder()
            {
                Title = "Como usar:",
                Color = new Color(4, 97, 247),
                ThumbnailUrl = (Context.Client.CurrentUser.GetAvatarUrl()),
                Footer = new EmbedFooterBuilder()
                {
                    Text = $"Solicitado por {Context.User.Username}#{Context.User.Discriminator}",
                    IconUrl = (Context.User.GetAvatarUrl())
                }
            };
            eb.AddField((efb) =>
            {
                efb.Name = $":thinking: Qual a função de `{pcmd}`?";
                efb.IsInline = true;
                efb.Value =                         //Para que serve o comando
                    "Envio um gif, e uma frase fofa, dizendo que você abraçou a pessoa mencionada. \n";
            });
            eb.AddField((efb) =>
            {
                efb.Name = ":thumbsup: Como ultilizar";
                efb.IsInline = true;
                efb.Value =                         //Descrição do comando
                    $"Você deve digitar `{pcmd} @usuário#0000`...";
            });
            eb.AddField((efb) =>
            {
                efb.Name = ":book:Exemplo de uso:";
                efb.IsInline = true;                //Exemplos
                efb.Value = $"`{pcmd} @{Context.User.Username}#{Context.User.Discriminator}`\n";
            });
            await ReplyAsync(Context.User.Mention, false, eb);
        }

        //Swag
        [Command("Swag")]
        [Summary("Como Usar: Swag.")]
        [RequireBotPermission(ChannelPermission.SendMessages)]
        public async Task _ComoUsarSwag()
        {
            string _cmd = "Swag";              //Comando
            string pcmd = Config.Bot.CmdPrefix + _cmd;

            var eb = new EmbedBuilder()
            {
                Title = "Como usar:",
                Color = new Color(4, 97, 247),
                ThumbnailUrl = (Context.Client.CurrentUser.GetAvatarUrl()),
                Footer = new EmbedFooterBuilder()
                {
                    Text = $"Solicitado por {Context.User.Username}#{Context.User.Discriminator}",
                    IconUrl = (Context.User.GetAvatarUrl())
                }
            };
            eb.AddField((efb) =>
            {
                efb.Name = $":thinking: Qual a função de `{pcmd}`?";
                efb.IsInline = true;
                efb.Value =                         //Para que serve o comando
                    "O bot mostra como é ter swag... \n";
            });
            eb.AddField((efb) =>
            {
                efb.Name = ":thumbsup: Como ultilizar";
                efb.IsInline = true;
                efb.Value =                         //Descrição do comando
                    $"Você deve digitar `{pcmd}`...";
            });
            eb.AddField((efb) =>
            {
                efb.Name = ":book:Exemplo de uso:";
                efb.IsInline = true;                //Exemplos
                efb.Value = $"`{pcmd}`\n";
            });
            await ReplyAsync(Context.User.Mention, false, eb);
        }

        //Beijar
        [Command("Beijar")]
        [Summary("Como Usar: Beijar.")]
        [RequireBotPermission(ChannelPermission.SendMessages)]
        public async Task _ComoUsarBeijar()
        {
            string _cmd = "Beijar";              //Comando
            string pcmd = Config.Bot.CmdPrefix + _cmd;

            var eb = new EmbedBuilder()
            {
                Title = "Como usar:",
                Color = new Color(4, 97, 247),
                ThumbnailUrl = (Context.Client.CurrentUser.GetAvatarUrl()),
                Footer = new EmbedFooterBuilder()
                {
                    Text = $"Solicitado por {Context.User.Username}#{Context.User.Discriminator}",
                    IconUrl = (Context.User.GetAvatarUrl())
                }
            };
            eb.AddField((efb) =>
            {
                efb.Name = $":thinking: Qual a função de `{pcmd}`?";
                efb.IsInline = true;
                efb.Value =                         //Para que serve o comando
                    "Envio um gif, dizendo que você beijou a pessoa mencionada. \n";
            });
            eb.AddField((efb) =>
            {
                efb.Name = ":thumbsup: Como ultilizar";
                efb.IsInline = true;
                efb.Value =                         //Descrição do comando
                    $"Você deve digitar `{pcmd} @usuário#0000`...";
            });
            eb.AddField((efb) =>
            {
                efb.Name = ":book:Exemplo de uso:";
                efb.IsInline = true;                //Exemplos
                efb.Value = $"`{pcmd} @{Context.User.Username}#{Context.User.Discriminator}`\n";
            });
            await ReplyAsync(Context.User.Mention, false, eb);
        }

        //Runa
        [Command("Runa")]
        [Summary("Como Usar: Runa.")]
        [RequireBotPermission(ChannelPermission.SendMessages)]
        public async Task _ComoUsarRuna()
        {
            string _cmd = "Runa";              //Comando
            string pcmd = Config.Bot.CmdPrefix + _cmd;

            var eb = new EmbedBuilder()
            {
                Title = "Como usar:",
                Color = new Color(4, 97, 247),
                ThumbnailUrl = (Context.Client.CurrentUser.GetAvatarUrl()),
                Footer = new EmbedFooterBuilder()
                {
                    Text = $"Solicitado por {Context.User.Username}#{Context.User.Discriminator}",
                    IconUrl = (Context.User.GetAvatarUrl())
                }
            };
            eb.AddField((efb) =>
            {
                efb.Name = $":thinking: Qual a função de `{pcmd}`?";
                efb.IsInline = true;
                efb.Value =                         //Para que serve o comando
                    "Envio para você as runas de determinado campeão. \n";
            });
            eb.AddField((efb) =>
            {
                efb.Name = ":thumbsup: Como ultilizar";
                efb.IsInline = true;
                efb.Value =                         //Descrição do comando
                    $"Você deve digitar `{pcmd} <Nome do campeão>`" +
                    "\nSem `'` como por exemplo no caso do `Cho'Gath`." +
                    "\nVocê também pode chamar por abreviações, como: Cho, ou então TK, por exemplo..." +
                    "\n\n**Maiúsculas e minúsculas não diferem**";
            });
            eb.AddField((efb) =>
            {
                efb.Name = ":book:Exemplo de uso:";
                efb.IsInline = true;                //Exemplos
                efb.Value = $"`{pcmd} Annie`\n" +
                            $"`{pcmd} MasterYi`\n" +
                            $"`{pcmd} twistedfate`\n" +
                            $"`{pcmd} kogmaw`\n";
            });
            await ReplyAsync(Context.User.Mention, false, eb);
        }

        //  Gato
        [Command("Gato")]
        [Summary("Como Usar: Gato.")]
        [RequireBotPermission(ChannelPermission.SendMessages)]
        public async Task _ComoUsarGato()
        {
            string _cmd = "Gato";              //Comando
            string pcmd = Config.Bot.CmdPrefix + _cmd;

            var eb = new EmbedBuilder()
            {
                Title = "Como usar:",
                Color = new Color(4, 97, 247),
                ThumbnailUrl = (Context.Client.CurrentUser.GetAvatarUrl()),
                Footer = new EmbedFooterBuilder()
                {
                    Text = $"Solicitado por {Context.User.Username}#{Context.User.Discriminator}",
                    IconUrl = (Context.User.GetAvatarUrl())
                }
            };
            eb.AddField((efb) =>
            {
                efb.Name = $":thinking: Qual a função de `{pcmd}`?";
                efb.IsInline = true;
                efb.Value =                         //Para que serve o comando
                    "Envio um gatinho para você. \n";
            });
            eb.AddField((efb) =>
            {
                efb.Name = ":thumbsup: Como ultilizar";
                efb.IsInline = true;
                efb.Value =                         //Descrição do comando
                    $"Você deve digitar `{pcmd}`";
            });
            await ReplyAsync(Context.User.Mention, false, eb);
        }

        //  Gif
        [Command("Gif")]
        [Summary("Como Usar: Gif.")]
        [RequireBotPermission(ChannelPermission.SendMessages)]
        public async Task _ComoUsarGif()
        {
            string _cmd = "Gif";              //Comando
            string pcmd = Config.Bot.CmdPrefix + _cmd;

            var eb = new EmbedBuilder()
            {
                Title = "Como usar:",
                Color = new Color(4, 97, 247),
                ThumbnailUrl = (Context.Client.CurrentUser.GetAvatarUrl()),
                Footer = new EmbedFooterBuilder()
                {
                    Text = $"Solicitado por {Context.User.Username}#{Context.User.Discriminator}",
                    IconUrl = (Context.User.GetAvatarUrl())
                }
            };
            eb.AddField((efb) =>
            {
                efb.Name = $":thinking: Qual a função de `{pcmd}`?";
                efb.IsInline = true;
                efb.Value =                         //Para que serve o comando
                    "Envio para você um gif baseado na tag que você digitar. \n";
            });
            eb.AddField((efb) =>
            {
                efb.Name = ":thumbsup: Como ultilizar";
                efb.IsInline = true;
                efb.Value =                         //Descrição do comando
                    $"Você deve digitar `{pcmd} <tag>`";
            });
            eb.AddField((efb) =>
            {
                efb.Name = ":book:Exemplo de uso:";
                efb.IsInline = true;                //Exemplos
                efb.Value = $"`{pcmd} Gato`\n" +
                            $"`{pcmd} Cachorro`\n" +
                            $"`{pcmd} Bolo`\n" +
                            $"`{pcmd} Rave`\n";
            });
            await ReplyAsync(Context.User.Mention, false, eb);
        }

    }
}