namespace MobileTsar.Models
{

    public class Consultant
    {

        public int ConsultantNum { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string FullName { get; set; }
        
        public string Gender { get; set; }

        public int ContactNumber { get; set; }

        public string ConsultantAddress { get; set; }


        public string ConsultantType { get; set; }

        public virtual Commission Commission { get; set; }

        public int CommissionId { get; set; }


        public int LeaveBalance { get; set; }

        //13/06/17

        public int AnnualLeaveBalance { get; set; }


        public int MaternityLeaveBalance { get; set; }


        public int SickLeaveBalance { get; set; }

        public int FamilyResponsibilityBalance { get; set; }

        public int PaternityLeaveBalance { get; set; }


        //[EmailAddress]
        public string ConsultantUserName { get; set; }

        public string Email { get; set; }


        public string Password { get; set; }



    }
}