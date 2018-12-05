using System;

namespace Sundar.Repository.Domain
{
    [Serializable]
    public class Sys_ModuleElement: Entity
    {
        public Sys_ModuleElement()
        {
            this.DomId = string.Empty;
            this.Name = string.Empty;
            this.Attr = string.Empty;
            this.Script = string.Empty;
            this.Icon = string.Empty;
            this.ClassName = string.Empty;
            this.Remark = string.Empty;
            this.Sort = 0;
            this.ModuleId = 0;
            this.Status = 1;
        }

        /// <summary>
        /// DOM ID
        /// </summary>
        public string DomId { get; set; }
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Type { get; set; }
        /// <summary>
        /// 元素附加属性
        /// </summary>
        public string Attr { get; set; }
        /// <summary>
        /// 元素调用脚本
        /// </summary>
        public string Script { get; set; }
        /// <summary>
        /// 元素图标
        /// </summary>
        public string Icon { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string ClassName { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }
        /// <summary>
        /// 排序字段
        /// </summary>
        public int Sort { get; set; }
        /// <summary>
        /// 功能模块Id
        /// </summary>
        public int ModuleId { get; set; }
        /// <summary>
        /// 当前状态
        /// </summary>
        public int Status { get; set; }
    }
}