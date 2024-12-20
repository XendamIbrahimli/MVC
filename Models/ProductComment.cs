﻿using System.ComponentModel.DataAnnotations;

namespace UniqloMVC.Models
{
    public class ProductComment:BaseEntity
    {
        [MaxLength(200)]
        public string? Comment { get; set; }
        public int ProductId { get; set; }
        public Product? Product { get; set; }
        public string? UserId {  get; set; }
        public User? User { get; set; }
    }
}
