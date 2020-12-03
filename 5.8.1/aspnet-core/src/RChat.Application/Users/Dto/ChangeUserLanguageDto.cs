using System.ComponentModel.DataAnnotations;

namespace RChat.Users.Dto
{
    public class ChangeUserLanguageDto
    {
        [Required]
        public string LanguageName { get; set; }
    }
}