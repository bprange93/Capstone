using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PlannerProject.Models
{
    public class ParentItem
    {
        [Key]
        public string Name { get; set; }
        [ForeignKey("ParentsTask")]
        public int ParentsTaskId { get; set; }
        public ParentsTask ParentsTask { get; set; }
    }
}
