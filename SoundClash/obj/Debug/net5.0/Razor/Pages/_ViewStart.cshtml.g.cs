#pragma checksum "C:\Users\Yoshi\source\repos\SoundClash\Pages\_ViewStart.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "7091c65830b0329e613be026ede8a57552863778"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(SoundClash.Pages.Pages__ViewStart), @"mvc.1.0.view", @"/Pages/_ViewStart.cshtml")]
namespace SoundClash.Pages
{
    #line hidden
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.AspNetCore.Mvc.ViewFeatures;
#nullable restore
#line 1 "C:\Users\Yoshi\source\repos\SoundClash\Pages\_ViewImports.cshtml"
using Microsoft.AspNetCore.Authorization;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "C:\Users\Yoshi\source\repos\SoundClash\Pages\_ViewImports.cshtml"
using Microsoft.AspNetCore.Identity;

#line default
#line hidden
#nullable disable
#nullable restore
#line 3 "C:\Users\Yoshi\source\repos\SoundClash\Pages\_ViewImports.cshtml"
using SoundClash;

#line default
#line hidden
#nullable disable
#nullable restore
#line 4 "C:\Users\Yoshi\source\repos\SoundClash\Pages\_ViewImports.cshtml"
using SoundClash.Authorization;

#line default
#line hidden
#nullable disable
#nullable restore
#line 5 "C:\Users\Yoshi\source\repos\SoundClash\Pages\_ViewImports.cshtml"
using SoundClash.Components.Pages;

#line default
#line hidden
#nullable disable
#nullable restore
#line 6 "C:\Users\Yoshi\source\repos\SoundClash\Pages\_ViewImports.cshtml"
using SoundClash.Data;

#line default
#line hidden
#nullable disable
#nullable restore
#line 7 "C:\Users\Yoshi\source\repos\SoundClash\Pages\_ViewImports.cshtml"
using SoundClash.Models;

#line default
#line hidden
#nullable disable
#nullable restore
#line 8 "C:\Users\Yoshi\source\repos\SoundClash\Pages\_ViewImports.cshtml"
using SoundClash.Models.View;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"7091c65830b0329e613be026ede8a57552863778", @"/Pages/_ViewStart.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"7a1d8e8d0360f5ea669ac661f57cf2d7657cb3ec", @"/Pages/_ViewImports.cshtml")]
    public class Pages__ViewStart : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#nullable restore
#line 1 "C:\Users\Yoshi\source\repos\SoundClash\Pages\_ViewStart.cshtml"
  
    Layout = "_Layout";

#line default
#line hidden
#nullable disable
        }
        #pragma warning restore 1998
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public IAuthorizationService AuthorizationService { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.IModelExpressionProvider ModelExpressionProvider { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IUrlHelper Url { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IViewComponentHelper Component { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IJsonHelper Json { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<dynamic> Html { get; private set; }
    }
}
#pragma warning restore 1591
