using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FinalProject.Models
{
    public class Post
    {

        [Key]
        public int Id { get; set; }
        [StringLength(50)]
        public string Subject { get; set; }
        [DisplayName("New Post")]

        [StringLength(500)]
        public string Body { get; set; }
        [DisplayName("Date Posted")]
        public DateTime Datecreated { get; set; }
        public virtual List<Comment> CommentsList { get; set; }
        [DisplayName("Written By")]
        public virtual ApplicationUser MemberId { get; set; }

    }
}