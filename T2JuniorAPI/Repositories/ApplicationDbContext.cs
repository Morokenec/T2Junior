﻿using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using T2JuniorAPI.Entities;

namespace T2JuniorAPI.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, IdentityRole<Guid>, Guid>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Organization> Organizations { get; set; }
        public DbSet<Club> Clubs { get; set; }
        public DbSet<ClubRole> ClubRoles { get; set; }
        public DbSet<ClubUser> ClubUsers { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<CommentLike> CommentLikes { get; set; }
        public DbSet<Event> Events { get; set; }
        public DbSet<EventDirection> EventDirections { get; set; }
        public DbSet<Note> Notes { get; set; }
        public DbSet<NoteStatus> NoteStatuses { get; set; }
        public DbSet<UserSubscribers> UserSubscribers { get; set; }
        public DbSet<Wall> Walls { get; set; }
        public DbSet<WallType> WallTypes { get; set; }
        public DbSet<Mediafile> Mediafiles { get; set; }
        public DbSet<MediaType> MediaTypes { get; set; }
        public DbSet<MediaComment> MediaComments { get; set; }
        public DbSet<MediaNote> MediaNotes { get; set; }
        public DbSet<MediaClub> MediaClubs { get; set; }
        public DbSet<MediaEvent> MediaEvents { get; set; }
        public DbSet<Achievement> Achievements { get; set; }
        public DbSet<UserAchievement> UserAchievements { get; set; }
        public DbSet<UserAvatar> UserAvatars { get; set; }
        public DbSet<NoteLike> NoteLikes { get; set; }
        public DbSet<Vote> Votes { get; set; }
        public DbSet<UserInitiative> UserInitiatives { get; set; }
        public DbSet<MediaInitiative> MediaInitiatives { get; set; }
        public DbSet<InitiativeStatus> InitiativeStatuses { get; set; }
        public DbSet<InitiativeComment> InitiativeComments { get; set; }
        public DbSet<Initiative> Initiatives { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Vote>()
                .HasOne(v => v.Initiative)
                .WithMany(i => i.Votes)
                .HasForeignKey(v => v.IdInitiative)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Vote>()
                .HasOne(v => v.User)
                .WithMany(u => u.Votes)
                .HasForeignKey(v => v.IdUser)
                .OnDelete(DeleteBehavior.Restrict);


            modelBuilder.Entity<Initiative>()
                .HasOne(i => i.Status)
                .WithMany(s => s.Initiatives)
                .HasForeignKey(i => i.IdStatus)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<InitiativeComment>()
                .HasOne(ic => ic.Initiative)
                .WithMany(i => i.InitiativeComments)
                .HasForeignKey(ic => ic.IdInitiative)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<InitiativeComment>()
                .HasOne(ic => ic.User)
                .WithMany(u => u.InitiativeComments)
                .HasForeignKey(ic => ic.IdUser)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<MediaInitiative>()
                .HasOne(mi => mi.Initiative)
                .WithMany(i => i.MediaInitiatives)
                .HasForeignKey(mi => mi.IdInitiative)
                .OnDelete(DeleteBehavior.Restrict);
            
            modelBuilder.Entity<MediaInitiative>()
                .HasOne(mi => mi.Mediafile)
                .WithOne(mf => mf.MediaInitiative)
                .HasForeignKey<MediaInitiative>(mi => mi.IdMedia)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<UserInitiative>()
                .HasOne(ui => ui.User)
                .WithMany(u => u.UserInitiatives)
                .HasForeignKey(ui => ui.IdUser)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<UserInitiative>()
                .HasOne(ui => ui.Initiative)
                .WithMany(i => i.UserInitiatives)
                .HasForeignKey(ui => ui.IdInitiative)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<ClubUser>()
                .HasKey(cu => new { cu.IdClub, cu.IdUser, cu.IdRole });

            modelBuilder.Entity<ApplicationUser>()
                .Property(a => a.IsDeleted)
                .HasDefaultValue(false);

            modelBuilder.Entity<MediaClub>()
                .Property(mc => mc.IsAvatar)
                .HasDefaultValue(false);

            modelBuilder.Entity<ClubUser>()
                .HasOne(cu => cu.IdClubNavigation)
                .WithMany(c => c.ClubUsers)
                .HasForeignKey(cu => cu.IdClub)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<ClubUser>()
                .HasOne(cu => cu.IdRoleNavigation)
                .WithMany(cr => cr.ClubUsers)
                .HasForeignKey(cu => cu.IdRole)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<ClubUser>()
                .HasOne(cu => cu.IdUserNavigation)
                .WithMany(u => u.ClubUsers)
                .HasForeignKey(cu => cu.IdUser)
                .OnDelete(DeleteBehavior.Restrict);

            // UserSubscribers
            modelBuilder.Entity<UserSubscribers>()
                .HasKey(us => new { us.IdUser, us.IdSubscriber });

            modelBuilder.Entity<UserSubscribers>()
                .HasOne(us => us.User)
                .WithMany(u => u.SubscribersAsUser)
                .HasForeignKey(us => us.IdUser)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<UserSubscribers>()
                .HasOne(us => us.Subscriber)
                .WithMany(u => u.SubscribersAsSubscriber)
                .HasForeignKey(us => us.IdSubscriber)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Comment>()
                .HasOne(c => c.ParrentComment)
                .WithMany(c => c.InverseParrentComment)
                .HasForeignKey(c => c.ParrentCommentId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Comment>()
                .HasOne(c => c.IdUserNavigation)
                .WithMany(u => u.Comments)
                .HasForeignKey(c => c.IdUser)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Comment>()
                .HasOne(c => c.IdNoteNavigation)
                .WithMany(n => n.Comments)
                .HasForeignKey(c => c.IdNote)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<CommentLike>()
                .HasOne(cl => cl.Comment)
                .WithMany(c => c.CommentLikes)
                .HasForeignKey(cl => cl.CommentId)
                .OnDelete(DeleteBehavior.Restrict);
            
            modelBuilder.Entity<CommentLike>()
                .HasOne(cl => cl.User)
                .WithMany(u => u.CommentLikes)
                .HasForeignKey(cl => cl.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Note>()
                .HasOne(n => n.IdRepostNavigation)
                .WithMany(n => n.InverseIdRepostNavigation)
                .HasForeignKey(n => n.IdRepost)
                .OnDelete(DeleteBehavior.Restrict)
                .IsRequired(false);

            modelBuilder.Entity<Note>()
                .HasOne(n => n.IdWallNavigation)
                .WithMany(w => w.Notes)
                .HasForeignKey(n => n.IdWall)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Note>()
                .HasOne(n => n.IdStatusNavigation)
                .WithMany(ns => ns.Notes)
                .HasForeignKey(n => n.IdStatus)
                .OnDelete(DeleteBehavior.Restrict);

            // Wall

            modelBuilder.Entity<Wall>()
                .HasOne(w => w.IdTypeNavigation)
                .WithMany(wt => wt.Walls)
                .HasForeignKey(w => w.IdType)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Wall>()
                .HasOne(w => w.UserOwner)
                .WithMany(a => a.Walls)
                .HasForeignKey(w => w.IdUserOwner)
                .OnDelete(DeleteBehavior.Restrict)
                .IsRequired(false);

            modelBuilder.Entity<Wall>()
                .HasOne(w => w.ClubOwner)
                .WithMany(c => c.Walls)
                .HasForeignKey(w => w.IdClubOwner)
                .OnDelete(DeleteBehavior.Restrict)
                .IsRequired(false);

            modelBuilder.Entity<Event>()
                .HasOne(e => e.IdClubNavigation)
                .WithMany()
                .HasForeignKey(e => e.IdClub)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Event>()
                .HasOne(e => e.IdDirectionNavigation)
                .WithMany(d => d.Events)
                .HasForeignKey(e => e.IdDirection)
                .OnDelete(DeleteBehavior.Restrict);

            // MediaFiles
            modelBuilder.Entity<Mediafile>()
                .HasOne(mf => mf.IdUserNavigation)
                .WithMany(u => u.Mediafiles)
                .HasForeignKey(mf => mf.IdUser)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Mediafile>()
                .HasOne(mf => mf.IdMediaTypesNavigation)
                .WithMany(mt => mt.Mediafiles)
                .HasForeignKey(mf => mf.IdType)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<MediaComment>()
                .HasOne(mc => mc.IdMediaNavigation)
                .WithOne(mf => mf.MediaComment)
                .HasForeignKey<MediaComment>(mc => mc.IdMedia)
                .OnDelete(DeleteBehavior.Restrict);
            
            modelBuilder.Entity<MediaComment>()
                .HasOne(mc => mc.IdCommentNavigation)
                .WithMany(c => c.MediaComments)
                .HasForeignKey(mc => mc.IdComment)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<MediaNote>()
                .HasOne(mn => mn.IdMediaNavigation)
                .WithOne(mf => mf.MediaNote)
                .HasForeignKey<MediaNote>(mn => mn.IdMedia)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<UserAchievement>()
                .HasKey(ua => new { ua.IdUser, ua.IdAchievement });

            modelBuilder.Entity<UserAchievement>()
                .HasOne(ua => ua.UserNavigation)
                .WithMany(u => u.UserAchievements)
                .HasForeignKey(ua => ua.IdUser)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<UserAchievement>()
                .HasOne(ua => ua.AchievementsNavigation)
                .WithMany(a => a.UserAchievement)
                .HasForeignKey(ua => ua.IdAchievement)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Achievement>()
                .HasOne(a => a.MediaFilesNavigation)
                .WithMany(mf => mf.Achievements)
                .HasForeignKey(a => a.IdMedia)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<MediaEvent>()
                .HasKey(me => new { me.IdEvent, me.IdMedia });

            modelBuilder.Entity<MediaEvent>()
                .HasOne(me => me.IdEventNavigation)
                .WithMany(e => e.MediaEvents)
                .HasForeignKey(me => me.IdEvent)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<MediaEvent>()
                .HasOne(me => me.MediaFilesNavigation)
                .WithMany(mf => mf.MediaEvents)
                .HasForeignKey(me => me.IdMedia)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<MediaClub>()
                .HasKey(mc => new { mc.IdClub, mc.IdMedia });

            modelBuilder.Entity<MediaClub>()
                .HasOne(mc => mc.IdClubNavigation)
                .WithMany(c => c.MediaClubs)
                .HasForeignKey(mc => mc.IdClub)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<MediaClub>()
                .HasOne(mc => mc.MediaFilesNavigation)
                .WithMany(mf => mf.MediaClubs)
                .HasForeignKey(mc => mc.IdMedia)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<MediaNote>()
                .HasKey(mn => new { mn.IdNote, mn.IdMedia });

            modelBuilder.Entity<MediaNote>()
                .HasOne(mn => mn.IdMediaNavigation)
                .WithOne(mf => mf.MediaNote)
                .HasForeignKey<MediaNote>(mn => mn.IdMedia)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<MediaNote>()
                .HasOne(mn => mn.IdNoteNavigation)
                .WithMany(n => n.MediaNotes)
                .HasForeignKey(mn => mn.IdNote)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<UserAvatar>()
                .HasOne(ua => ua.User)
                .WithMany(u => u.UserAvatars)
                .HasForeignKey(ua => ua.IdUser)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<UserAvatar>()
                .HasOne(ua => ua.Media)
                .WithMany(m => m.UserAvatars)
                .HasForeignKey(ua => ua.IdMedia)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<NoteLike>()
                .HasOne(l => l.Note)
                .WithMany(n => n.Likes)
                .HasForeignKey(l => l.NoteId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<NoteLike>()
                .HasOne(l => l.User)
                .WithMany(u => u.Likes)
                .HasForeignKey(l => l.UserId)
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