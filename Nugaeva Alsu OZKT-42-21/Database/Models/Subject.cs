using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Nugaeva_Alsu_OZKT_42_21.Database.Models
{
    public class Subject
    {
        [JsonIgnore]
        public int SubjectId { get; set; }
        public string SubjectName { get; set; }

        /*[JsonIgnore]
        public List<Group>? Groups { get; set; }*/

        //ublic virtual ICollection<Student> Students { get; set; } = new List<Student>();
        [JsonIgnore]
        public virtual ICollection<Group> Groups { get; set; } = new List<Group>();
        public bool IsDeleted { get; set; }
    }
}