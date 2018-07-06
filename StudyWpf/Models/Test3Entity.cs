using Livet;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static StudyWpf.Models.Constants;

namespace StudyWpf.Models
{
    public class Test3Entity : NotificationObject
    {
        [Key]   // EFcore だと複合キーは、OnModelingでLamdaで記述必要
        public int Id { get; set; }
        public int Test2Id { get; set; }
        public string Name { get; set; }
        public bool IsOk { get; set; }
        [NotMapped]
        public bool SendEnabled { get; set; }
        public ResultStatus ResultStatus { get; set; }
    }
}