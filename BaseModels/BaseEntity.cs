using GalaSoft.MvvmLight;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseModels
{
    public abstract class BaseEntity: INotifyPropertyChanged
    {
        /// <summary>
        /// 唯一自增长Id
        /// </summary>
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [JsonProperty(Order = 0)]
        public int Id { get; set; }
        /// <summary>
        /// 修改人
        /// </summary>
        [StringLength(60)]
        [JsonProperty(Order = 3)]
        public string Modifier { get; set; }
        /// <summary>
        /// 修改时间
        /// </summary>
        [Column(TypeName = "timestamp")]
        [JsonProperty(Order = 4)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public DateTime? ModifyTime { get; set; }
        /// <summary>
        /// 创建者
        /// </summary>
        [StringLength(60)]
        [JsonProperty(Order = 1)]
        public string Creator { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        [Column(TypeName = "timestamp")]
        [JsonProperty(Order = 2)]
        public DateTime? CreateTime { get; set; }
        /// <summary>
        /// 是否逻辑删除
        /// </summary>
        public bool? IsDeleted { get; set; }
        /// <summary>
        /// 排序
        /// </summary>
        public int? SortId { get; set; }
        /// <summary>
        /// 是否同步(如果需要与远程服务器同步数据)
        /// </summary>
        public bool? IsSync { get; set; }
        /// <summary>
        /// 数据状态，默认Null，新增1，修改2，删除-1
        /// </summary>
        /// 
        [NotMapped]
        public DataState? DataState { get; set; }
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                var e = new PropertyChangedEventArgs(propertyName);
                handler(this, e);
            }
        }
    }
    public enum DataState
    {
        Default = 0, Add = 1, Mod = 2, Del = -1
    }
}
