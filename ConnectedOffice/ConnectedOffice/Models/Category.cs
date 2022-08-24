using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using ConnectedOffice.Models;

namespace ConnectedOffice.Models
{
    public partial class Category
    {
        public Guid CategoryId { get; set; }
        public string CategoryName { get; set; } = null!;
        public string? CategoryDescription { get; set; }
        public DateTime DateCreated { get; set; }
    }
}
