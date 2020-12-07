using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PlannerProject.Models
{
    public class Reminder
    {
        [Key]
        public int Id { get; set; }
        public string reminder { get; set; }
        [ForeignKey ("parentId")]
        public int parentId { get; set; }
    }
}
