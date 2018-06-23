namespace StudyWpf.Models
{
    public class TestModel
    {
        public enum ResultStatusDefinition {
            OK,
            NG,
            NotYet,
        }
        public int Id { get; internal set; }
        public int SheetId { get; internal set; }
        public string Name { get; set; }
        public ResultStatusDefinition ResultStatus { get; set; }
    }
}