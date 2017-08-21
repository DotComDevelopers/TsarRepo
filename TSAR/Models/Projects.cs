using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TSAR.Models
{
    public class Projects
    {
        [Key]
        public int ProjectId { get; set; }

        public string ProjectName { get; set; }
        public virtual Client Client { get; set; }
        public int Id { get; set; }

        public virtual Consultant Consultant { get; set; }
        public int ConsultantNum { get; set; }
        // public List<Consultant> consultants { get; set; }


        [Required]
        [Display(Name = "Start Date")]
        [DateMustBeEqualOrGreaterThanCurrentDate]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime StartDate { get; set; }

        [Required]
        [Display(Name = "End Date")]
        [DateMustBeEqualOrGreaterThanCurrentDate]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime EndDate { get; set; }
        public static List<Projects> projects { get; internal set; }






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
}