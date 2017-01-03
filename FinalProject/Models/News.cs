using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FinalProject.Models
{
    public class News
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(50)]
        public string Subject { get; set; }
        [StringLength(500)]
        public string Body { get; set; }
        [DisplayName("Date Posted")]
        public DateTime DatePosted { get; set; }
        public DateTime EventDate { get; set; }
        [Required]
        [DisplayName("Event")]
        public bool EventOrNews { get; set; }
    }
}