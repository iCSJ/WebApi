using BaseModels;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CyModel
{
    [Table("Pos")]
    public class Pos : CyEntity
    {
        public string Name { get; set; }
        public virtual List<Tables> Tables { get; set; }
    }
}
