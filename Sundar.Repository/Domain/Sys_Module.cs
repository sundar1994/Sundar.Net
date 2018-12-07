using System;

namespace Sundar.Repository.Domain
{
    public class Sys_Module : TreeEntity
    {
        public Sys_Module()
        {
            this.Url = string.Empty;
            this.IconName = string.Empty;
            this.Status = 0;
            this.SortNo = 0;
            this.Code = string.Empty;
        }

        /// <summary>
        /// 主页面URL
        /// </summary>
        public string Url { get; set; }
        /// <summary>
        /// 排序号
        /// </summary>
        public int SortNo { get; set; }
        /// <summary>
        /// 是否自动展开
        /// </summary>
        public bool IsAutoExpand { get; set; }
        /// <summary>
        /// 节点图标文件名称
        /// </summary>
        public string IconName { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int IsMenu { get; set; }
        /// <summary>
        /// 当前状态
        /// </summary>
        public int Status { get; set; }
        /// <summary>
        /// 模块标识
        /// </summary>
        /// <value>The code.</value>
        public string Code { get; set; }
    }
}