using System.Collections.Generic;
using Sundar.Common;
using Sundar.Repository.Domain;

namespace Sundar.BLL.Response
{
    public class ModuleView
    {
        /// <summary>
        /// ID
        /// </summary>
        /// <returns></returns>
        public int Id { get; set; }

        /// <summary>
	    /// 节点语义ID
	    /// </summary>
        public string CascadeId { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        /// <returns></returns>
        public string Name { get; set; }

        /// <summary>
        /// 主页面URL
        /// </summary>
        /// <returns></returns>
        public string Url { get; set; }

        /// <summary>
        /// 父节点流水号
        /// </summary>
        /// <returns></returns>
        public int ParentId { get; set; }

        /// <summary>
        /// 父节点流水号
        /// </summary>
        /// <returns></returns>
        public string ParentName { get; set; }

        /// <summary>
        /// 节点图标文件名称
        /// </summary>
        /// <returns></returns>
        public string IconName { get; set; }


        public bool Checked { get; set; }

        /// <summary>
        /// 排序号
        /// </summary>
        public int SortNo { get; set; }

        public string Code { get; set; }

        /// <summary>
        /// 模块中的元素
        /// </summary>
        public List<Sys_ModuleElement> Elements = new List<Sys_ModuleElement>();

        public static implicit operator ModuleView(Sys_Module module)
        {
            return module.MapTo<ModuleView>();
        }

        public static implicit operator Sys_Module(ModuleView view)
        {
            return view.MapTo<Sys_Module>();
        }
    }
}
