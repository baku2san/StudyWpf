using Livet;
using System.ComponentModel.DataAnnotations;
using static StudyWpf.Models.Constants;

namespace StudyWpf.Models
{
    public class Test2Entity: NotificationObject
    {
        [Key]   // EFcore だと複合キーは、OnModelingでLamdaで記述必要
        public int Id { get; set; }
        public int TestModelId { get; set; }
        public string Name { get; set; }
        public ResultStatus ResultStatus { get; set; }
    }
}