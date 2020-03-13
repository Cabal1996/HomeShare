
/*
 * Data base item representing subject of shearing
 */

namespace Homeshare.Model
{
    public class Sharable : TableItem
    {
        public string Name { get; set; }
        public string Type { get; set; }
        public string Periodicity { get; set; }
        public string Price { get; set; }
    }
}
