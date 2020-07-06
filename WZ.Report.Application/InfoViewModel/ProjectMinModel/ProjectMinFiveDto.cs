using System;
using System.Collections.Generic;
using System.Text;

namespace WZ.Report.Application.InfoViewModel.ProjectMinModel
{
   public class ProjectMinFiveDto
    {
        /// <summary>
        /// 被谈话人
        /// </summary>
        public string Interviewee { get; set; }

        /// <summary>
        /// 被谈话人部门单位及职务
        /// </summary>
        public string IntervieweeDepartment { get; set; }

        /// <summary>
        /// 时间
        /// </summary>
        public DateTime CreateDate { get; set; }

        /// <summary>
        /// 地点
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        /// 谈话人
        /// </summary>
        public string Takker { get; set; }

        /// <summary>
        /// 谈话人部门及职务
        /// </summary>
        public string TakkerAddress { get; set; }

        /// <summary>
        /// 记录人
        /// </summary>
        public string Record { get; set; }

        /// <summary>
        /// 记录人地址
        /// </summary>
        public string RecordDepartment { get; set; }

        /// <summary>
        /// 谈话内容
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// 被谈话人态度
        /// </summary>
        public string IntervieweeMammer { get; set; }

        /// <summary>
        /// 谈话人签名
        /// </summary>
        public string TakkerSig { get; set; }

        /// <summary>
        /// 被谈话人签名
        /// </summary>
        public string IntervieweeSig { get; set; }
    }
}
