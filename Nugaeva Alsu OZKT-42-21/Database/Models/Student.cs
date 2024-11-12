using Nugaeva_Alsu_OZKT_42_21.Database.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

public class Student
{
    [JsonIgnore]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int StudentId { get; set; }

    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string MiddleName { get; set; }
   
    public int? GroupId { get; set; }
    [JsonIgnore]
    public Group? Group { get; set; }

    /*[JsonIgnore]
    public virtual Group? Group { get; set; }*/
    //ublic virtual ICollection<Subject> Subjects { get; set; } = new List<Subject>();
    /*public virtual ICollection<Group> Groups { get; set; } = new List<Group>();*/

    // public List<Subject>? Subjects { get; set; }
    public bool IsDeleted { get; set; }
}


