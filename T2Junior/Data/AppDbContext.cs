using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore.MySql.Scaffolding.Internal;
using T2Junior.Models;

namespace T2Junior.Data;

public partial class AppDbContext : DbContext
{
    public AppDbContext()
    {
    }

    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Club> Clubs { get; set; }

    public virtual DbSet<ClubRole> ClubRoles { get; set; }

    public virtual DbSet<ClubUser> ClubUsers { get; set; }

    public virtual DbSet<Comment> Comments { get; set; }

    public virtual DbSet<Event> Events { get; set; }

    public virtual DbSet<EventDirection> EventDirections { get; set; }

    public virtual DbSet<Note> Notes { get; set; }

    public virtual DbSet<NoteStatus> NoteStatuses { get; set; }

    public virtual DbSet<Organization> Organizations { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<UserRole> UserRoles { get; set; }

    public virtual DbSet<Wall> Walls { get; set; }

    public virtual DbSet<WallType> WallTypes { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseMySql("server=k0zya-m0rda.ru;user=t2_api_user;password=t2APIC0nn3ct!;database=YouthMovementDB", Microsoft.EntityFrameworkCore.ServerVersion.Parse("8.0.40-mysql"));

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseCollation("utf8mb4_0900_ai_ci")
            .HasCharSet("utf8mb4");

        modelBuilder.Entity<Club>(entity =>
        {
            entity.HasKey(e => e.IdClub).HasName("PRIMARY");

            entity.ToTable("clubs");

            entity.Property(e => e.IdClub).HasColumnName("id_club");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .HasColumnName("name")
                .UseCollation("utf8mb3_general_ci")
                .HasCharSet("utf8mb3");
            entity.Property(e => e.Raiting).HasColumnName("raiting");
            entity.Property(e => e.Reports)
                .HasMaxLength(45)
                .HasColumnName("reports");
            entity.Property(e => e.Rules)
                .HasMaxLength(500)
                .HasColumnName("rules")
                .UseCollation("utf8mb3_general_ci")
                .HasCharSet("utf8mb3");
            entity.Property(e => e.Target)
                .HasMaxLength(200)
                .HasColumnName("target")
                .UseCollation("utf8mb3_general_ci")
                .HasCharSet("utf8mb3");
        });

        modelBuilder.Entity<ClubRole>(entity =>
        {
            entity.HasKey(e => e.IdRole).HasName("PRIMARY");

            entity.ToTable("club_roles");

            entity.HasIndex(e => e.Name, "name_UNIQUE").IsUnique();

            entity.Property(e => e.IdRole).HasColumnName("id_role");
            entity.Property(e => e.Name)
                .HasMaxLength(70)
                .HasColumnName("name")
                .UseCollation("utf8mb3_general_ci")
                .HasCharSet("utf8mb3");
        });

        modelBuilder.Entity<ClubUser>(entity =>
        {
            entity.HasKey(e => new { e.IdClub, e.IdUser })
                .HasName("PRIMARY")
                .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0 });

            entity.ToTable("club_users");

            entity.HasIndex(e => e.IdRole, "FK_CRoles_CU_idx");

            entity.HasIndex(e => e.IdUser, "FK_Users_CU_idx");

            entity.Property(e => e.IdClub).HasColumnName("id_club");
            entity.Property(e => e.IdUser).HasColumnName("id_user");
            entity.Property(e => e.IdRole).HasColumnName("id_role");

            entity.HasOne(d => d.IdClubNavigation).WithMany(p => p.ClubUsers)
                .HasForeignKey(d => d.IdClub)
                .HasConstraintName("FK_Clubs_CU");

            entity.HasOne(d => d.IdRoleNavigation).WithMany(p => p.ClubUsers)
                .HasForeignKey(d => d.IdRole)
                .HasConstraintName("FK_CRoles_CU");

            entity.HasOne(d => d.IdUserNavigation).WithMany(p => p.ClubUsers)
                .HasForeignKey(d => d.IdUser)
                .HasConstraintName("FK_Users_CU");
        });

        modelBuilder.Entity<Comment>(entity =>
        {
            entity.HasKey(e => e.IdComment).HasName("PRIMARY");

            entity.ToTable("comments");

            entity.HasIndex(e => e.ParrentCommentId, "FK_Comments_Comments_idx");

            entity.HasIndex(e => e.IdNote, "FK_Notes_Comments_idx");

            entity.HasIndex(e => e.IdUser, "FK_Users_Comments_idx");

            entity.Property(e => e.IdComment).HasColumnName("id_comment");
            entity.Property(e => e.CreationDatetime)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("datetime")
                .HasColumnName("creation_datetime");
            entity.Property(e => e.IdNote).HasColumnName("id_note");
            entity.Property(e => e.IdUser).HasColumnName("id_user");
            entity.Property(e => e.LikeCount).HasColumnName("like_count");
            entity.Property(e => e.ParrentCommentId).HasColumnName("parrent_comment_id");
            entity.Property(e => e.Text)
                .HasMaxLength(500)
                .HasColumnName("text")
                .UseCollation("utf8mb3_general_ci")
                .HasCharSet("utf8mb3");

            entity.HasOne(d => d.IdNoteNavigation).WithMany(p => p.Comments)
                .HasForeignKey(d => d.IdNote)
                .HasConstraintName("FK_Notes_Comments");

            entity.HasOne(d => d.IdUserNavigation).WithMany(p => p.Comments)
                .HasForeignKey(d => d.IdUser)
                .HasConstraintName("FK_Users_Comments");

            entity.HasOne(d => d.ParrentComment).WithMany(p => p.InverseParrentComment)
                .HasForeignKey(d => d.ParrentCommentId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_Comments_Comments");
        });

        modelBuilder.Entity<Event>(entity =>
        {
            entity.HasKey(e => e.IdEvent).HasName("PRIMARY");

            entity.ToTable("events");

            entity.HasIndex(e => e.IdClub, "FK_Clubs_Events_idx");

            entity.HasIndex(e => e.IdDirection, "FK_EvDirections_Events_idx");

            entity.Property(e => e.IdEvent).HasColumnName("id_event");
            entity.Property(e => e.Datetime)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("datetime")
                .HasColumnName("datetime");
            entity.Property(e => e.FactParticpants).HasColumnName("fact_particpants");
            entity.Property(e => e.IdClub).HasColumnName("id_club");
            entity.Property(e => e.IdDirection).HasColumnName("id_direction");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .HasColumnName("name")
                .UseCollation("utf8mb3_general_ci")
                .HasCharSet("utf8mb3");
            entity.Property(e => e.NumberParticpants)
                .HasDefaultValueSql("'1'")
                .HasColumnName("number_particpants");
            entity.Property(e => e.Place)
                .HasMaxLength(200)
                .HasColumnName("place")
                .UseCollation("utf8mb3_general_ci")
                .HasCharSet("utf8mb3");
            entity.Property(e => e.Raiting)
                .HasDefaultValueSql("'1'")
                .HasColumnName("raiting");

            entity.HasOne(d => d.IdClubNavigation).WithMany(p => p.Events)
                .HasForeignKey(d => d.IdClub)
                .HasConstraintName("FK_Clubs_Events");

            entity.HasOne(d => d.IdDirectionNavigation).WithMany(p => p.Events)
                .HasForeignKey(d => d.IdDirection)
                .HasConstraintName("FK_EvDirections_Events");
        });

        modelBuilder.Entity<EventDirection>(entity =>
        {
            entity.HasKey(e => e.IdDirection).HasName("PRIMARY");

            entity.ToTable("event_directions");

            entity.HasIndex(e => e.Name, "name_UNIQUE").IsUnique();

            entity.Property(e => e.IdDirection).HasColumnName("id_direction");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .HasColumnName("name")
                .UseCollation("utf8mb3_general_ci")
                .HasCharSet("utf8mb3");
        });

        modelBuilder.Entity<Note>(entity =>
        {
            entity.HasKey(e => e.IdNote).HasName("PRIMARY");

            entity.ToTable("notes");

            entity.HasIndex(e => e.IdStatus, "FK_NStatuses_Notes_idx");

            entity.HasIndex(e => e.IdRepost, "FK_Notes_Notes_idx");

            entity.HasIndex(e => e.IdWall, "FK_Walls_Notes_idx");

            entity.Property(e => e.IdNote).HasColumnName("id_note");
            entity.Property(e => e.CreationDatetime)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("datetime")
                .HasColumnName("creation_datetime");
            entity.Property(e => e.Description)
                .HasMaxLength(1000)
                .HasColumnName("description")
                .UseCollation("utf8mb3_general_ci")
                .HasCharSet("utf8mb3");
            entity.Property(e => e.IdRepost).HasColumnName("id_repost");
            entity.Property(e => e.IdStatus).HasColumnName("id_status");
            entity.Property(e => e.IdWall).HasColumnName("id_wall");
            entity.Property(e => e.LikeCount).HasColumnName("like_count");
            entity.Property(e => e.Name)
                .HasMaxLength(200)
                .HasColumnName("name")
                .UseCollation("utf8mb3_general_ci")
                .HasCharSet("utf8mb3");

            entity.HasOne(d => d.IdRepostNavigation).WithMany(p => p.InverseIdRepostNavigation)
                .HasForeignKey(d => d.IdRepost)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_Notes_Notes");

            entity.HasOne(d => d.IdStatusNavigation).WithMany(p => p.Notes)
                .HasForeignKey(d => d.IdStatus)
                .HasConstraintName("FK_NStatuses_Notes");

            entity.HasOne(d => d.IdWallNavigation).WithMany(p => p.Notes)
                .HasForeignKey(d => d.IdWall)
                .HasConstraintName("FK_Walls_Notes");
        });

        modelBuilder.Entity<NoteStatus>(entity =>
        {
            entity.HasKey(e => e.IdStatus).HasName("PRIMARY");

            entity.ToTable("note_statuses");

            entity.HasIndex(e => e.Name, "name_UNIQUE").IsUnique();

            entity.Property(e => e.IdStatus).HasColumnName("id_status");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .HasColumnName("name")
                .UseCollation("utf8mb3_general_ci")
                .HasCharSet("utf8mb3");
        });

        modelBuilder.Entity<Organization>(entity =>
        {
            entity.HasKey(e => e.IdOrganization).HasName("PRIMARY");

            entity.ToTable("organizations");

            entity.HasIndex(e => e.Name, "name_UNIQUE").IsUnique();

            entity.Property(e => e.IdOrganization).HasColumnName("id_organization");
            entity.Property(e => e.Name)
                .HasMaxLength(200)
                .HasColumnName("name")
                .UseCollation("utf8mb3_general_ci")
                .HasCharSet("utf8mb3");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.IdUser).HasName("PRIMARY");

            entity.ToTable("users");

            entity.HasIndex(e => e.IdOrganization, "FK_Organizations_Users_idx");

            entity.HasIndex(e => e.IdRole, "FK_URoles_Users_idx");

            entity.HasIndex(e => e.Email, "email_UNIQUE").IsUnique();

            entity.HasIndex(e => e.Phone, "phone_UNIQUE").IsUnique();

            entity.Property(e => e.IdUser).HasColumnName("id_user");
            entity.Property(e => e.AccumulatedPoints).HasColumnName("accumulated_points");
            entity.Property(e => e.Age).HasColumnName("age");
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .HasColumnName("email");
            entity.Property(e => e.FirstName)
                .HasMaxLength(45)
                .HasColumnName("first_name")
                .UseCollation("utf8mb3_general_ci")
                .HasCharSet("utf8mb3");
            entity.Property(e => e.IdOrganization).HasColumnName("id_organization");
            entity.Property(e => e.IdRole).HasColumnName("id_role");
            entity.Property(e => e.LastName)
                .HasMaxLength(45)
                .HasColumnName("last_name")
                .UseCollation("utf8mb3_general_ci")
                .HasCharSet("utf8mb3");
            entity.Property(e => e.Password)
                .HasMaxLength(255)
                .HasColumnName("password")
                .UseCollation("utf8mb3_general_ci")
                .HasCharSet("utf8mb3");
            entity.Property(e => e.Patronymic)
                .HasMaxLength(45)
                .HasColumnName("patronymic")
                .UseCollation("utf8mb3_general_ci")
                .HasCharSet("utf8mb3");
            entity.Property(e => e.Phone)
                .HasMaxLength(12)
                .HasColumnName("phone");
            entity.Property(e => e.Post)
                .HasMaxLength(45)
                .HasColumnName("post")
                .UseCollation("utf8mb3_general_ci")
                .HasCharSet("utf8mb3");
            entity.Property(e => e.Sex)
                .HasDefaultValueSql("'1'")
                .HasColumnName("sex");
            entity.Property(e => e.IsActive)
                .HasDefaultValueSql("'0'")
                .HasColumnName("is_active");

            entity.HasOne(d => d.IdOrganizationNavigation).WithMany(p => p.Users)
                .HasForeignKey(d => d.IdOrganization)
                .HasConstraintName("FK_Organizations_Users");

            entity.HasOne(d => d.IdRoleNavigation).WithMany(p => p.Users)
                .HasForeignKey(d => d.IdRole)
                .HasConstraintName("FK_URoles_Users");

            entity.HasMany(d => d.IdSubscribers).WithMany(p => p.IdUsers)
                .UsingEntity<Dictionary<string, object>>(
                    "UserSubscriber",
                    r => r.HasOne<User>().WithMany()
                        .HasForeignKey("IdSubscribers")
                        .HasConstraintName("FK_Users_USubscribers2"),
                    l => l.HasOne<User>().WithMany()
                        .HasForeignKey("IdUser")
                        .HasConstraintName("FK_Users_USubscribers"),
                    j =>
                    {
                        j.HasKey("IdUser", "IdSubscribers")
                            .HasName("PRIMARY")
                            .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0 });
                        j.ToTable("user_subscribers");
                        j.HasIndex(new[] { "IdSubscribers" }, "FK_Users_USubscribers2");
                        j.IndexerProperty<int>("IdUser").HasColumnName("id_user");
                        j.IndexerProperty<int>("IdSubscribers").HasColumnName("id_subscribers");
                    });

            entity.HasMany(d => d.IdUsers).WithMany(p => p.IdSubscribers)
                .UsingEntity<Dictionary<string, object>>(
                    "UserSubscriber",
                    r => r.HasOne<User>().WithMany()
                        .HasForeignKey("IdUser")
                        .HasConstraintName("FK_Users_USubscribers"),
                    l => l.HasOne<User>().WithMany()
                        .HasForeignKey("IdSubscribers")
                        .HasConstraintName("FK_Users_USubscribers2"),
                    j =>
                    {
                        j.HasKey("IdUser", "IdSubscribers")
                            .HasName("PRIMARY")
                            .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0 });
                        j.ToTable("user_subscribers");
                        j.HasIndex(new[] { "IdSubscribers" }, "FK_Users_USubscribers2");
                        j.IndexerProperty<int>("IdUser").HasColumnName("id_user");
                        j.IndexerProperty<int>("IdSubscribers").HasColumnName("id_subscribers");
                    });
        });

        modelBuilder.Entity<UserRole>(entity =>
        {
            entity.HasKey(e => e.IdRole).HasName("PRIMARY");

            entity.ToTable("user_roles");

            entity.HasIndex(e => e.Name, "name_UNIQUE").IsUnique();

            entity.Property(e => e.IdRole).HasColumnName("id_role");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .HasColumnName("name")
                .UseCollation("utf8mb3_general_ci")
                .HasCharSet("utf8mb3");
        });

        modelBuilder.Entity<Wall>(entity =>
        {
            entity.HasKey(e => e.IdWall).HasName("PRIMARY");

            entity.ToTable("walls");

            entity.HasIndex(e => e.IdOwner, "FK_Users_Walls_idx");

            entity.HasIndex(e => e.IdType, "FK_WTypes_Walls_idx");

            entity.Property(e => e.IdWall).HasColumnName("id_wall");
            entity.Property(e => e.IdOwner).HasColumnName("id_owner");
            entity.Property(e => e.IdType).HasColumnName("id_type");

            entity.HasOne(d => d.IdOwnerNavigation).WithMany(p => p.Walls)
                .HasForeignKey(d => d.IdOwner)
                .HasConstraintName("FK_Clubs_Walls");

            entity.HasOne(d => d.IdOwner1).WithMany(p => p.Walls)
                .HasForeignKey(d => d.IdOwner)
                .HasConstraintName("FK_Users_Walls");

            entity.HasOne(d => d.IdTypeNavigation).WithMany(p => p.Walls)
                .HasForeignKey(d => d.IdType)
                .HasConstraintName("FK_WTypes_Walls");
        });

        modelBuilder.Entity<WallType>(entity =>
        {
            entity.HasKey(e => e.IdType).HasName("PRIMARY");

            entity.ToTable("wall_types");

            entity.HasIndex(e => e.Name, "name_UNIQUE").IsUnique();

            entity.Property(e => e.IdType).HasColumnName("id_type");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .HasColumnName("name")
                .UseCollation("utf8mb3_general_ci")
                .HasCharSet("utf8mb3");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
