using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CashAccountingRepoMSSQL {
    internal class ColumnDescription {

        public string ColumnName { get; set; }
        public bool IsIdentity { get; set; }
        public int maxLenght { get; set; }
        public byte precision { get; set; }
        public byte scale { get; set; }

    }
}
