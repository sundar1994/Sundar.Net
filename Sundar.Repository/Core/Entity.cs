using System;

namespace Sundar.Repository.Domain
{
    public abstract class Entity
    {
        /// <summary>
        /// 自增主键
        /// </summary>
        public int Id { get; set; }
    }
}