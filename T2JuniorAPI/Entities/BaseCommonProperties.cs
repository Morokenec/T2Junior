using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace T2JuniorAPI.Entities
{
    public abstract class BaseCommonProperties
    {
        [Key]
        [Column("Id", Order = 0)]
        [Description("Идентификатор записи в БД")]
        public Guid Id { get; set; }

        [Column("CreationDate", Order = 1)]
        [Description("Дата создания записи в БД")]
        public DateTime CreationDate { get; set; } = DateTime.Now;

        [Column("UpdateDate", Order = 2)]
        [Description("Дата обновления записи в БД")]
        public DateTime UpdateDate { get; set; } = DateTime.Now;

        [Column("IsDelete", Order = 3)]
        [Description("Флаг удаления записи из БД")]
        public bool IsDelete { get; set; } = false;
    }
}
