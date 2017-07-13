using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Common.Core;
using Data.Enum;

namespace Data.Entities
{
    public class EntityBase
    {
        /// <summary>
        /// 主键
        /// </summary>
        [Key]
        public Guid Id { get; set; } = GuKey.NewId();

        /// <summary>
        /// 是否删除
        /// </summary>
        [DefaultValue(false)]
        public bool IsDel { get; set; } = false;

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; } = DateTime.Now;

        /// <summary>
        /// 删除时间
        /// </summary>
        public DateTime DeleteTime { get; set; } = InitTime.Time;

        /// <summary>
        /// 数据状态，0禁用，1可用
        /// </summary>
        [DefaultValue(1)]
        public int Status { get; set; } = 1;
    }
}