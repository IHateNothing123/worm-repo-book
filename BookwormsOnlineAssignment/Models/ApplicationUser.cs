using Microsoft.AspNetCore.Identity;

namespace BookwormsOnlineAssignment.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string CreditCard { get; set; }
        public string BillingAddress { get; set; }
        public string ShippingAddress { get; set; }
        // public string Photo { get; set; }
	}
}
