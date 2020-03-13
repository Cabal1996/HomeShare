using SQLite;

/*
 * Base class for data base table item 
 */

namespace Homeshare.Model
{
    public class TableItem
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
    }
}
