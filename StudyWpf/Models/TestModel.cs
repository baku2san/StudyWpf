using System.ComponentModel.DataAnnotations;

namespace StudyWpf.Models
{
    public class TestModel
    {
        public enum ResultStatusDefinition {
            OK,
            NG,
            NotYet,
        }
        [Key]   // EFcore だと複合キーは、OnModelingでLamdaで記述必要
        public int Id { get; internal set; }
        public int SheetId { get; internal set; }
        public string Name { get; set; }
        public ResultStatusDefinition ResultStatus { get; set; }
    }
}