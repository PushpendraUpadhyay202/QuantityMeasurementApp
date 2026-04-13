namespace ModelLayer.DTOs
{
    public class SignupRequestDto
    {
        public string FirstName {get; set;} = String.Empty;
        public string LastName {get; set;} = String.Empty;
        public string Email {get; set;} = String.Empty;
        public string Password {get; set;} = String.Empty;
    }
}