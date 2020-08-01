using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MadPay724.Data.Models
{
    public class BankCard : BaseEntity<string>
    {
        public BankCard()
        {
            Id = Guid.NewGuid().ToString();
            Created = DateTime.Now;
            Modified = DateTime.Now;
        }
        [Required]
        public string BankName { get; set; }
        public string Shaba { get; set; }
        [Required]
        [Range(16, 16)]
        public string CardNumber { get; set; }
        [Required]
        [StringLength(2,MinimumLength = 2)]
        public string ExpireMonth { get; set; }
        [Required]
        [StringLength(2, MinimumLength = 2)]
        public string ExpireYear { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }
    }
}
