using FeedForwardBusinessEntities.EntityModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FeedForwardBusinessEntities.EntityContext
{
    public class Context : DbContext
    {
        public DbSet<UserDetail> UserDetailInfo { get; set; }

        public DbSet<RoleDetail> RoleDetailInfo { get; set; }

        public DbSet<DesignationLevel> DesignationLevelInfo { get; set; }

        public DbSet<QuestionDetail> QuestionDetailInfo { get; set; }

        public DbSet<FeedbackSession> FeedbackSessionInfo { get; set; }

        public DbSet<FeedbackSchedulingDetail> FeedbackSchedulingDetailInfo { get; set; }

        public DbSet<LevelDetail> LevelDetailInfo { get; set; }

        public DbSet<FeedbackCategoryLevel> FeedbackCategoryLevelInfo { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=EKHS0840;Database=FeedForward;Integrated Security=true;");
            }
            base.OnConfiguring(optionsBuilder);
        }
    }
}
