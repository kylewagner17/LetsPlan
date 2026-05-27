using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace LetsPlan.Models
{
    public class Event
    {
        private List<Event> eventGroup;

        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Title { get; set; }
        public DateTime Date { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public string CategoryType { get; set; }
        public int CreatedByUserId { get; set; }
        public DateTime LastUpdated { get; set; }
        public string GroupsJson { get; set; }


        [Ignore]
        public List<Group> Groups
        {
            get => string.IsNullOrEmpty(GroupsJson)
                ? new List<Group>()
                : JsonSerializer.Deserialize<List<Group>>(GroupsJson);

            set => GroupsJson = JsonSerializer.Serialize(value);
        }

        
    }
}
