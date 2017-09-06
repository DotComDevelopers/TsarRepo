using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobileTsar.Models
{
  public class ClientPassword
  {

    public int ClientPasswordId { get; set; }

    public virtual Client Client { get; set; }
    public int id { get; set; }
    public string Operator { get; set; }
    public string OperatorPassword { get; set; }
 
    public string CompanyPassword { get; set; }
  }
}
