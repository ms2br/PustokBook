﻿using PustokBook.Models;
using System.ComponentModel.DataAnnotations;

namespace PustokBook.Areas.Admin.ViewModels.Authors
{
    public class AdminAuthorVM
    {
        public int Id { get; init; }

        [MinLength(3), MaxLength(25)]
        public string FirstName { get; set; }

        [MinLength(3), MaxLength(25)]
        public string LastName { get; set; }

        public List<AuthorBook>? AuthorBook { get; set; }
        public bool IsDeleted { get; set; } = false;
    }
}