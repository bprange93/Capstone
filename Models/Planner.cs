using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PlannerProject.Models
{
    public class Planner
    {
        [Key]
        public int Id { get; set; }
        public string DayOfWeek { get; set; }
        public string Reward { get; set; }
        public int TimeOfDay { get; set; }
    }
}
