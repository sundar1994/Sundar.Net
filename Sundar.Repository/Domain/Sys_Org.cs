using System;

namespace Sundar.Repository.Domain
{
    /// <summary>
    /// 后台用户组织表
    /// </summary>
    public class Sys_Org : TreeEntity
    {
        /// <summary>
        /// 热键
        /// </summary>
        public string HotKey { get; set; }
        /// <summary>
        /// 是否叶子节点
        /// </summary>
        public bool IsLeaf { get; set; }
        /// <summary>
        /// 是否自动展开
        /// </summary>
        public bool IsAutoExpand { get; set; }
        /// <summary>
        /// 节点图标文件名称
        /// </summary>
        public string IconName { get; set; }
        /// <summary>
        /// 当前状态
        /// </summary>
        public int Status { get; set; }
        /// <summary>
        /// 业务对照码
        /// </summary>
        public string BizCode { get; set; }
        /// <summary>
        /// 自定义扩展码
        /// </summary>
        public string CustomCode { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }
        /// <summary>
        /// 创建人ID
        /// </summary>
        public int CreateId { get; set; }
        /// <summary>
        /// 排序号
        /// </summary>
        public int SortNo { get; set; }
        /// <summary>
        /// 分类名称
        /// </summary>
        public string TypeName { get; set; }
        /// <summary>
        /// 分类ID
        /// </summary>
        public int TypeId { get; set; }
    }
}