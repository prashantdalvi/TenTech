using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TechApi.Models
{
    public class Employee
    {
        [Key]
        public int SchemeId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailId { get; set; }
        public string Mobile { get; set; }
    }
}
