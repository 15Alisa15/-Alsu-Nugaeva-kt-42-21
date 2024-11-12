using Nugaeva_Alsu_OZKT_42_21.Database.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Nugaeva_Alsu_OZKT_42_21.Database.Models
{
        public class Group
        {
       
        public int GroupId { get; set; }
            public string GroupName { get; set; }

        public int? SubjectId { get; set; }
     /*   public int? StudentId { get; set; }*/

        [JsonIgnore]
        public Subject? Subject { get; set; }
        /*[JsonIgnore]
        public Student? Student { get; set; }*/
        /*public virtual ICollection<Student> Students { get; set; } = new List<Student>();
        
       public virtual ICollection<Subject> Subjects { get; set; } = new List<Subject>();*/

        public bool IsDeleted { get; set; }

            public bool IsValidGroupName()
            {
                return Regex.Match(GroupName, @"\D*-\d*-\d\d").Success;
            }
        }
    }