using System.ComponentModel.DataAnnotations;

namespace ConnectedOffice.Auth
{
	public class RegisterModel
	{
		[Required(ErrorMessage = "Username must be specified")]
		public String? Username { get; set; }
		[Required(ErrorMessage = "A password must be specified")]
		[DataType(DataType.Password)]
		public String? Password { get; set; }
	}
}
