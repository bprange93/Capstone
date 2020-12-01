using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PlannerProject.Models
{
    public class Chore
    {
        [Key]
        public string Name { get; set; }
        public bool isCompleted { get; set; }
    }
}
