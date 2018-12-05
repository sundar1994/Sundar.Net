using System;

namespace Sundar.Repository.Domain
{
    [Serializable]
    public class Sys_Module : Entity
    {
        /// <summary>
        /// 
        /// </summary>
        public string CascadeId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Name { get; set; }
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
        public int ParentId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string ParentName { get; set; }
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