using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HmsLibrary.Data.Model;

public class Role : BaseEntity
{
    [Required, MaxLength(40)]
    public string RoleName { get; set; }
}