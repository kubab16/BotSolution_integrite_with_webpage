#pragma checksum "C:\Users\User-Standard\Desktop\BotSolution_integrite_with_webpage\Webpage\Views\Identify\Register.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "a57704ed52bc711888f7d62c8a80785660145d7e"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Identify_Register), @"mvc.1.0.view", @"/Views/Identify/Register.cshtml")]
namespace AspNetCore
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
#line 1 "C:\Users\User-Standard\Desktop\BotSolution_integrite_with_webpage\Webpage\Views\_ViewImports.cshtml"
using Webpage;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "C:\Users\User-Standard\Desktop\BotSolution_integrite_with_webpage\Webpage\Views\_ViewImports.cshtml"
using Webpage.Models;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"a57704ed52bc711888f7d62c8a80785660145d7e", @"/Views/Identify/Register.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"6290471084a076268dd3d459d1970682844298a3", @"/Views/_ViewImports.cshtml")]
    public class Views_Identify_Register : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-controller", "", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_2 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("id", new global::Microsoft.AspNetCore.Html.HtmlString("registerForm"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_3 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("method", "post", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_4 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-controller", "Identify", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_5 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "Register", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        #line hidden
        #pragma warning disable 0649
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperExecutionContext __tagHelperExecutionContext;
        #pragma warning restore 0649
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner __tagHelperRunner = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner();
        #pragma warning disable 0169
        private string __tagHelperStringValueBuffer;
        #pragma warning restore 0169
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __backed__tagHelperScopeManager = null;
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __tagHelperScopeManager
        {
            get
            {
                if (__backed__tagHelperScopeManager == null)
                {
                    __backed__tagHelperScopeManager = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager(StartTagHelperWritingScope, EndTagHelperWritingScope);
                }
                return __backed__tagHelperScopeManager;
            }
        }
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper;
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.RenderAtEndOfFormTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper;
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#nullable restore
#line 1 "C:\Users\User-Standard\Desktop\BotSolution_integrite_with_webpage\Webpage\Views\Identify\Register.cshtml"
  
    ViewData["Title"] = "Rejestrowanie";

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n<h1>Rejestrowanie</h1>\r\n\r\n<div class=\"row\">\r\n    <div class=\"col-md-4\">\r\n        ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("form", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "a57704ed52bc711888f7d62c8a80785660145d7e5604", async() => {
                WriteLiteral("\r\n            <h4>Utwórz nowe konto.</h4>\r\n            <hr />\r\n            <div class=\"text-danger\" data-valmsg-summary=\"true\">\r\n                ");
#nullable restore
#line 13 "C:\Users\User-Standard\Desktop\BotSolution_integrite_with_webpage\Webpage\Views\Identify\Register.cshtml"
           Write(TempData["Error"]);

#line default
#line hidden
#nullable disable
                WriteLiteral(@"
            </div>
            <div class=""form-group"">
                <label for=""Input_Nick"">Nick</label>
                <input class=""form-control"" type=""text"" data-val=""true"" data-val-length=""Nick mósi zawierać od 4 do 60 znaków"" data-val-length-min=""4"" maxlength=""60"" data-val-required=""Nick jest wymagany"" id=""Input_Nick"" name=""Nick""");
                BeginWriteAttribute("value", " value=\"", 772, "\"", 797, 1);
#nullable restore
#line 17 "C:\Users\User-Standard\Desktop\BotSolution_integrite_with_webpage\Webpage\Views\Identify\Register.cshtml"
WriteAttributeValue("", 780, TempData["Nick"], 780, 17, false);

#line default
#line hidden
#nullable disable
                EndWriteAttribute();
                WriteLiteral(" />\r\n                <span class=\"text-danger field-validation-valid\" data-valmsg-for=\"Nick\" data-valmsg-replace=\"true\">");
#nullable restore
#line 18 "C:\Users\User-Standard\Desktop\BotSolution_integrite_with_webpage\Webpage\Views\Identify\Register.cshtml"
                                                                                                              Write(TempData["EroorNick"]);

#line default
#line hidden
#nullable disable
                WriteLiteral(@"</span>
            </div>
            <div class=""form-group"">
                <label for=""Input_Email"">Email</label>
                <input class=""form-control"" type=""email"" data-val=""true"" data-val-email=""Nie prawidłowy adres email."" data-val-required=""Adres email jest wymagany."" id=""Input_Email"" name=""Email""");
                BeginWriteAttribute("value", " value=\"", 1257, "\"", 1282, 1);
#nullable restore
#line 22 "C:\Users\User-Standard\Desktop\BotSolution_integrite_with_webpage\Webpage\Views\Identify\Register.cshtml"
WriteAttributeValue("", 1265, TempData["mail"], 1265, 17, false);

#line default
#line hidden
#nullable disable
                EndWriteAttribute();
                WriteLiteral(@" />
                <span class=""text-danger field-validation-valid"" data-valmsg-for=""Email"" data-valmsg-replace=""true""></span>
            </div>
            <div class=""form-group"">
                <label for=""Input_Password"">Password</label>
                <input class=""form-control"" type=""password"" data-val=""true"" data-val-length=""Hasło mósi zawierać od 6 do 40 znaków."" data-val-length-max=""40"" data-val-length-min=""6"" data-val-required=""Hasło jest wymagane."" id=""Input_Password"" maxlength=""100"" name=""Password"" />
                <span class=""text-danger field-validation-valid"" data-valmsg-for=""Password"" data-valmsg-replace=""true"">");
#nullable restore
#line 28 "C:\Users\User-Standard\Desktop\BotSolution_integrite_with_webpage\Webpage\Views\Identify\Register.cshtml"
                                                                                                                  Write(TempData["EroorPassword"]);

#line default
#line hidden
#nullable disable
                WriteLiteral(@"</span>
            </div>
            <div class=""form-group"">
                <label for=""Input_ConfirmPassword"">Confirm password</label>
                <input class=""form-control"" type=""password"" data-val=""true"" data-val-equalto=""Hasłą są nie zgodne!"" data-val-equalto-other=""Password"" id=""Input_ConfirmPassword"" name=""ConfirmPassword"" />
                <span class=""text-danger field-validation-valid"" data-valmsg-for=""ConfirmPassword"" data-valmsg-replace=""true""></span>
            </div>
            <div class=""form-group"">
                <label for=""Input_Email"">Data urodzenia*</label>
                <input class=""form-control"" type=""date"" name=""BrightDay""");
                BeginWriteAttribute("value", " value=\"", 2636, "\"", 2659, 1);
#nullable restore
#line 37 "C:\Users\User-Standard\Desktop\BotSolution_integrite_with_webpage\Webpage\Views\Identify\Register.cshtml"
WriteAttributeValue("", 2644, TempData["BD"], 2644, 15, false);

#line default
#line hidden
#nullable disable
                EndWriteAttribute();
                WriteLiteral(" />\r\n                <span class=\"text-danger\">");
#nullable restore
#line 38 "C:\Users\User-Standard\Desktop\BotSolution_integrite_with_webpage\Webpage\Views\Identify\Register.cshtml"
                                     Write(TempData["EroorBD"]);

#line default
#line hidden
#nullable disable
                WriteLiteral(@"</span>
            </div>
            <div class=""form-group"">
                <div class=""checkbox"">
                    <label for=""Input_Rule"">
                        <input type=""checkbox"" data-val=""true"" data-val-required=""Hasło jest wymagane."" id=""Input_Rule"" name=""Rule"" value=""true"" />
                        Akceptuje ");
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "a57704ed52bc711888f7d62c8a80785660145d7e11146", async() => {
                    WriteLiteral("reguramin");
                }
                );
                __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper>();
                __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
                __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Controller = (string)__tagHelperAttribute_0.Value;
                __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_0);
                __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = (string)__tagHelperAttribute_1.Value;
                __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_1);
                await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
                if (!__tagHelperExecutionContext.Output.IsContentModified)
                {
                    await __tagHelperExecutionContext.SetOutputContentAsync();
                }
                Write(__tagHelperExecutionContext.Output);
                __tagHelperExecutionContext = __tagHelperScopeManager.End();
                WriteLiteral(@"
                    </label>
                </div>
            </div>
            <div class=""form-group"">
                <div class=""checkbox"">
                    <label for=""Input_Pricacy"">
                        <input type=""checkbox"" data-val=""true"" data-val-required=""Hasło jest wymagane."" id=""Input_Pricacy"" name=""Pricacy"" value=""true"" />
                        Akceptuje ");
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "a57704ed52bc711888f7d62c8a80785660145d7e12990", async() => {
                    WriteLiteral("politykę prywatności");
                }
                );
                __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper>();
                __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
                __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Controller = (string)__tagHelperAttribute_0.Value;
                __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_0);
                __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = (string)__tagHelperAttribute_1.Value;
                __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_1);
                await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
                if (!__tagHelperExecutionContext.Output.IsContentModified)
                {
                    await __tagHelperExecutionContext.SetOutputContentAsync();
                }
                Write(__tagHelperExecutionContext.Output);
                __tagHelperExecutionContext = __tagHelperScopeManager.End();
                WriteLiteral(".\r\n                    </label>\r\n                </div>\r\n            </div>\r\n            <button id=\"registerSubmit\" type=\"submit\" class=\"btn btn-primary\">Zarejestruj się</button>\r\n        ");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.RenderAtEndOfFormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_2);
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.Method = (string)__tagHelperAttribute_3.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_3);
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.Controller = (string)__tagHelperAttribute_4.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_4);
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.Action = (string)__tagHelperAttribute_5.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_5);
            if (__Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.RouteValues == null)
            {
                throw new InvalidOperationException(InvalidTagHelperIndexerAssignment("asp-route-returnUrl", "Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper", "RouteValues"));
            }
            BeginWriteTagHelperAttribute();
#nullable restore
#line 9 "C:\Users\User-Standard\Desktop\BotSolution_integrite_with_webpage\Webpage\Views\Identify\Register.cshtml"
                                                                                                       WriteLiteral(ViewData["ReturnUrl"]);

#line default
#line hidden
#nullable disable
            __tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.RouteValues["returnUrl"] = __tagHelperStringValueBuffer;
            __tagHelperExecutionContext.AddTagHelperAttribute("asp-route-returnUrl", __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.RouteValues["returnUrl"], global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral(@"
        <p>*Pole nie wymagane</p>
    </div>
    <div class=""col-md-6 col-md-offset-2"">
        <section>
            <!--Facebook-->
            Facebook
        </section>
        <section>
            <!--Google-->
            Google
        </section>
        <section>
            <!--Discord-->
            Discord
        </section>
    </div>
</div>

");
            DefineSection("Scripts", async() => {
                WriteLiteral("\r\n    <script src=\"/Identity/lib/jquery-validation/dist/jquery.validate.js\"></script>\r\n    <script src=\"/Identity/lib/jquery-validation-unobtrusive/jquery.validate.unobtrusive.js\"></script>\r\n");
            }
            );
        }
        #pragma warning restore 1998
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
