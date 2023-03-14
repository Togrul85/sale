using System.ComponentModel.DataAnnotations;

namespace FrontToBack2.ViewModels
{
    public class CategoryCreateVM
    {
        [Required, MaxLength(50)]

        public string Name { get; set; }

        [Required, MinLength(5)]
        public string Description { get; set; }
    }
}

