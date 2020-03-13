

using System;

namespace Homeshare.Model 
{
    public class CostTable : TableItem
    {
        public DateTime Date { get; set; }
        public int CostItemId { get; set; }
        public float Value { get; set; }
    }
}
