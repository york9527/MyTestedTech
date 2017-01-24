using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using System.Web;

namespace FormAuthenticationTest.Models
{
    public class User
    {
        public User() 
        {
            Roles=new List<Role>();
            Status = 1;//1正常、2禁用
        }

        [Key,DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UserId { get; set; }
        [Display(Name = "用户名")]
        [Required,StringLength(30,MinimumLength =3 )]
        public string UserName { get; set; }
        [Display(Name="密码")]
        [Required,StringLength(30,MinimumLength = 6,ErrorMessage = "The length of password must between 6 and 30")]
        public string Password { get; set; }
        [Required,Display(Name="邮箱")]
        [RegularExpression(@"(\w|\.)+@(\w|\.)+", ErrorMessage = "Email is invalid")]
        public string Email { get; set; }
        [DataType("byte")]
        public int Status { get; set; }
        public DateTime LastActiveDate { get; set; }
        
        public List<Role> Roles { get; set; } 
    }

    public class Role
    {
        public Role()
        {
            Users=new List<User>();
        }

        [Key,DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int RoleId { get; set; }
        [Required,StringLength(30,MinimumLength = 3,ErrorMessage = "length must between 3 and 30")]
        public string RoleName { get; set; }
        [MaxLength(100)]
        public string Description { get; set; }
        [Required]
        public DateTime CreDate { get; set; }
        public DateTime UpdDate { get; set; }

        public List<User> Users { get; set; } 
    }

    public class Book
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public string Isbn { get; set; }
        public string Summary { get; set; }
        public string Author { get; set; }
        public string Thumbnail { get; set; }
        public double Price { get; set; }
        public DateTime Published { get; set; }
    }
}