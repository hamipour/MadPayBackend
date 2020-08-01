using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MadPay724.Data.Models
{
    public class Photo : BaseEntity<string>
    {
        public Photo()
        {
            Id = Guid.NewGuid().ToString();
            Created = DateTime.Now;
            Modified = DateTime.Now;
        }
        [Required]
        public string Url { get; set; }
        public string Description { get; set; }
        public string Alt { get; set; }
        [Required]
        public short IsMain { get; set; }

        [Required]
        public string UserId { get; set; }
        public User User { get; set; }
    }
}
