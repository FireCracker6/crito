using System.ComponentModel.DataAnnotations;

namespace Crito.Contexts.Models;

public class NewsLetterForm
{
    [Required]
    public string Email { get; set; } = null!;
}
