using Microsoft.AspNetCore.Mvc.DataAnnotations;
using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SoundClash.CustomAttributes.Adapter
{
    /// <summary>
    /// Adapter to realize custom Validation Attributes
    /// </summary>
    public class CustomValidationAttributeAdapterProvider : IValidationAttributeAdapterProvider
    {
        private readonly IValidationAttributeAdapterProvider baseProvider =
            new ValidationAttributeAdapterProvider();

        public IAttributeAdapter GetAttributeAdapter(ValidationAttribute attribute,
            IStringLocalizer stringLocalizer)
        {
            if (attribute is SoundTypeAttribute soundTypeAttribute)
            {
                return new SoundTypeAttributeAdapter(soundTypeAttribute, stringLocalizer);
            }
            else if (attribute is FileSizeAttribute fileSizeAttribute)
            {
                return new FileSizeAttributeAdapter(fileSizeAttribute, stringLocalizer);
            }
            else if (attribute is FileExtensionAttribute fileExtensionAttribute)
            {
                return new FileExtensionAttributeAdapter(fileExtensionAttribute, stringLocalizer);
            }
            else if (attribute is FileSignatureAttribute fileSignatureAttribute)
            {
                return new FileSignatureAttributeAdapter(fileSignatureAttribute, stringLocalizer);
            }

            return baseProvider.GetAttributeAdapter(attribute, stringLocalizer);
        }
    }
}
