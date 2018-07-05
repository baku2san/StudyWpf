using System.ComponentModel.DataAnnotations;
using Livet;
using static StudyWpf.Models.Constants;

namespace StudyWpf.Models
{
    public class TestEntity: NotificationObject
    {
        [Key]   // EFcore だと複合キーは、OnModelingでLamdaで記述必要
        public int Id { get; set; }
        public int SheetId { get; set; }
        public string Name { get; set; }
        public ResultStatus ResultStatus { get; set; }
    }
}