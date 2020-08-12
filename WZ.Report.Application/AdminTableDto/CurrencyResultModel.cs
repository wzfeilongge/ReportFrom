using System;
using System.Collections.Generic;
using System.Text;

namespace WZ.Report.Application.AdminTableDto
{
    /// <summary>
    /// 部门负责人 领导班子成员统计表格
    /// </summary>
    public class CurrencyResultModel
    {
        /// <summary>
        /// 四月
        /// </summary>
        public bool April { get; set; } = false;

        /// <summary>
        /// 八月
        /// </summary>
        public bool August { get; set; } = false;

        /// <summary>
        /// 十二月
        /// </summary>
        public bool December { get; set; } = false;

        /// <summary>
        /// 二月
        /// </summary>
        public bool February { get; set; } = false;

        /// <summary>
        /// 一月
        /// </summary>
        public bool January { get; set; } = false;

        /// <summary>
        /// 七月
        /// </summary>
        public bool July { get; set; } = false;

        /// <summary>
        /// 六月
        /// </summary>
        public bool June { get; set; } = false;

        /// <summary>
        /// 三月
        /// </summary>
        public bool March { get; set; } = false;

        /// <summary>
        /// 五月
        /// </summary>
        public bool May { get; set; } = false;

        /// <summary>
        /// 单位名称姓名 单位名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 十一月
        /// </summary>
        public bool November { get; set; } = false;

        /// <summary>
        /// 十月
        /// </summary>
        public bool October { get; set; } = false;

        /// <summary>
        /// 主体负责人姓名 联系电话
        /// </summary>
        public string PhoneNameDepartment { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }

        /// <summary>
        /// 登记权限
        /// </summary>
        public int Role { get; set; }

        /// <summary>
        /// 九月
        /// </summary>
        public bool September { get; set; } = false;

        /// <summary>
        /// 经办人
        /// </summary>
        public string Undertaker { get; set; }

        /// <summary>
        /// 承办人联系方式
        /// </summary>
        public string Phone { get; set; }


        /// <summary>
        /// 登记ID
        /// </summary>
        public int UserId { get; set; }

    }
}
