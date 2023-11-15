using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;
namespace project_airlines.Models.User;

public class User : IdentityUser
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
	public int Id {  get; set; }


    [PersonalData]
    public override string UserName { get; set; }

    [Required(ErrorMessage = "Please enter the Email !")]
	public string Email { get; set; }


	[Required(ErrorMessage = "Please enter the Password !")]
	[DataType(DataType.Password)]
	public string Password { get; set; }

	[Required(ErrorMessage = "Confirm the password !")]
	[DataType(DataType.Password)]
	[Compare("Password")]

	public string Repwd { get; set; }

	[Required(ErrorMessage = "Select the gender !")]
	[Display(Name ="Gender")]
	public string Gender { get; set; }

	//[Required(ErrorMessage = "Upload Profile Image")]
	//[Display(Name ="Profile Image :")]
	//public string Image { get; set; } 


}
