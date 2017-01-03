using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FinalProject.Models
{
	public class RecievedMessages
	{

        [Key]
        public int Id { get; set; }
        [Display(Name = "Message")]
        [StringLength(100)]
        public string Body { get; set; }
        [Display(Name = "Time Recieved")]
        public DateTime timesent { get; set; }
        public bool Read { get; set; }
        [Display(Name = "To")]
        public string ReceptionId { get; set; }
        [Display(Name = "From")]
        public string SenderId { get; set; }
        public virtual List<ApplicationUser> MemberId { get; set; }

    }
}