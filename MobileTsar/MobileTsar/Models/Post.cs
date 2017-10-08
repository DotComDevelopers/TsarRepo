using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobileTsar.Models
{


  public class Post
  {
    public int Id { get; set; }
    public string Title { get; set; }
    public string ShortDescription { get; set; }
    public string Body { get; set; }
    public DateTime PostedOn { get; set; }
    public object Consultant { get; set; }
    public int ConsultantNum { get; set; }
    public string FirstName { get; set; }
  }

}
