using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PlannerProject.Models
{
    public class ChoreItem
    {
        [Key]
        public string Name { get; set; }
        public bool isCompleted { get; set; }
        [ForeignKey("ChoreList")]
        public int ChoreListId { get; set; }
        public ChoreList ChoreList { get; set; }
        public string EndTime { get; set; }
        public string Description { get; set; }
        
    
    }
}
