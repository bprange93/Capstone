using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PlannerProject.Models
{
    public class ParentChildJunction
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("ParentId")]
        public int ParentId { get; set; }
        [ForeignKey("ChildId")]
        public int ChildId { get; set; }
    }
}
