using Microsoft.EntityFrameworkCore;
using T2WebSoket.Models;

namespace T2WebSoket.Repositories
{
    public class ChatDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Chat> Chats { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<UsersChats> UsersChats { get; set; }
        public DbSet<ChatType> ChatTypes { get; set; }
        public DbSet<MessageFile> MessageFiles { get; set; }
        public DbSet<ChatFile> Files { get; set; }

        public ChatDbContext(DbContextOptions<ChatDbContext> options) : base(options) 
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Chat>()
                .HasOne(c => c.ChatType)
                .WithMany(ct => ct.Chats)
                .HasForeignKey(c => c.TypeId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<MessageFile>()
                .HasOne(mf => mf.Message)
                .WithMany(m => m.MessageFiles)
                .HasForeignKey(mf => mf.IdMessage)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<MessageFile>()
                .HasOne(mf => mf.File)
                .WithMany(cf => cf.MessageFiles)
                .HasForeignKey(mf => mf.IdFile)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<ChatFile>()
                .HasOne(cf => cf.User)
                .WithMany(u => u.ChatFiles)
                .HasForeignKey(cf => cf.UserId)
                .OnDelete(DeleteBehavior.Restrict);
        }

        private static void ConfigureCommonFields(ModelBuilder modelBuilder)
        {
            foreach (var entryType in modelBuilder.Model.GetEntityTypes().Where(t => t.ClrType.IsSubclassOf(typeof(BaseCommonProperties))))
            {
                modelBuilder.Entity(entryType.Name, x =>
                {
                    x.HasKey("Id");
                    x.Property("Id").HasDefaultValueSql("gen_random_uuid()");
                    x.Property("CreationDate").HasDefaultValueSql("now()");
                    x.Property("UpdateDate").HasDefaultValueSql("now()");
                    x.Property("IsDelete").HasDefaultValue(false);
                });
            }
        }
    }
}
