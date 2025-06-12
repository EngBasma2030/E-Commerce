namespace Domain.Models.IdentityModule
{
    public class Address
    {
        public int Id { get; set; } // PK
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string Street { get; set; }

        public ApplicationUser User { get; set; }
        public string UserId { get; set; } = default!; // FK [Unique Index]
    }
}