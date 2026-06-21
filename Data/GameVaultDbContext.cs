using Microsoft.EntityFrameworkCore;
using GameVault.API.Models.Entities;

namespace GameVault.API.Data
{
    public class GameVaultDbContext : DbContext
    {
        public GameVaultDbContext(DbContextOptions<GameVaultDbContext> options)
            : base(options)
        {
        }

        public DbSet<Player> Players => Set<Player>();
        public DbSet<Match> Matches => Set<Match>();
        public DbSet<MatchParticipant> MatchParticipants => Set<MatchParticipant>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Username must be unique across all players
            modelBuilder.Entity<Player>()
                .HasIndex(p => p.Username)
                .IsUnique();

            // Email must be unique across all players
            modelBuilder.Entity<Player>()
                .HasIndex(p => p.Email)
                .IsUnique();

            // One Match has many MatchParticipants
            modelBuilder.Entity<MatchParticipant>()
                .HasOne(mp => mp.Match)
                .WithMany(m => m.Participants)
                .HasForeignKey(mp => mp.MatchId);

            // One Player has many MatchParticipants
            modelBuilder.Entity<MatchParticipant>()
                .HasOne(mp => mp.Player)
                .WithMany(p => p.MatchParticipants)
                .HasForeignKey(mp => mp.PlayerId);
        }
    }
}