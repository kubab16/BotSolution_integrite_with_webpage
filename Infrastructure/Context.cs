using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.IO;

namespace Infrastructure
{
    /// <summary>
    /// Bot DataBase conection
    /// </summary>
    public class Context :  DbContext 
    {

        public DbSet<Server> Servers { get; set; }
        public DbSet<Comands> Comands { get; set; }
        public DbSet<Webhooks> Webhooks { get; set; }
        public DbSet<Punishment> Punishment { get; set; }
        public DbSet<ModerationRole> ModerationRoles { get; set; }
        public DbSet<Language> Language { get; set; }
        public DbSet<SeriesRating> Rating { get; set; }
        public DbSet<Series> series { get; set; }
        public DbSet<player> players { get; set; }
        public DbSet<Episode> episodes { get; set; }
        public DbSet<EPisodeComments> Ecomments { get; set; }
        public DbSet<SeriesComments> Scomments { get; set; }
        public DbSet<User> users { get; set; }
        public DbSet<WathedEpisode> wathedSeries { get; set; }


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
        public Language Langue { get; set; }
        [DefaultValue(null)]
        public ulong Vmute { get; set; }
        [DefaultValue(null)]
        public ulong Tmute { get; set; }
        [DefaultValue(null)]
        public ulong AutoRoleId { get; set; }
    }
    /// <summary>
    /// Langue list!
    /// </summary>
    public class Language
    {
        public int id { get; set; }
        public string name { get; set; }
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
        public ulong id { get; set; }
        public ulong IdServer { get; set; }
        public string name { get; set; }
        public string Token { get; set; }
    }
    public class Punishment
    {
        public int id { get; set; }
        public ulong GuidId { get; set; }
        public ulong UserId { get; set; }
        public string TypeOfPunishment { get; set; }
        public DateTime? EndDate { get; set; }
        public string Reason { get; set; }
        public ulong ModeratorId { get; set; }
        public bool Finished { get; set; }
    }
    public class ModerationRole
    {
        [Key]
        public ulong GuildID { get; set; }
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
        public bool ping { get; set; }
        [DefaultValue(false)]
        public bool user { get; set; }
        [DefaultValue(false)]
        public bool server { get; set; }
        [DefaultValue(false)]
        public bool Avatar { get; set; }

        [DefaultValue(true)]
        public bool Funny { get; set; }

        [DefaultValue(false)]
        public bool Configuration { get; set; }

        public void SetSection(string section, bool value)
        {
            switch (section)
            {
                case "Modration":
                    this.Modration = value;
                    break;
                case "Vmute":
                    this.Vmute = value;
                    break;
                case "Tmute":
                    this.Tmute = value;
                    break;
                case "Ban":
                    this.Ban = value;
                    break;
                case "Warn":
                    this.Warn = value;
                    break;
                case "Kick":
                    this.Kick = value;
                    break;
                case "Information":
                    this.Information = value;
                    break;
                case "ping":
                    this.ping = value;
                    break;
                case "user":
                    this.user = value;
                    break;
                case "server":
                    this.server = value;
                    break;
                case "Avatar":
                    this.Avatar = value;
                    break;
                case "Funny":
                    this.Funny = value;
                    break;
            }
        }
    }
    public class Series
    {
        [Key]
        [DisplayName("Series ID")]
        public ulong id { get; set; }
        [DisplayName("Series image")]
        public byte[] image { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public string tags { get; set; }
    }
    public class SeriesRating
    {
        [Key]
        public ulong id { get; set; }
        [DisplayName("Series' ID")]
        public ulong seriesId { get; set; }
        [DisplayName("User's ID")]
        public ulong userId { get; set; }
        public double Raiting { get; set; }
        public double Graphics { get; set; }
        public double Story { get; set; }
        public double Characters { get; set; }
        public double Music { get; set; }
    }
    public class player
    {
        [Key]
        public ulong id { get; set; }
        [DisplayName("Episode ID")]
        public ulong EpisodeID { get; set; }
        [DisplayName("User's name")]
        public ulong UserId { get; set; }
        public string name { get; set; }
        public string link { get; set; }
    }
    public class Episode
    {
        [Key]
        [DisplayName("Episode ID")]
        public ulong id { get; set; }
        public int Number { get; set; }
        public string name { get; set; }
        public ulong SeriesId { get; set; }
    }
    public class SeriesComments
    {
        [DisplayName("Coments ID")]
        public ulong id { get; set; }
        [DisplayName("User ID")]
        public ulong userId { get; set; }
        [DisplayName("Coments text")]
        public string comment { get; set; }
        [DisplayName("Like")]
        public int positive { get; set; }
        [DisplayName("Unlike")]
        public int negative { get; set; }
    }
    public class EPisodeComments
    {
        [DisplayName("Coments ID")]
        public ulong id { get; set; }
        [DisplayName("User ID")]
        public ulong userId { get; set; }
        [DisplayName("Coments text")]
        public string comment { get; set; }
        [DisplayName("Like")]
        public int positive { get; set; }
        [DisplayName("Unlike")]
        public int negative { get; set; }
    }
    public class User 
    {
        [DisplayName("User ID")]
        public ulong id { get; set; }      
        [DisplayName("User's accout discord ID")]
        public ulong DiscordAccountId { get; set; }
        [DisplayName("User's avatara")]
        public byte[] avatar { get; set; }
        [DisplayName("User's hashed passwoed")]
        public string HashedPassord { get; set; }       
        public string mail { get; set; }
        public string name { get; set; }
        public DateTime BrightDate { get; set; }
        public bool adult { get; set; }
        [DefaultValue(null)]
        public ulong Group { get; set; }
        public permisionGlobal permision { get; set; }
    }
    public class permisionGlobal
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
        public int id { get; set; }
        [DisplayName("User iD")]
        public int UserId { get; set; }
        [DisplayName("Series Id")]
        public int SeriesId { get; set; }
    }
    public class conectionSeries
    {
        public ulong id { get; set; }
        [DisplayName("First series connection name")]
        public string TypeConnecionFirst { get; set; }
        [DisplayName("First series ID")]
        public ulong FirstSeries { get; set; }
        [DisplayName("Second series connection name")]
        public string TypeConnecionSecond { get; set; }
        [DisplayName("Second series ID")]
        public ulong SecondSeries { get; set; }
    }
}