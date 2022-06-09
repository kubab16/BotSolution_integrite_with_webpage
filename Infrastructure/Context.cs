using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.IO;
using Infrastructure.Common;

namespace Infrastructure
{
    /// <summary>
    /// Bot DataBase conection
    /// </summary>
    public class Context : DbContext
    {
        public DbSet<Comands> Comands { get; set; }
        public DbSet<ConectionSeries> ConectionSeries { get; set; }
        public DbSet<Episode> Episodes { get; set; }
        public DbSet<EpisodeComments> EpisodeComments { get; set; }
        public DbSet<Language> Language { get; set; }
        public DbSet<ModerationRole> ModerationRoles { get; set; }
        public DbSet<PermisionGlobal> PermisionGlobals { get; set; }
        public DbSet<Player> Players { get; set; }
        public DbSet<Punishment> Punishment { get; set; }
        public DbSet<Series> Series { get; set; }
        public DbSet<SeriesComments> SeriesComments { get; set; }
        public DbSet<SeriesTag> SeriesTags { get; set; }
        public DbSet<Server> Servers { get; set; }
        public DbSet<Tags> Tags { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<WathedEpisode> WathedEpisode { get; set; }
        public DbSet<Webhooks> Webhooks { get; set; }
        public DbSet<TrustUser> TrustUsers { get; set; }
        public DbSet<BadWord> BadWords { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {

            var configuration = new ConfigurationBuilder()
                        .SetBasePath(Directory.GetCurrentDirectory())
                        .AddJsonFile("DBConfig.json", false, true)
                        .Build();
            if (configuration["host"] == null) configuration["host"] = "localhost";
            if (configuration["user"] == null) configuration["user"] = "root";
            if (configuration["password"] == null) configuration["password"] = "";
            if (configuration["port"] == null) configuration["port"] = "3306";
            if (configuration["Timeout"] == null) configuration["Timeout"] = "5";

            var connectionString = $"server={configuration["host"]};" +
                $"database={configuration["database"]};" +
                $"user={configuration["user"]};" +
                $"password={configuration["password"]};" +
                $"port={configuration["port"]};" +
                $"Connect Timeout={configuration["Connect Timeout"]};";
            options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
        }
    }
    /// <summary>
    /// Information abot serwer (prefix, logs, id)
    /// </summary>
    public class Server
    {
        [Key]
        public ulong GuildId { get; set; }
        public string Prefix { get; set; }
        public ulong Logs { get; set; }
        public ulong ChannelId { get; set; }
        public string Welcome { get; set; }
        public string Goodbye { get; set; }
        public string Banned { get; set; }
        public byte[] Background { get; set; }
        [DefaultValue(null)]
        public ulong Vmute { get; set; }
        [DefaultValue(null)]
        public ulong Tmute { get; set; }
        [DefaultValue(null)]
        public ulong AutoRoleId { get; set; }

        public Language Langue { get; set; }
        public Comands Comands { get; set; }
        public List<Webhooks> Webhooks { get; set; }
        public List<Punishment> Punishments { get; set; }
        public List<ModerationRole> ModerationRoles { get; set; }
    }
    /// <summary>
    /// Langue list!
    /// </summary>
    public class Language
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string File { get; set; }
    }
    /// <summary>
    /// Information about turn on/off comend section
    /// </summary>
    public class Comands
    {
        public ulong Id { get; set; }
        [DefaultValue(true)]
        public bool Moderation { get; set; }
        [DefaultValue(true)]
        public bool Miusic { get; set; }
        [DefaultValue(true)]
        public bool Information { get; set; }
        [DefaultValue(true)]
        public bool Funny { get; set; }
    }
    /// <summary>
    /// List of all bot webhooks
    ///  id - Id webhook
    ///  IdServer - Guid id
    ///  name - webhook's name 
    ///  Token - Webhook's token
    /// </summary>
    public class Webhooks
    {
        public ulong Id { get; set; }
        public string Name { get; set; }
        public string Token { get; set; }
        public ulong ChannelId { get; set; }

        public ulong IdServer { get; set; }
        public Server Server { get; set; }
    }
    public class Punishment
    {
        public int Id { get; set; }
        public ulong UserId { get; set; }
        public string TypeOfPunishment { get; set; }
        public DateTime? EndDate { get; set; }
        public string Reason { get; set; }
        public ulong ModeratorId { get; set; }
        public bool Finished { get; set; }

        public ulong GuidId { get; set; }
        public Server Server { get; set; }
    }
    public class ModerationRole
    {
        [Key]
        public ulong ModerationRoleId { get; set; }
        public ulong RoleId { set; get; }

        [DefaultValue(false)] // Moderation
        public bool Modration { get; set; }
        [DefaultValue(false)]
        public bool Vmute { get; set; }
        [DefaultValue(false)]
        public bool Tmute { get; set; }
        [DefaultValue(false)]
        public bool Ban { get; set; }
        [DefaultValue(false)]
        public bool Warn { get; set; }
        [DefaultValue(false)]
        public bool Kick { get; set; }

        [DefaultValue(false)]
        public bool Information { get; set; }
        [DefaultValue(true)]
        public bool Ping { get; set; }
        [DefaultValue(false)]
        public bool User { get; set; }
        [DefaultValue(false)]
        public bool ServerCmd { get; set; }
        [DefaultValue(false)]
        public bool Avatar { get; set; }

        [DefaultValue(true)]
        public bool Funny { get; set; }

        [DefaultValue(false)]
        public bool Configuration { get; set; }

        public ulong GuildID { get; set; }
        public Server Server { get; set; }

        public void SetSection(string section, bool value)
        {
            switch (section)
            {
                case "Modration":
                    Modration = value;
                    break;
                case "Vmute":
                    Vmute = value;
                    break;
                case "Tmute":
                    Tmute = value;
                    break;
                case "Ban":
                    Ban = value;
                    break;
                case "Warn":
                    Warn = value;
                    break;
                case "Kick":
                    Kick = value;
                    break;
                case "Information":
                    Information = value;
                    break;
                case "ping":
                    Ping = value;
                    break;
                case "user":
                    User = value;
                    break;
                case "server":
                    ServerCmd = value;
                    break;
                case "Avatar":
                    Avatar = value;
                    break;
                case "Funny":
                    Funny = value;
                    break;
            }
        }
    }
    public class Series
    {
        [Key]
        [DisplayName("Series ID")]
        public ulong SeriesId { get; set; }
        [DisplayName("Series image")]
        public byte[] Image { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime? Start { get; set; }
        public DateTime? End { get; set; }
        public double EpisodeLenght { get; set; }

        public List<SeriesRating> SeriesRatings { get; set; }
        public List<SeriesTag> Tags { get; set; }
        public List<SeriesComments> SeriesComments { get; set; }
        public List<Episode> Episodes { get; set; }
    }

    public class TrustUser
    {
        [Key]
        public ulong Id { get; set; }
        public ulong UserId { get; set; }
        public ulong GuildId { get; set; }
        public ulong TrustValue { get; set; }
        [DefaultValue(0)]
        public ulong MessageCount { get; set; }
        [DefaultValue(0)]
        public ulong OddMessageCount { get; set; }
        [DefaultValue(0)]
        public ulong ReactionCount { get; set; }
    }
    public class SeriesRating
    {
        [Key]
        public ulong Id { get; set; }
        [DisplayName("User's ID")]
        public ulong UserId { get; set; }
        public double Raiting { get; set; }
        public double Graphics { get; set; }
        public double Story { get; set; }
        public double Characters { get; set; }
        public double Music { get; set; }

        [DisplayName("Series' ID")]
        public ulong SeriesId { get; set; }
        public Series Series { get; set; }
    }
    public class SeriesTag
    {
        [Key]
        public uint Id { get; set; }

        public Tags Tag { get; set; }
        public string TagId { get; set; }

        public int SeriesId { get; set; }
        public Series Series { get; set; }
    }
    public class Tags
    {
        [Key]
        public string TagId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
    public class Player
    {
        [Key]
        public ulong Id { get; set; }
        public string Name { get; set; }
        public string Link { get; set; }

        [DisplayName("Episode ID")]
        public ulong EpisodeID { get; set; }
        public Episode Episodes { get; set; }

        [DisplayName("User ID")]
        public ulong UserId { get; set; }
        public User User { get; set; }
    }
    public class Episode
    {
        [Key]
        [DisplayName("Episode ID")]
        public ulong Id { get; set; }
        public int Number { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime PublishDate { get; set; }

        [DisplayName("Series' ID")]
        public ulong SeriesId { get; set; }
        public Series Series { get; set; }

        public List<Player> players { get; set; }
    }
    public class SeriesComments
    {
        [DisplayName("Coments ID")]
        public ulong Id { get; set; }
        [DisplayName("Coments text")]
        public string Comment { get; set; }
        [DisplayName("User ID")]
        public ulong UserId { get; set; }
        public User User { get; set; }
    }
    public class EpisodeComments
    {
        [DisplayName("Coments ID")]
        public ulong Id { get; set; }
        [DisplayName("Coments text")]
        public string Comment { get; set; }
        [DisplayName("Like")]
        public int Positive { get; set; }
        [DisplayName("Unlike")]
        public int Negative { get; set; }

        [DisplayName("User ID")]
        public ulong UserId { get; set; }
        public User User { get; set; }
    }
    public class User
    {
        [DisplayName("User ID")]
        public ulong Id { get; set; }
        [DisplayName("User's account discord ID")]
        public ulong DiscordAccountId { get; set; }
        [DisplayName("User's avatar")]
        public byte[] Avatar { get; set; }
        [DisplayName("User's hashed password")]
        public string HashedPassord { get; set; }
        public string Mail { get; set; }
        public string Name { get; set; }
        public DateTime BrightDate { get; set; }
        public bool Adult { get; set; }
        [DefaultValue(null)]
        public ulong Group { get; set; }

        public PermisionGlobal Permision { get; set; }
    }
    public class PermisionGlobal
    {
        [Key]
        public ulong PermisionGlobalId { get; set; }
        public bool Admin { get; set; }
        public bool Modelator { get; set; }
        [DisplayName("Edit series")]
        public bool EditSeries { get; set; }
        [DisplayName("Edit player")]
        public bool EditPlayer { get; set; }
        [DisplayName("Add coments")]
        public bool Coments { get; set; }
    }
    public class WathedEpisode
    {
        [DisplayName("Wath episode Id")]
        public int Id { get; set; }

        [DisplayName("User iD")]
        public ulong UserId { get; set; }
        public User User { get; set; }

        [DisplayName("Series Id")]

        public Series Series { get; set; }
    }
    public class ConectionSeries
    {
        public ulong Id { get; set; }
        [DisplayName("First series connection name")]
        public string TypeConnecionFirst { get; set; }
        [DisplayName("First series ID")]
        public ulong FirstSeries { get; set; }
        [DisplayName("Second series connection name")]
        public string TypeConnecionSecond { get; set; }
        [DisplayName("Second series ID")]
        public ulong SecondSeries { get; set; }
    }

    public class BadWord
    {
        [Key]
        public ulong Id { get; set; }
        public string BadIssue { get; set; }
    }
}