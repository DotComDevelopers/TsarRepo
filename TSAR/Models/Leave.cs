using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;

namespace TSAR.Models
{
    public class Leave
    {

        [Key]
        //[Required]
        [Display(Name = "Leave Id")]
        public int LeaveId { get; set; }

        //[Required]
        [Display(Name = "Consultant Name")]
        public string FirstName { get; set; }

        //[Required]
        [Display(Name = "Approved By")]
        public string ApprovedBy { get; set; }

        [Required]
        [Display(Name = "Confirmed?")]
        public bool IsConfirmed { get; set; }

        [Required]
        [Display(Name = "Comments")]
        public string LeaveDecsription { get; set; }

        [Required]
        [Display(Name = "Leave Balance")]
        public int AccumulatedLeave { get; set; }

        public int AllocatedLeave { get; set; }

        [Display(Name = "No. of Days")]
        public int LeaveCount { get; set; }

        [Required]
        [Display(Name = "Leave Date")]
        [DateMustBeEqualOrGreaterThanCurrentDate]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime LeaveDate { get; set; }

        [Required]
        [Display(Name = "Return Date")]
        [DateMustBeEqualOrGreaterThanCurrentDate]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime ReturnDate { get; set; }


    //public virtual LeaveType LeaveType { get; set; }
    ////[Display(Name = "Leave Type")]

    [Display(Name = "Type of Leave")]
    public string LeaveTypeName { get; set; }

    public virtual Consultant Consultant { get; set; }
    public int ConsultantNum { get; set; }

    //public int Count(Func<object, object> p)
    //{
    //  throw new NotImplementedException();
    //}
  }


    //Created custom validation attribute to validate the date chosen for leave
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    public sealed class DateMustBeEqualOrGreaterThanCurrentDate : ValidationAttribute, IClientValidatable
    {

        private const string DefaultErrorMessage = "Date selected {0:dd/MM/yyyy} must be on or after today";

        public DateMustBeEqualOrGreaterThanCurrentDate()
            : base(DefaultErrorMessage)
        {
        }

        public override string FormatErrorMessage(string name)
        {
            return string.Format(DefaultErrorMessage, name);
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            // break >>>>>>>> when editing leave
            var dateSelected = (DateTime)value;
            if (dateSelected < DateTime.Today)
            {
                var message = FormatErrorMessage(dateSelected.ToShortDateString());
                return new ValidationResult(message);
            }
            return null;
        }

        public IEnumerable<ModelClientValidationRule> GetClientValidationRules(ModelMetadata metadata, ControllerContext context)
        {
            var rule = new ModelClientCustomDateValidationRule(FormatErrorMessage(metadata.DisplayName));
            yield return rule;
        }
    }

    public sealed class ModelClientCustomDateValidationRule : ModelClientValidationRule
    {

        public ModelClientCustomDateValidationRule(string errorMessage)
        {
            ErrorMessage = errorMessage;
            ValidationType = "datemustbeequalorgreaterthancurrentdate";
        }
    }
}
