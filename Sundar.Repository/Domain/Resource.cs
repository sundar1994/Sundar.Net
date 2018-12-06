using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sundar.Repository.Domain
{
    /// <summary>
	/// 资源表
	/// </summary>
    public partial class Resource : Entity
    {
        public Resource()
        {
            this.CascadeId = string.Empty;
            this.Name = string.Empty;
            this.SortNo = 0;
            this.Description = string.Empty;
            this.ParentName = string.Empty;
            this.ParentId = 0;
            this.AppId = string.Empty;
            this.AppName = string.Empty;
            this.TypeName = string.Empty;
            this.TypeId = string.Empty;
        }

        /// <summary>
	    /// 节点语义ID
	    /// </summary>
        public string CascadeId { get; set; }
        /// <summary>
	    /// 名称
	    /// </summary>
        public string Name { get; set; }
        /// <summary>
	    /// 排序号
	    /// </summary>
        public int SortNo { get; set; }
        /// <summary>
	    /// 描述
	    /// </summary>
        public string Description { get; set; }
        /// <summary>
	    /// 父节点名称
	    /// </summary>
        public string ParentName { get; set; }
        /// <summary>
	    /// 父节点流ID
	    /// </summary>
        public int ParentId { get; set; }
        /// <summary>
	    /// 资源所属应用ID
	    /// </summary>
        public string AppId { get; set; }
        /// <summary>
	    /// 所属应用名称
	    /// </summary>
        public string AppName { get; set; }
        /// <summary>
	    /// 分类名称
	    /// </summary>
        public string TypeName { get; set; }
        /// <summary>
	    /// 分类ID
	    /// </summary>
        public string TypeId { get; set; }
        /// <summary>
	    /// 是否可用
	    /// </summary>
        public bool Disable { get; set; }

    }
}
