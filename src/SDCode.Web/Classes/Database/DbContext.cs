using Microsoft.EntityFrameworkCore;
using SDCode.Web.Models;
using SDCode.Web.Models.Data;

namespace SDCode.Web.Classes.Database
{
    public class SQLiteDBContext : DbContext
    {
        public DbSet<ConsentDbModel> Consents { get; set; }
        public DbSet<DemographicsDbModel> Demographics { get; set; }
        public DbSet<EpworthDbModel> Epworths { get; set; }
        public DbSet<PSQIDbModel> PSQIs { get; set; }
        public DbSet<ResponseDbDataModel> ResponseDatas { get; set; }
        public DbSet<PhaseImageModel> PhaseImages { get; set; }
        public DbSet<NeglectedImageModel> NeglectedImages { get; set; }
        public DbSet<ObscuredImageModel> ObscuredImages { get; set; }
        public DbSet<SessionMetaDbModel> SessionMetas { get; set; }
        public DbSet<SleepQuestionsDbModel> SleepQuestions { get; set; }
        public DbSet<StanfordDbModel> Stanfords { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options) => options.UseSqlite("Data Source=MemoryStudy.db");

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ConsentDbModel>().HasKey(x => x.ParticipantID);
            modelBuilder.Entity<DemographicsDbModel>().HasKey(x => x.ParticipantID);
            modelBuilder.Entity<EpworthDbModel>().HasKey(x => x.ParticipantID);
            modelBuilder.Entity<PhaseImageModel>().HasKey(x => x.ID);
            modelBuilder.Entity<NeglectedImageModel>().HasKey(x => x.ID);
            modelBuilder.Entity<ObscuredImageModel>().HasKey(x => x.ID);
            modelBuilder.Entity<PSQIDbModel>().HasKey(x => x.ParticipantID);
            modelBuilder.Entity<ResponseDbDataModel>().HasKey(x => new { x.SessionID, x.ParticipantID, x.TestName, x.Image });
            modelBuilder.Entity<SessionMetaDbModel>().HasKey(x => new { x.ParticipantID, x.SessionName });
            modelBuilder.Entity<SleepQuestionsDbModel>().HasKey(x => x.ParticipantID);
            modelBuilder.Entity<StanfordDbModel>().HasKey(x => x.ParticipantID);
        }
    }
}