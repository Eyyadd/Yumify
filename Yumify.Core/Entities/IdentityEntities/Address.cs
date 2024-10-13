using System.ComponentModel.DataAnnotations.Schema;

namespace Yumify.Core.Entities.IdentityEntities
{
    public class Address : BaseEntity
    {
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string City { get; set; } = null!;
        public string Street { get; set; } = null!;
        public string Country { get; set; } = null!;

        public ApplicationUser? User { get; set; }

        [ForeignKey(nameof(User))]
        public string ApplicationUserId { get; set; } = null!;
    }
}