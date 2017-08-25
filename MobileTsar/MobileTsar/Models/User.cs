using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobileTsar.Models
{
  public class User
  {
    public string Email { get; set; }
    public bool HasRegistered { get; set; }
    public string LoginProvider { get; set; }

  }
}
