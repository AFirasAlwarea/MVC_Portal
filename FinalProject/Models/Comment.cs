using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FinalProject.Models
{
    public class Comment
    {
        [Key]
        public int Id { get; set; }
        [StringLength(500)]
        [Required]
        [DisplayName("Comment")]
        public string Body { get; set; }
        [DisplayName("Writer")]
        public string WriterId { get; set; }
        public DateTime DateComment { get; set; }
        [DisplayName("Original Post")]
        public virtual Post Postobj { get; set; }
    }
}