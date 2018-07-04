using Livet;
using System.ComponentModel.DataAnnotations;

namespace StudyWpf.Models
{
    public class Test2Entity: NotificationObject
    {
        public enum Test2Status
        {
            OK,
            NG,
            NotYet,
        }
        [Key]   // EFcore だと複合キーは、OnModelingでLamdaで記述必要
        public int Id { get; set; }
        public int TestModelId { get; set; }
        public string Name { get; set; }
        public Test2Status ResultStatus { get; set; }
    }
}