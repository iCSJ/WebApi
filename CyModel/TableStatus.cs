using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CyModel
{
    public enum TableStatus
    {
        /// <summary>
        /// 空闲
        /// </summary>
        Free = 0,
        /// <summary>
        /// 使用
        /// </summary>
        Used = 1,
        /// <summary>
        /// 买单
        /// </summary>
        Checking = 2
    }
}
