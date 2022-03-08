using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace Commander.API.Models
{
    public class BaseModel
    {
        [DisallowNull]
        [Required, MinLength(1)]
        public int Id { get; set; }
    }
}
