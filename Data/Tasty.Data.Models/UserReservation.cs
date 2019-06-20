namespace Tasty.Data.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    using Tasty.Data.Common.Models;

    public class UserReservation : BaseDeletableModel<string>, IAuditInfo
    {
        //TASK-> ADD add-migration UserReservation
        public UserReservation()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        [Required]
        public DateTime Date { get; set; }

        [Required]
        public int PeopleCount { get; set; }

        [Required]
        public string Hour { get; set; }

        [Required]
        public string UserId { get; set; }

        public TastyUser User { get; set; }
    }
}
