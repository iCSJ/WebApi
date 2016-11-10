namespace CyModel
{
    using BaseModels;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("Option")]
    public partial class Option : CyEntity
    {
        [StringLength(255)]
        public string Key { get; set; }

        [StringLength(255)]
        public string Value { get; set; }
        [StringLength(255)]
        public string Value1 { get; set; }
        [StringLength(255)]
        public string Value2 { get; set; }
    }
}
