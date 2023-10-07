using System.ComponentModel.DataAnnotations;

namespace Crito.Contexts.Models;

public class NewsLetterEntity
{
    [Key]
    public string Email { get; set; } = null!;
}
