// ReSharper disable VirtualMemberCallInConstructor

namespace Tasty.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    public interface IPersonalUserData
    {
        int? Age { get; set; }

        [Required]
        string FirstName { get; set; }

        [Required]
        string LastName { get; set; }

        string Residence { get; set; }
    }
}
