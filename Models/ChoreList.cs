using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PlannerProject.Models
{
    public class ChoreList
    {   
        
            [Key]
            public int Id { get; set; }
        [Display(Name = "Day of the Week")]
        public string DayOfWeek { get; set; }
            public string Reward { get; set; }
            public string Title { get; set; }
            public string Comment { get; set; }
            [ForeignKey("Child")]
            public int ChildId { get; set; }
            public Child Child { get; set; }
        
    }
}
