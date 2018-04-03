using System;
using Discord;
using Discord.Commands;
using Discord.WebSocket;
using DPP_Bot.Core.Configs;
using DPP_Bot.Core.DB;

namespace DPP_Bot.Core.Services
{
    public static class EmbedHandler
    {
        private static readonly DbMisc DbMisc = new DbMisc();
        public static Embed CriarEmbed(string title, string body, EmbedMessageType type, bool withTimeStamp = false)
        {
            var embed = new EmbedBuilder();
            embed.WithTitle(title);
            embed.WithDescription(body);

            switch (type)
            {
                case EmbedMessageType.Info:
                    embed.WithColor(new Color(52, 152, 219));
                    break;
                case EmbedMessageType.Success:
                    embed.WithColor(new Color(22, 160, 133));
                    break;
                case EmbedMessageType.Error:
                    embed.WithColor(new Color(192, 57, 43));
                    break;
                case EmbedMessageType.Exception:
                    embed.WithColor(new Color(230, 126, 34));
                    break;
                default:
                    embed.WithColor(new Color(178, 178, 178));
                    break;
            }

            if (withTimeStamp)
            {
                embed.WithCurrentTimestamp();
            }

            return embed;
        }
        public enum EmbedMessageType
        {
            Success = 0,
            Info = 10,
            Error = 20,
            Exception = 30
        }

        public static Embed CmdLogEmbed(SocketCommandContext context, SocketMessage s)
        {
            var embed = new EmbedBuilder();
            var comando = s.ToString().Remove(0, Config.Bot.CmdPrefix.Length).ToLower();

            embed.WithTitle("📟 Comando");
            embed.WithColor(new Color(65, 244, 118));
            embed.WithDescription($"Em: <#{context.Channel.Id}>\n" +
                                  $"Por: {context.User.Mention}\n" +
                                  $"```{comando}```");
            embed.WithFooter($"ID:{context.User.Id}", context.User.GetAvatarUrl());
            embed.WithCurrentTimestamp();
            return embed;
        }
        public static Embed BoasVindasEmbed(SocketGuildUser user)
        {
            var embed = new EmbedBuilder();
            var rand = new Random();

            embed.WithTitle(Config.Bot.WcEmbTitle);
            embed.WithColor(new Color(rand.Next(0, 255), rand.Next(0, 255), rand.Next(0, 255)));
            embed.WithThumbnailUrl(user.GetAvatarUrl());
            embed.WithImageUrl(Config.Bot.WcEmbImgUrl);
            embed.WithFooter($"ID: {user.Id}", user.GetAvatarUrl());
            embed.WithCurrentTimestamp();
            embed.WithDescription(Config.Bot.WcEmbDescription.Replace("{user}", user.Mention));
            
            return embed;
        }

        public static Embed BoasVindasLogEmbed(SocketGuildUser user)
        {
            var embed = new EmbedBuilder();
            embed.WithTitle("👉 Entrou");
            embed.WithColor(new Color(66, 176, 244));
            embed.WithThumbnailUrl(user.GetAvatarUrl());
            embed.WithDescription($"User: {user.Mention}\n" +
                                  $"ID: {user.Id}");
            embed.WithFooter($"ID: {user.Id}", user.GetAvatarUrl());
            embed.WithCurrentTimestamp();
            return embed;
        }

        public static Embed PvBoasVindasEmbed(SocketGuildUser user)
        {
            var embed = new EmbedBuilder();

            embed.WithTitle(Config.Bot.PvWcEmbTitle);
            embed.WithColor(new Color(244, 65, 65));
            embed.WithDescription(Config.Bot.PvWcEmbDescription.Replace("{user}",user.Mention));
            embed.WithFooter(Config.Bot.PvWcEmbFooter);
            embed.AddField(Config.Bot.PvWcField1Title, Config.Bot.PvWcField1Description);
            embed.AddField(Config.Bot.PvWcField2Title, Config.Bot.PvWcField2Description);
            embed.AddField(Config.Bot.PvWcField3Title, Config.Bot.PvWcField3Description);
            return embed;
        }

        public static Embed LogEmbed(string cmd, SocketUser adm, IGuildUser user, string razão = "Razão não específica")
        {
            var embed = new EmbedBuilder();

            switch (cmd.ToLower().Trim())
            {
                case "mute":
                    embed.WithTitle("🙊 Mute");
                    embed.WithColor(new Color(103, 106, 112));
                    embed.WithDescription($"Usuário: {user.Mention}\n" +
                                          $"ID: {user.Id}\n" +
                                          $"Por: {adm.Mention}");
                    break;
                case "unmute":
                    embed.WithTitle("🐵 Desmute");
                    embed.WithColor(new Color(237, 237, 237));
                    embed.WithDescription($"Usuário: {user.Mention}\n" +
                                          $"ID: {user.Id}\n" +
                                          $"Por: {adm.Mention}");
                    break;
                case "kick":
                    embed.WithTitle("👈🏃 Kick");
                    embed.WithColor(new Color(255, 246, 0));
                    embed.WithDescription($"Usuário: {user.Mention}\n" +
                                          $"ID: {user.Id}\n" +
                                          $"Por:{adm.Mention}\n" +
                                          $"Razão```{razão}```");
                    break;
                case "ban":
                    embed.WithTitle("🚫 Ban");
                    embed.WithColor(new Color(244, 65, 65));
                    embed.WithDescription($"Usuário: {user.Mention}\n" +
                                          $"ID: {user.Id}\n" +
                                          $"Por: {adm.Mention}\n" +
                                          $"Razão```{razão}```");
                    break;
                case "banwarn":
                    embed.WithTitle("⚠️ Ban Warn");
                    embed.WithColor(new Color(244, 65, 65));
                    embed.WithDescription($"Usuário: {user.Mention}\n" +
                                          $"ID: {user.Id}\n" +
                                          $"Por: {adm.Mention}\n" +
                                          $"Razão```{razão}```");
                    break;
                case "warn":
                    embed.WithTitle("⚠️ Warn");
                    embed.WithColor(new Color(244, 166, 65));
                    embed.WithDescription($"Usuário: {user.Mention}\n" +
                                          $"ID: {user.Id}\n" +
                                          $"Por: {adm.Mention}");
                    break;
                case "tirarwarns":
                    embed.WithTitle("♻️ Warns Removidos");
                    embed.WithColor(new Color(255, 234, 173));
                    embed.WithDescription($"Usuário: {user.Mention}\n" +
                                          $"ID: {user.Id}\n" +
                                          $"Por: {adm.Mention}");
                    break;
            }

            embed.WithThumbnailUrl(user.GetAvatarUrl());
            embed.WithFooter("", adm.GetAvatarUrl());
            embed.WithCurrentTimestamp();
            return embed;
        }

        public static Embed BanEmbed(SocketUser adm, IGuildUser user, string razão, bool brincadeira)
        {
            var embed = new EmbedBuilder();
            var r = new Random();
            var gifselecionado = DbMisc.GifsBans[r.Next(0, DbMisc.GifsBans.Length)];

            if (brincadeira)
            {   //  Falso Ban (Executado por usuário)
                embed.WithTitle("🚫 Banido");
                embed.WithColor(new Color(66, 134, 244));
                embed.WithDescription($"{adm.Mention}, quer que {user.Mention} seja banido\n" +
                                      "Razão:\n" +
                                      $"```{razão}```");
            }
            else
            {   //  Ban verdadeiro (Executado por ADM)
                embed.WithTitle("🚫 Banido");
                embed.WithColor(new Color(244, 66, 66));
                embed.WithDescription($"O usuário: {user.Mention}\n" +
                                      "Razão:\n" +
                                      $"```{razão}```");
            }

            //Dados padrões
            embed.WithThumbnailUrl(user.GetAvatarUrl());
            embed.WithImageUrl(gifselecionado);
            embed.WithFooter(adm.Username, adm.GetAvatarUrl());
            embed.WithCurrentTimestamp();
            return embed;
        }

        public static Embed PvBanEmbed(SocketUser adm, IGuildUser user, string razão)
        {
            var embed = new EmbedBuilder();

            embed.WithTitle("🚫 Você foi banido!");
            embed.WithColor(new Color(244, 66, 66));
            embed.WithDescription(
                $"Olá {user.Username}, sinto em lhe informar, que você foi banido de nosso servidor.");
            embed.AddField("Banido por:", $"``{adm.Username}#{adm.Discriminator}``");
            embed.AddField("Motivo do Ban:", $"```{razão}```");
            embed.WithThumbnailUrl(user.GetAvatarUrl());
            embed.WithFooter(adm.Username, adm.GetAvatarUrl());
            embed.WithCurrentTimestamp();

            return embed;
        }

        public static Embed WarnLogEmbed(SocketUser adm, IGuildUser user,Int32 warns, string razão)
        {
            var embed = new EmbedBuilder();

            return embed;
        }
    }
}
