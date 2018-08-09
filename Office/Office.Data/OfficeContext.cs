using System;
using Microsoft.EntityFrameworkCore;
using Office.Models;

namespace Office.Data
{
    public class OfficeContext : DbContext
    {

        public DbSet<Invitation> Invitations { get; set; }

        public DbSet<Team> Teams { get; set; }

        public DbSet<User> Users { get; set; }

        public DbSet<UserTeam> UserTeams { get; set; }

        public DbSet<Event> Events { get; set; }

        public DbSet<TeamEvent> TeamEvents { get; set; }



        protected override void OnConfiguring(DbContextOptionsBuilder builder)
        {
            if (!builder.IsConfigured)
            {
                builder.UseSqlServer(Configuration.ConnnectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<User>()
                .HasKey(p => p.Id);

            builder.Entity<User>()
                .HasIndex(u => u.Username)
                .IsUnique();

            builder.Entity<Team>()
                .HasKey(p => p.Id);

            builder.Entity<Team>()
                .HasIndex(p => p.Name)
                .IsUnique();

            builder.Entity<Team>()
                .Property(p => p.Acronym)
                .HasColumnType("CHAR(3)");


            builder.Entity<Team>()
                .HasOne(p => p.Creator)
                .WithMany(p => p.TeamsCreated)
                .HasForeignKey(k => k.CreatorId)
                .OnDelete(DeleteBehavior.Restrict);
            

            builder.Entity<Event>()
                .HasKey(p => p.Id);

            builder.Entity<Event>()
                .Property(p => p.Name)
                .IsUnicode();

            builder.Entity<Event>()
                .Property(p => p.Description)
                .IsUnicode();

            builder.Entity<Event>()
                .HasOne(p => p.Creator)
                .WithMany(p => p.EventsCreated)
                .HasForeignKey(p => p.CreatorId)
                .OnDelete(DeleteBehavior.Restrict);


            builder.Entity<Invitation>()
                .HasKey(p => p.Id);

            builder.Entity<Invitation>()
                .HasOne(p => p.InvitedUser)
                .WithMany(p => p.Invitations)
                .HasForeignKey(p => p.InvitedUserId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Invitation>()
                .HasOne(p => p.Team)
                .WithMany(p => p.Invitations)
                .HasForeignKey(p => p.TeamId);


            builder.Entity<TeamEvent>()
                .HasKey(p => new { p.TeamId, p.EventId });

            builder.Entity<TeamEvent>()
                .HasOne(p => p.Team)
                .WithMany(p => p.Events)
                .HasForeignKey(p => p.TeamId);


            builder.Entity<TeamEvent>()
                .HasOne(p => p.Event)
                .WithMany(p => p.Teams)
                .HasForeignKey(p => p.EventId);


            builder.Entity<UserTeam>()
                .HasKey(p => new {p.UserId, p.TeamId});

            builder.Entity<UserTeam>()
                .HasOne(p => p.User)
                .WithMany(p => p.Teams)
                .HasForeignKey(p => p.UserId);


            builder.Entity<UserTeam>()
                .HasOne(p => p.Team)
                .WithMany(p => p.TeamMembers)
                .HasForeignKey(p => p.TeamId);
        }
    }
}
