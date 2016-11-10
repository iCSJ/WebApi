using BaseModels;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CyModel
{
    [Table("Tables")]
    public class Tables : CyEntity
    {
        public string Name { get; set; }
        public int PosId { get; set; }
        public int SeatNumber { get; set; }
        public decimal RoomFee { get; set; }
        public int Status { get; set; }
        /// <summary>
        /// 人均消费
        /// </summary>
        public decimal PersonConsume { get; set; }
        /// <summary>
        /// 最低消费
        /// </summary>
        public decimal LowestConsume { get; set; }
        [JsonIgnore]
        public virtual Pos Pos { get; set; }
    }
}
