using Livet;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static StudyWpf.Models.Constants;

namespace StudyWpf.Models
{
    public class Test2Entity: NotificationObjectExtend
    {
        [Key]   // EFcore だと複合キーは、OnModelingでLamdaで記述必要
        public int Id { get; set; }
        public int TestModelId { get; set; }
        public string Name { get; set; }
        private bool _IsOk;
        public bool IsOk
        {
            get { return _IsOk; }
            set { this.SetProperty(ref _IsOk, value); }
        }

        [NotMapped]
        public bool SendEnabled { get; set; }
        public ResultStatus ResultStatus { get; set; }
    }
}