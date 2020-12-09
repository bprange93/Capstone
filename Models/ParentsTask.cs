using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PlannerProject.Models
{
    public class ParentsTask
    {
        [Key]
        public int Id { get; set; }
        public string Reminders { get; set; }
        public string Type { get; set; }
        [ForeignKey("Child")]
        public int ChildId { get; set; }
        public Child Child { get; set; }
        public string Description { get; set; }
        public string Title { get; set; }
    }
}
