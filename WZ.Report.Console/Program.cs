using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using WZ.Report.Common;
using WZ.Report.Model;

namespace WZ.Report.Console
{
    public class Program
    {
        private static readonly string Connstr = "Server=127.0.0.1;Port=3309;Database=wzreport;Uid=root;Pwd=123456;Character Set=utf8";

        private static readonly IFreeSql freeSql = new FreeSql.FreeSqlBuilder()
        .UseConnectionString((FreeSql.DataType.MySql), Connstr)
        .UseMonitorCommand(cmd => Trace.WriteLine($"线程：{cmd.CommandText}\r\n"))
        .UseAutoSyncStructure(true)
        .UseNoneCommandParameter(true)
        .Build();

        private static bool flag = true;

        public static void CodeFirst()
        {
            List<Type> types = new List<Type>
            {
               typeof(FillForm),
               typeof(ProjectInfo),
               typeof(Questions),
               typeof(RegisterInfo),
               typeof(SysUser),
               typeof(Conversation)
            };
            freeSql.CodeFirst.SyncStructure(types.ToArray());
        }

        public static void Main(string[] args)
        {

            //     CodeFirst();
            ////  SeedUserData();

            //  System.Console.WriteLine($"执行结束");

            //  System.Console.ReadKey();

            //修改用户密码工具

            while (flag)
            {
                System.Console.WriteLine("密码修改工具");

                System.Console.WriteLine("请输入要修改密码的用户名");

                var str = System.Console.ReadLine();

                var repo = freeSql.GetRepository<SysUser>();

                var data = repo.Where(x => x.UserName.Equals(str)).First();

                if (data != null)
                {
                    System.Console.WriteLine($"找到用户{JsonConvert.SerializeObject(data)} ");

                    System.Console.WriteLine("请输入要修改的密码");
                    var pwd = System.Console.ReadLine();
                    System.Console.WriteLine("请再次输入要修改的密码");
                    var secpwd = System.Console.ReadLine();
                    if (string.IsNullOrEmpty(pwd))
                    {
                        System.Console.WriteLine("请输入正确的密码");
                        continue;
                    }
                    if (pwd.Equals(secpwd))
                    {
                        data.Password = MD5Helper.MD5Encrypt32(pwd);
                        var result = repo.Update(data);

                        if (result > 0)
                        {
                            System.Console.WriteLine("密码修改成功");
                        }
                        else
                        {
                            System.Console.WriteLine("密码修改失败");
                        }
                    }
                }
                else
                {
                    System.Console.WriteLine("没有找到用户");
                }

                System.Console.WriteLine("密码是否继续修改");
                System.Console.WriteLine("1继续  2退出");
                var choose = System.Console.ReadLine();
                switch (choose)
                {
                    case "1":
                        flag = true;
                        break;
                    case "2":
                        return;
                    case "addtable":
                        SeedData();
                        return;
                    case "adduserdata":
                        SeedUserData();
                        return;
                    default:
                        return;
                }
            }

            System.Console.ReadKey();
        }
        public static void SeedData()
        {
            //1 部门负责人  2 党组书记 3 班子成员 0其他

            #region  表格 种子数据

            // 1 部门负责人  种子数据
            List<ProjectInfo> projectInfosdepartment = new List<ProjectInfo>
            {
                new ProjectInfo
                {

                    ProjectName="对本部门各干警开展日常性谈话(全年不少于2次/每人,已开展谈话的请同时上报附表五)",
                    Sort=1,
                    Role=1
                },
                 new ProjectInfo
                {

                    ProjectName="全年为部门干警上党风廉政教育课(不少于1次)",
                    Sort=2,
                    Role=1
                },
                  new ProjectInfo
                {

                    ProjectName="全年开展支部组织生活会(不少于12次)",
                    Sort=3,
                    Role=1
                },
                 new ProjectInfo
                {

                    ProjectName="每半年度对本部门落实党风廉政建设工作情况和负责人落实“一岗双责”情况进行一次汇总，分析不足，提出计划，向分管院领导报告并书面报备(全年2次)",
                    Sort=4,
                    Role=1
                },  new ProjectInfo
                {

                    ProjectName="过问案件情况",
                    Sort=1,
                    Role=1,
                    LinkTable=4,
                    DefaultData="填写附件表4"
                },
                  new ProjectInfo
                {

                    ProjectName="对本部门干警落实中央八项规定精神和日常管理制度遵守情况的监督监察情况(次数、具体内容)",
                    Sort=5,
                    Role=1
                }
                  ,
                  new ProjectInfo
                {

                    ProjectName="对本部门发生的违纪违法行为或其他重大问题及时向分管院领导报告,并向纪检监察部门通报",
                    Sort=6,
                    Role=1
                }
                  ,
                  new ProjectInfo
                {

                    ProjectName="其他职责范围内的党风廉政主体责任落实工作",
                    Sort=7,
                    Role=1
                }

            };

            // 2党组书记  种子数据
            List<ProjectInfo> projectsinfosecretary = new List<ProjectInfo>
            {

                new ProjectInfo
                {

                    ProjectName="每季度召开党风廉政专题研究党组会(全年不少于4次)",
                    Sort=1,
                    Role=2
                },
                  new ProjectInfo
                {

                    ProjectName="对各班子成员和下级法院党组主要负责人开展日常性谈话(全年不少于2次/每人,已开展谈话的请同时上报附表五)",
                    Sort=2,
                    Role=2
                },
                  new ProjectInfo
                {

                    ProjectName="每年作一次专题党风廉政报告或上党课",
                    Sort=3,
                    Role=2
                }
                  ,
                  new ProjectInfo
                {

                    ProjectName="召开班子民主生活会",
                    Sort=4,
                    Role=2
                }
                  ,
                  new ProjectInfo
                {

                    ProjectName="以普通党员参加支部活动和组织生活",
                    Sort=5,
                    Role=2
                }
                  ,
                  new ProjectInfo
                {

                    ProjectName="每半年听取班子成员(基层法院主要负责人)落实党风廉政工作专题汇报一次",
                    Sort=6,
                    Role=2
                }
                  ,
                  new ProjectInfo
                {

                    ProjectName="年初带队对基层法院党组上年度党风廉政建设主体责任落实情况进行检查考核",
                    Sort=7,
                    Role=2
                }
                  ,
                  new ProjectInfo
                {

                    ProjectName="按要求及时向上级法院党组和纪委报告上年度和当前党风廉政建设责任制落实情况、党组书记“第一次责任人”履职和廉洁自律情况",
                    Sort=8,
                    Role=2
                }
                  ,
                  new ProjectInfo
                {

                    ProjectName="集体研究“三重一大”等事项(次数、事项内容)",
                    Sort=9,
                    Role=2
                }
                  ,
                  new ProjectInfo
                {

                    ProjectName="过问案件情况",
                    Sort=10,
                    Role=2,
                    LinkTable=4,
                    DefaultData="填写附件表4"
                }
                  ,
                  new ProjectInfo
                {

                    ProjectName="对基层法院领导班子成员特别是“一把手”的协管监督(次数、方式)",
                    Sort=11,
                    Role=2
                }
                   ,
                  new ProjectInfo
                {

                    ProjectName="支持纪检监察履职情况",
                    Sort=12,
                    Role=2
                }
                   ,
                  new ProjectInfo
                {

                    ProjectName="其他属于党组书记(院长)职责范围内的党风廉政和反腐败工作主题",
                    Sort=13,
                    Role=2
                }





            };


            // 3 班子成员 种子数据
            List<ProjectInfo> projectInfos = new List<ProjectInfo>
            {

                new ProjectInfo
                {

                    ProjectName="对分管各部门负责人开展日常性谈话(全年不少于2次/每人,已开展谈话的请同时上报附表五)",
                    Sort=1,
                    Role=3
                },
                 new ProjectInfo
                {

                    ProjectName="全年为分管部门干警上党风廉政教育课(不少于1次)",
                    Sort=2,
                    Role=3
                },
                    new ProjectInfo
                {

                    ProjectName="全年参加班子民主生活会(不少于1次)",
                    Sort=3,
                    Role=3
                },
                 new ProjectInfo
                {

                    ProjectName="全年以普通党员参加支部活动和组织生活(不少于12次)",
                    Sort=4,
                    Role=3
                },
                    new ProjectInfo
                {

                    ProjectName="每半年听取分管部门落实党风廉政工作专题汇报一次(全年2次)",
                    Sort=5,
                    Role=3
                },
                 new ProjectInfo
                {

                    ProjectName="年初带队对基层法院党组上年度党风廉政建设主体责任落实情况进行检查考核",
                    Sort=6,
                    Role=3
                },
                    new ProjectInfo
                {

                    ProjectName="按要求及时向院党组汇报上一年度和当前分管部门党风廉政建设责任制落实情况和本人履行“一岗双责”情况",
                    Sort=7,
                    Role=3
                },
                 new ProjectInfo
                {

                    ProjectName="过问案件情况",
                    Sort=8,
                    Role=3,
                    LinkTable=4,
                    DefaultData="填写附件表4"
                },
                    new ProjectInfo
                {

                    ProjectName="集体研究“三重一大”等事项(次数、事项内容)",
                    Sort=9,
                    Role=3
                },
                 new ProjectInfo
                {

                    ProjectName="对联系基层法院领导班子成员特别是“一把手”的协管监督(次数,方式)",
                    Sort=10,
                    Role=3
                }
                 ,
                 new ProjectInfo
                {

                    ProjectName="对分管部门发生的违纪违法行为或其他重大问题及时向书记(院长)报告,并向纪检监察通报",
                    Sort=11,
                    Role=3
                }
                 ,
                 new ProjectInfo
                {

                    ProjectName="其他属于各分管领导职责范围内的党风廉政和反腐败工作主体责任",
                    Sort=12,
                    Role=3
                }



            };

            #endregion

            var resultdepartment = freeSql.Insert(projectInfosdepartment).ExecuteAffrows();

            System.Console.WriteLine($"插入数据成功 部门负责人数据{resultdepartment}条");

            var resultsecretary = freeSql.Insert(projectsinfosecretary).ExecuteAffrows();
            System.Console.WriteLine($"插入数据成功 党组书记数据{resultsecretary}条");

            var resultmember = freeSql.Insert(projectInfos).ExecuteAffrows();
            System.Console.WriteLine($"插入数据成功 班子成员数据{resultmember}条");

        }

        public static void SeedUserData()
        {
            #region 班子成员种子数据

            List<SysUser> sysUsers = new List<SysUser>
            {
                new SysUser
                {
                    Role=3,
                    UserName="陈有为",
                    UserPhone="15088996666",
                    Password=MD5Helper.MD5Encrypt32("chenyouwei")
                },
                new SysUser
                {
                    Role=3,
                    UserName="丁筱海",
                    UserPhone="13906654516",
                    Password=MD5Helper.MD5Encrypt32("dingxiaohai")
                },
                new SysUser
                {
                    Role=3,
                    UserName="谢作幸",
                    UserPhone="13506662066",
                    Password=MD5Helper.MD5Encrypt32("xiezuoxing")
                },
                new SysUser
                {
                    Role=3,
                    UserName="邱志丰",
                    UserPhone="13806824918",
                    Password=MD5Helper.MD5Encrypt32("qiuzhifeng")
                },
                new SysUser
                {
                    Role=3,
                    UserName="潘光林",
                    UserPhone="13968827666",
                    Password=MD5Helper.MD5Encrypt32("panguanglin")
                },
                new SysUser
                {
                    Role=3,
                    UserName="王一炬",
                    UserPhone="13506649005",
                    Password=MD5Helper.MD5Encrypt32("wangyiju")
                },
                new SysUser
                {
                    Role=3,
                    UserName="张纯亮",
                    UserPhone="13987878858",
                    Password=MD5Helper.MD5Encrypt32("zhangchunliang")
                },
                new SysUser
                {
                    Role=3,
                    UserName="陈卫国",
                    UserPhone="13857705898",
                    Password=MD5Helper.MD5Encrypt32("chenweiguo")
                },
                new SysUser
                {
                    Role=3,
                    UserName="陈积业",
                    UserPhone="13906878552",
                    Password=MD5Helper.MD5Encrypt32("chenjiye")
                },
                new SysUser
                {
                    Role=3,
                    UserName="朱鹏鸣",
                    UserPhone="13868377788",
                    Password=MD5Helper.MD5Encrypt32("zhupengming")
                }                ,
                new SysUser
                {
                    Role=3,
                    UserPhone="13906776037",
                    UserName="韩安锦1",
                    Password=MD5Helper.MD5Encrypt32("hananjin")
                }
            };




            #endregion

            #region 部门负责人种子数据

            List<SysUser> sysUserDepartment = new List<SysUser>
            {
                new SysUser
                {
                    Role=1,
                    UserName="徐民",
                    Department="人事处",
                    Password=MD5Helper.MD5Encrypt32("xumin")
                },
                new SysUser
                {
                    Role=1,
                    UserName="李平",
                    Department="宣教处",
                    Password=MD5Helper.MD5Encrypt32("liping")
                },
                new SysUser
                {
                    Role=1,
                    UserName="郑李武",
                    Department="机关党委",
                    Password=MD5Helper.MD5Encrypt32("zhengliwu")
                },
                new SysUser
                {
                    Role=1,
                    UserName="陈如良",
                    Department="新闻宣教处",
                    Password=MD5Helper.MD5Encrypt32("chenruliang")
                },
                new SysUser
                {
                    Role=1,
                    UserName="戴真",
                    Department="立案庭",
                    Password=MD5Helper.MD5Encrypt32("daizhen")
                },
                new SysUser
                {
                    Role=1,
                    UserName="任国权",
                    Department="刑一庭",
                    Password=MD5Helper.MD5Encrypt32("renguoquan")
                },
                new SysUser
                {
                   Role=1,
                    UserName="徐建伟",
                    Department="刑二庭",
                    Password=MD5Helper.MD5Encrypt32("xujianwei")
                },
                new SysUser
                {
                    Role=1,
                    UserName="夏孟宣",
                    Department="民一庭",
                    Password=MD5Helper.MD5Encrypt32("xiamengxuan")
                },
                new SysUser
                {
                    Role=1,
                    UserName="郑国栋",
                    Department="民二庭",
                    Password=MD5Helper.MD5Encrypt32("zhengguodong")
                },
                new SysUser
                {
                    Role=1,
                    UserName="陈锋",
                    Department="民三庭",
                    Password=MD5Helper.MD5Encrypt32("chenfeng")
                },
                new SysUser
                {
                    Role=1,
                    UserName="曹启东",
                    Department="民四庭",
                    Password=MD5Helper.MD5Encrypt32("caoqidong")
                },
                new SysUser
                {
                    Role=1,
                    UserName="高兴兵",
                    Department="民五庭",
                    Password=MD5Helper.MD5Encrypt32("gaoxingbing")
                }
                ,
                new SysUser
                {
                    Role=1,
                    UserName="方飞潮",
                    Department="民六庭",
                    Password=MD5Helper.MD5Encrypt32("fangfeichao")
                }
                ,
                new SysUser
                {
                    Role=1,
                    UserName="林青青",
                    Department="行政一庭",
                    Password=MD5Helper.MD5Encrypt32("linqingqing")
                }
                ,
                new SysUser
                {
                    Role=1,
                    UserName="马永利",
                    Department="行政二庭",
                    Password=MD5Helper.MD5Encrypt32("caoqidong")
                }
                ,
                new SysUser
                {
                    Role=1,
                    UserName="吴忠烈",
                    Department="审监庭",
                    Password=MD5Helper.MD5Encrypt32("wuzhognlie")
                }
                ,
                new SysUser
                {
                    Role=1,
                    UserName="陈成荣",
                    Department="执行庭",
                    Password=MD5Helper.MD5Encrypt32("chenchengrong")
                }
                ,
                new SysUser
                {
                    Role=1,
                    UserName="丁前鹏",
                    Department="执行实施处",
                    Password=MD5Helper.MD5Encrypt32("dingqianpeng")
                }
                ,
                new SysUser
                {
                    Role=1,
                    UserName="叶淑红",
                    Department="执行监督处",
                    Password=MD5Helper.MD5Encrypt32("yeshuhong")
                }
                ,
                new SysUser
                {
                    Role=1,
                    UserName="朱若荪",
                    Department="研究室",
                    Password=MD5Helper.MD5Encrypt32("zhuruosun")
                }
                ,
                new SysUser
                {
                    Role=1,
                    UserName="徐克谊",
                    Department="审管办",
                    Password=MD5Helper.MD5Encrypt32("xukeyi")
                }
                ,
                new SysUser
                {
                    Role=1,
                    UserName="阮利平",
                    Department="法警支队",
                    Password=MD5Helper.MD5Encrypt32("ruanliping")
                }
                ,
                new SysUser
                {
                    Role=1,
                    UserName="张琛",
                    Department="司法鉴定处",
                    Password=MD5Helper.MD5Encrypt32("wangchen")
                }
                ,
                new SysUser
                {
                    Role=1,
                    UserName="韩安锦",
                    Department="行装处",
                    Password=MD5Helper.MD5Encrypt32("hananjin")
                }




            };





            #endregion

            #region 党组书记 种子数据


            List<SysUser> users = new List<SysUser>
            {
                new SysUser
                {

                    Role=2,
                    UserName="周丰",
                    JobAddress="鹿城法院",
                    Undertaker="彭飞琴",
                    UndertakerPhone="15757766256",
                    Version=1,
                    Password=MD5Helper.MD5Encrypt32("zhoufeng")
                },
                 new SysUser
                {

                    Role=2,
                    UserName="李敏",
                    JobAddress="龙湾法院",
                    Undertaker="蕾迎晨",
                    UndertakerPhone="15205872883",
                    Version=1,
                    Password=MD5Helper.MD5Encrypt32("limin")
                },
                  new SysUser
                {

                    Role=2,
                    UserName="周虹",
                    JobAddress="瓯海法院",
                    Undertaker="王小军",
                    UndertakerPhone="13806856389",
                    Version=1,
                    Password=MD5Helper.MD5Encrypt32("zhouhong")
                },
                   new SysUser
                {

                    Role=2,
                    UserName="李德通",
                    JobAddress="洞头法院",
                    Undertaker="郑怡",
                    UndertakerPhone="18757783686",
                    Version=1,
                    Password=MD5Helper.MD5Encrypt32("lidetong")
                },
                   new SysUser
                {
                    Role=2,
                    UserName="林向光",
                    JobAddress="乐清法院",
                    Undertaker="林建林",
                    UndertakerPhone="13819778808",
                    Version=1,
                    Password=MD5Helper.MD5Encrypt32("zhoufeng")
                },
                     new SysUser
                {

                    Role=2,
                    UserName="胡丕敢",
                    JobAddress="瑞安法院",
                    Undertaker="陈再武",
                    UndertakerPhone="18858761782",
                    Version=1,
                    Password=MD5Helper.MD5Encrypt32("hupigan")
                },
                      new SysUser
                {

                    Role=2,
                    UserName="杨兴明",
                    JobAddress="永嘉法院",
                    Undertaker="李俊",
                    UndertakerPhone="13968943963",
                    Version=1,
                    Password=MD5Helper.MD5Encrypt32("yangxingming")
                },
                       new SysUser
                {

                    Role=2,
                    UserName="章禾舟",
                    JobAddress="文成法院",
                    Undertaker="刘洋洋",
                    UndertakerPhone="18358796166",
                    Version=1,
                    Password=MD5Helper.MD5Encrypt32("zhanghezhou")
                },
                        new SysUser
                {

                    Role=2,
                    UserName="刘万成",
                    JobAddress="平阳法院",
                    Undertaker="陈爽",
                    UndertakerPhone="13506630890",
                    Version=1,
                    Password=MD5Helper.MD5Encrypt32("liuwancheng")
                },
                 new SysUser
                {
                    Role=2,
                    UserName="詹大勇",
                    JobAddress="泰顺法院",
                    Undertaker="董景对",
                    UndertakerPhone="13868393033",
                    Version=1,
                    Password=MD5Helper.MD5Encrypt32("zhandayong")
                },
                   new SysUser
                {
                    Role=2,
                    UserName="陈斌",
                    JobAddress="苍南法院",
                    Undertaker="章国栋",
                    UndertakerPhone="18267851388",
                    Version=1,
                    Password=MD5Helper.MD5Encrypt32("chenbin")
                },
                 new SysUser
                {

                    Role=2,
                    UserName="董忠波",
                    JobAddress="龙港法院",
                    Undertaker="",
                    UndertakerPhone="",
                    Version=1,
                    Password=MD5Helper.MD5Encrypt32("dongzhongbo")
                },



            };





            #endregion

            //党组书记 数据
            var dzsj = freeSql.Insert(users).ExecuteAffrows();
            System.Console.WriteLine($"插入数据成功 党组书记数据{dzsj}条");
            //部门负责人种子数据
            var department = freeSql.Insert(sysUserDepartment).ExecuteAffrows();
            System.Console.WriteLine($"插入数据成功 部门负责人员数据{department}条");
            //领导班子
            var ldbz = freeSql.Insert(sysUsers).ExecuteAffrows();
            System.Console.WriteLine($"插入数据成功 班子人员数据{ldbz}条");
        }
    }
}
