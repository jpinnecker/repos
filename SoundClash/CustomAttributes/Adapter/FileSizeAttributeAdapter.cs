using Microsoft.AspNetCore.Mvc.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SoundClash.CustomAttributes.Adapter
{
    /// <summary>
    /// Adapter to enable custom FileSize Validation Attribute
    /// </summary>
    public class FileSizeAttributeAdapter : AttributeAdapterBase<FileSizeAttribute>
    {
        public FileSizeAttributeAdapter(FileSizeAttribute attribute,
            IStringLocalizer stringLocalizer)
            : base(attribute, stringLocalizer)
        {

        }

        public override void AddValidation(ClientModelValidationContext context)
        {
            MergeAttribute(context.Attributes, "data-val", "true");
            MergeAttribute(context.Attributes, "data-val-filesize", GetErrorMessage(context));
        }

        public override string GetErrorMessage(ModelValidationContextBase validationContext) =>
            Attribute.GetErrorMessage();
    }
}
