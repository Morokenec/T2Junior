using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using T2JuniorAPI.Models;

namespace T2JuniorAPI.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Organization> Organizations { get; set; }
        public DbSet<News> News { get; set; }
        public DbSet<Club> Clubs { get; set; }
        public DbSet<ClubRole> ClubRoles { get; set; }
        public DbSet<ClubUser> ClubUsers { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Event> Events { get; set; }
        public DbSet<EventDirection> EventDirections { get; set; }
        public DbSet<Note> Notes { get; set; }
        public DbSet<NoteStatus> NoteStatuses { get; set; }
        public DbSet<UserSubscribers> UserSubscribers { get; set; }
        public DbSet<Wall> Walls { get; set; }
        public DbSet<WallType> WallTypes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ClubUser>()
                .HasKey(cu => new { cu.IdClub, cu.IdUser, cu.IdRole });

            modelBuilder.Entity<ClubUser>()
                .HasOne(cu => cu.IdClubNavigation)
                .WithMany(c => c.ClubUsers)
                .HasForeignKey(cu => cu.IdClub);

            modelBuilder.Entity<ClubUser>()
                .HasOne(cu => cu.IdRoleNavigation)
                .WithMany(cr => cr.ClubUsers)
                .HasForeignKey(cu => cu.IdRole);

            modelBuilder.Entity<ClubUser>()
                .HasOne(cu => cu.IdUserNavigation)
                .WithMany(u => u.ClubUsers)
                .HasForeignKey(cu => cu.IdUser);

            // UserSubscribers
            modelBuilder.Entity<UserSubscribers>()
                .HasKey(us => new { us.IdUser, us.IdSubscriber });

            modelBuilder.Entity<UserSubscribers>()
                .HasOne(us => us.User)
                .WithMany(u => u.Subscribers)
                .HasForeignKey(us => us.IdUser)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<UserSubscribers>()
                .HasOne(us => us.Subscriber)
                .WithMany()
                .HasForeignKey(us => us.IdSubscriber)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Comment>()
                .HasOne(c => c.ParrentComment)
                .WithMany(c => c.InverseParrentComment)
                .HasForeignKey(c => c.ParrentCommentId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Note>()
                .HasOne(n => n.IdRepostNavigation)
                .WithMany(n => n.InverseIdRepostNavigation)
                .HasForeignKey(n => n.IdRepost)
                .OnDelete(DeleteBehavior.Restrict);

            // Wall
            modelBuilder.Entity<Wall>()
                .HasOne(w => w.Owner)
                .WithMany(u => u.Walls)
                .HasForeignKey(w => w.IdOwner)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Wall>()
                .HasOne(w => w.IdOwnerNavigation)
                .WithMany(c => c.Walls)
                .HasForeignKey(w => w.IdOwner)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}