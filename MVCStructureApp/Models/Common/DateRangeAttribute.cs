using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVCStructureApp.Models.Common
{
    public class DateRangeAttribute : RangeAttribute, IClientValidatable
    {
        public DateRangeAttribute(string minimumValue)
            : base(typeof(DateTime), minimumValue, DateTime.Now.ToShortDateString())
        {

        }

        public IEnumerable<ModelClientValidationRule> GetClientValidationRules(ModelMetadata metadata, ControllerContext context)
        {
            yield return new ModelClientValidationRule
            {
                ErrorMessage = this.ErrorMessage,
                ValidationType = "daterange"
            };
        }
    }
}