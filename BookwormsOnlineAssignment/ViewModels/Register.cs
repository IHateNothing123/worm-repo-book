using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace BookwormsOnlineAssignment.ViewModels
{
    public class Register
    {
        [Required]
        [DataType(DataType.EmailAddress)]
        [EmailAddress(ErrorMessage = "Invalid email address")]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Compare(nameof(Password),
            ErrorMessage = "Password and confirmation password does not match")]
        public string ConfirmPassword { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [RegularExpression(@"^[a-zA-Z]+(([',. -][a-zA-Z ])?[a-zA-Z]*)*$",
            ErrorMessage = "Invalid first name format")]
        public string FirstName { get; set; }

		[Required]
		[DataType(DataType.Text)]
        [RegularExpression(@"^[a-zA-Z]+(([',. -][a-zA-Z ])?[a-zA-Z]*)*$",
            ErrorMessage = "Invalid last name format")]
        public string LastName { get; set; }

        [Required]
        [DataType(DataType.CreditCard)]
        [RegularExpression(@"^\b(?:\d[ ]*?){16}\b$",
            ErrorMessage = "Invalid credit card number")]
        public string CreditCard { get; set; }

        [Required]
        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"^[89]\d{7}$",
            ErrorMessage = "Phone number must be a 8-digit Singapore number")]
        public string PhoneNumber { get; set; }

        [Required]
        [DataType(DataType.Text)]
		public string BillingAddress { get; set; }

        [Required]
        [DataType(DataType.Text)]
        public string ShippingAddress { get; set; }
	}
}
