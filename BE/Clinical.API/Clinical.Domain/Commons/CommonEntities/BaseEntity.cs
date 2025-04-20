using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinical.Domain.Commons.CommonEntities
{
    public class BaseEntity
    {
        public int Id { get; set; }
        public DateTime? ModifyTime { get; set; }
        public int? ModifyBy { get; set; }
        public DateTime? CreateTime { get; set; }
        public int? CreateBy { get; set; }
    }
}
