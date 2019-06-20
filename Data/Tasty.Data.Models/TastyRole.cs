// ReSharper disable VirtualMemberCallInConstructor

namespace Tasty.Data.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    using Microsoft.AspNetCore.Identity;
    using Tasty.Data.Common.Models;

    public class TastyRole : IdentityRole, IAuditInfo, IDeletableEntity
    {
        public TastyRole()
            : this(null)
        {
        }

        public TastyRole(string name)
            : base(name)
        {
            this.Id = Guid.NewGuid().ToString();
        }

        [Required]
        public DateTime CreatedOn { get; set; }

        public DateTime? ModifiedOn { get; set; }

        public bool IsDeleted { get; set; }

        public DateTime? DeletedOn { get; set; }
    }
}
