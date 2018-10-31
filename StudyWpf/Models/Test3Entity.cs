using Livet;
using System;
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
        [Required]
        public string Name { get; set; } = "def";
        private bool _IsOk;
        private bool _IsSelected;

        public bool IsOk
        {
            get { return _IsOk; }
            set
            {
                if (_IsOk == value) return;
                _IsOk = value;
                RaisePropertyChanged(nameof(IsOk));
            }
        }
        public bool IsSelected
        {
            get { return _IsSelected; }
            set
            {
                if (_IsSelected == value)
                {
                    return;
                }
                _IsSelected = value;

                RaisePropertyChanged(nameof(IsSelected));
            }
        }
        [NotMapped]
        public bool SendEnabled { get; set; }
        public ResultStatus ResultStatus { get; set; }
    }
}