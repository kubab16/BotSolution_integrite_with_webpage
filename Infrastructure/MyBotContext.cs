using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Text;

namespace Infrastructure
{
    /// <summary>
    /// Bot DataBase conection
    /// </summary>
    public class MyBotContext : DbContext
    {
        public DbSet<Server> Servers { get; set; }
        public DbSet<AutoRole> AutoRoles { get; set; }
        public DbSet<Comands> Comands { get; set; }
        public DbSet<Webhooks> Webhooks { get; set; }
        public DbSet<Punishment> Punishment { get; set; }
        public DbSet<ModerationRole> ModerationRoles { get; set; }
        public DbSet<PunishRole> punishRoles { get; set; }
        public DbSet<Language> Language { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            var configuration = new ConfigurationBuilder()
                        .SetBasePath(Directory.GetCurrentDirectory())
                        .AddJsonFile("DBConfig.json", false, true)
                        .Build();
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
        public ulong Id { get; set; }
        public string Prefix { get; set; }      
        public ulong Logs { get; set; }
        public ulong ChannelId { get; set; }
        public string Welcome { get; set; }
        public string Goodbye { get; set; }
        public string Banned { get; set; }
        public string Background { get; set; }
        public Language Langue { get; set; }
    }
    public class Language
    {
        public int id { get; set; }
        public string name { get; set; }
        public string File { get; set; }
    }
    public class AutoRole
    {
        public int Id { get; set; }
        public ulong RoleId { get; set; }
        public ulong ServerId { get; set; }
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
        public ulong id { get; set;}
        public ulong RoleId { set; get; }
        [DefaultValue(0)]
        public Int32 PermisionLevel { set; get; }                                         
    }
    public class PunishRole
    {      
        public ulong Id { get; set; }
        public ulong Vmute { get; set; }
        public ulong Tmute { get; set; }
    }
}
