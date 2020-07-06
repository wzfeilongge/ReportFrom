using FreeSql;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using WZ.Report.Common;

namespace WZ.Report.Model
{
    public class Reportcontext : DbContext
    {
        public Reportcontext()
        {

        }
        public static Reportcontext GetDbContext
        {
            get
            {
                return new Reportcontext();
            }
        }

        private static readonly string Connstr = Appsettings.app(new string[] { "sql", "str" });

        private static readonly int Sqltype = Appsettings.app(new string[] { "sql", "sqlType" }).ObjToInt();
        public DbSet<FillForm> FillForms { get; set; }

        public DbSet<ProjectInfo> ProjectInfos { get; set; }

        public DbSet<Questions> Questions { get; set; }

        public DbSet<RegisterInfo> RegisterInfos { get; set; }

        public DbSet<Conversation> Conversations { get; set; }
        public DbSet<SysUser> SysUsers { get; set; }

        public static readonly IFreeSql freeSql = new FreeSqlBuilder()
        .UseConnectionString((DataType)Sqltype, Connstr)
        .UseMonitorCommand(cmd => Trace.WriteLine($"线程：{cmd.CommandText}\r\n"))
        .UseAutoSyncStructure(true)
        .UseNoneCommandParameter(true)
        .Build();


        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseFreeSql(freeSql);
            base.OnConfiguring(options);
        }
    }
}
