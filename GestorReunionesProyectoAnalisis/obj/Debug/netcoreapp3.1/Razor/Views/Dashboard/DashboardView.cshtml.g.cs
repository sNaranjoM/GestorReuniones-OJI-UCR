#pragma checksum "C:\Users\Usuario\Desktop\GR Presentación Final\GestorReunionesAnalisis-luis\GestorReunionesProyectoAnalisis\Views\Dashboard\DashboardView.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "446748a12f2749850e3b28ebf227f6ebb4607d8c"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Dashboard_DashboardView), @"mvc.1.0.view", @"/Views/Dashboard/DashboardView.cshtml")]
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
#line 1 "C:\Users\Usuario\Desktop\GR Presentación Final\GestorReunionesAnalisis-luis\GestorReunionesProyectoAnalisis\Views\_ViewImports.cshtml"
using GestorReunionesProyectoAnalisis;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "C:\Users\Usuario\Desktop\GR Presentación Final\GestorReunionesAnalisis-luis\GestorReunionesProyectoAnalisis\Views\_ViewImports.cshtml"
using GestorReunionesProyectoAnalisis.Models;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"446748a12f2749850e3b28ebf227f6ebb4607d8c", @"/Views/Dashboard/DashboardView.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"987adce823cd3fb67776dff44770fcf9d85b8ca2", @"/Views/_ViewImports.cshtml")]
    public class Views_Dashboard_DashboardView : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<String>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("rel", new global::Microsoft.AspNetCore.Html.HtmlString("stylesheet"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("type", new global::Microsoft.AspNetCore.Html.HtmlString("text/css"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_2 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("href", new global::Microsoft.AspNetCore.Html.HtmlString("~/css/script.css"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_3 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("href", new global::Microsoft.AspNetCore.Html.HtmlString("~/css/moduloAdministrador.css"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_4 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("href", new global::Microsoft.AspNetCore.Html.HtmlString("~/css/bootstrap.css"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_5 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("href", new global::Microsoft.AspNetCore.Html.HtmlString("~/lib/bootstrap/dist/css/bootstrap.min.css"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_6 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("href", new global::Microsoft.AspNetCore.Html.HtmlString("~/css/style.css"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_7 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("form-signin"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_8 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-controller", "Dashboard", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_9 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "CargarDashboard", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_10 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("method", "POST", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
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
        private global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper;
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper;
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.RenderAtEndOfFormTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#nullable restore
#line 1 "C:\Users\Usuario\Desktop\GR Presentación Final\GestorReunionesAnalisis-luis\GestorReunionesProyectoAnalisis\Views\Dashboard\DashboardView.cshtml"
  
    ViewData["Title"] = "DashboardView";

    if (ViewBag.PermisoUsuario == "1")
        {
            Layout = "_Layout";
        }
        else if (ViewBag.PermisoUsuario == "2")
        {
            Layout = "LayoutUsuarioEstandar";
        }
        else if (ViewBag.PermisoUsuario == "3")
        {
            Layout = "LayoutUsuarioCatalogos";
        }

        if (ViewBag.PermisoUsuario == null)
        {

#line default
#line hidden
#nullable disable
            WriteLiteral("            <script>\n                redireccion(\'/Usuario/MuestraLoginUsuario\');\n            </script>\n");
#nullable restore
#line 22 "C:\Users\Usuario\Desktop\GR Presentación Final\GestorReunionesAnalisis-luis\GestorReunionesProyectoAnalisis\Views\Dashboard\DashboardView.cshtml"
        }


#line default
#line hidden
#nullable disable
            WriteLiteral("\n");
            WriteLiteral("\n<script src=\"https://cdnjs.cloudflare.com/ajax/libs/Chart.js/2.9.3/Chart.min.js\" integrity=\"sha512-s+xg36jbIujB2S2VKfpGmlC3T5V2TF3lY48DX7u2r9XzGzgPsa6wTpOQA7J9iffvdeBN0q9tKzRxVxw1JviZPg==\" crossorigin=\"anonymous\"></script>\n\n");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("link", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.SelfClosing, "446748a12f2749850e3b28ebf227f6ebb4607d8c8698", async() => {
            }
            );
            __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_0);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_1);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_2);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\n");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("link", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.SelfClosing, "446748a12f2749850e3b28ebf227f6ebb4607d8c9893", async() => {
            }
            );
            __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_0);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_1);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_3);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\n");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("link", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.SelfClosing, "446748a12f2749850e3b28ebf227f6ebb4607d8c11088", async() => {
            }
            );
            __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_0);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_1);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_4);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\n");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("link", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.SelfClosing, "446748a12f2749850e3b28ebf227f6ebb4607d8c12284", async() => {
            }
            );
            __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_0);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_1);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_5);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\n");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("link", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagOnly, "446748a12f2749850e3b28ebf227f6ebb4607d8c13480", async() => {
            }
            );
            __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_0);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_1);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_6);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral(@"


<section id=""contenido"">
    <section id=""principal"">


        <div class=""d-md-flex flex-md-equal w-100 my-md-3 pl-md-3"">

            <div class=""bg-light mr-md-3 pt-3 px-3 pt-md-5 px-md-5 text-center overflow-hidden"">

                <div class=""signup-form"">
                    ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("form", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "446748a12f2749850e3b28ebf227f6ebb4607d8c14974", async() => {
                WriteLiteral(@"
                        <h3>Dashboard General</h3>
                        <p class=""hint-text"">Espacio para buscar informacion sobre las reuniones segun dos rangos de fechas. </p>

                        <div class=""form-group"">
                            <input type=""date"" id=""fechaInicio"" name=""fechaInicio"" class=""form-control"" placeholder=""Fecha""");
                BeginWriteAttribute("required", " required=\"", 1884, "\"", 1895, 0);
                EndWriteAttribute();
                WriteLiteral(">\n                        </div>\n\n                        <div class=\"form-group\">\n                            <input type=\"date\" id=\"fechaFinal\" name=\"fechaFinal\" class=\"form-control\" placeholder=\"Fecha\"");
                BeginWriteAttribute("required", " required=\"", 2100, "\"", 2111, 0);
                EndWriteAttribute();
                WriteLiteral(@">
                        </div>

                        <div class=""form-group""> <button type=""submit"" class=""btn btn-outline-secondary"">Ingresar</button> </div>
                        <div class=""form-group""><span id=""resultadoRespuesta""></span></div>
");
#nullable restore
#line 60 "C:\Users\Usuario\Desktop\GR Presentación Final\GestorReunionesAnalisis-luis\GestorReunionesProyectoAnalisis\Views\Dashboard\DashboardView.cshtml"
                         if (ViewBag.INVALIDO != null)
                        {

#line default
#line hidden
#nullable disable
                WriteLiteral("                            <div class=\"alert alert-danger\" id=\"al1\" role=\"alert\" style=\"display: block; text-align:center\">\n                                <h6> ");
#nullable restore
#line 63 "C:\Users\Usuario\Desktop\GR Presentación Final\GestorReunionesAnalisis-luis\GestorReunionesProyectoAnalisis\Views\Dashboard\DashboardView.cshtml"
                                Write(ViewBag.INVALIDO);

#line default
#line hidden
#nullable disable
                WriteLiteral("</h6>\n                            </div>\n");
#nullable restore
#line 65 "C:\Users\Usuario\Desktop\GR Presentación Final\GestorReunionesAnalisis-luis\GestorReunionesProyectoAnalisis\Views\Dashboard\DashboardView.cshtml"
                            
                        }

#line default
#line hidden
#nullable disable
                WriteLiteral("\n\n                    ");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.RenderAtEndOfFormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_7);
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.Controller = (string)__tagHelperAttribute_8.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_8);
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.Action = (string)__tagHelperAttribute_9.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_9);
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.Method = (string)__tagHelperAttribute_10.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_10);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\n                </div>\n            </div>\n\n\n        </div>\n       \n\n");
#nullable restore
#line 77 "C:\Users\Usuario\Desktop\GR Presentación Final\GestorReunionesAnalisis-luis\GestorReunionesProyectoAnalisis\Views\Dashboard\DashboardView.cshtml"
         if (ViewBag.ListaDashboard != null)
        {


#line default
#line hidden
#nullable disable
            WriteLiteral(@"            <div class=""album py-5 bg-light"">
                <div class=""container"">

                    <div class=""row"">
                        <div class=""col-md-4"">
                            <div class=""card mb-4 shadow-sm text-lg-center"">
                                <h3 class=""text-muted"">Asistencia</h3>
                                <canvas id=""myChart"" width=""400"" height=""400""></canvas>
                            </div>
                        </div>
                        <div class=""col-md-4"">
                            <div class=""card mb-4 shadow-sm text-lg-center"">
                                <h3 class=""text-muted"">Cantidad de reuniones</h3>
                                <canvas id=""graficoBarrasCantidadReuniones"" width=""400"" height=""400""></canvas>
                            </div>
                        </div>


                        <div class=""col-md-4"">
                            <div class=""card mb-4 shadow-sm text-lg-center"">
                                <h3 class");
            WriteLiteral(@"=""text-muted"">Tipo de reuniones</h3>
                                <canvas id=""graficoDonaTipo"" width=""200"" height=""200""></canvas>
                            </div>
                        </div>

                        <div class=""col-md-4"">
                            <div class=""card mb-4 shadow-sm text-lg-center"">
                                <h3 class=""text-muted"">Dinero invertido</h3>
                                <canvas id=""gfclineaDinero"" width=""200"" height=""200""></canvas>
                            </div>
                        </div>

                        <div class=""col-md-4"">
                            <div class=""card mb-4 shadow-sm text-lg-center"">
                                <h3 class=""text-muted"">Tiempo invertido</h3>
                                <canvas id=""gfclineaTiempo"" width=""200"" height=""200""></canvas>
                            </div>
                        </div>


                    </div>
                </div>
            </div>
");
#nullable restore
#line 123 "C:\Users\Usuario\Desktop\GR Presentación Final\GestorReunionesAnalisis-luis\GestorReunionesProyectoAnalisis\Views\Dashboard\DashboardView.cshtml"
        }

#line default
#line hidden
#nullable disable
            WriteLiteral("\n    </section>\n</section>\n\n\n");
#nullable restore
#line 129 "C:\Users\Usuario\Desktop\GR Presentación Final\GestorReunionesAnalisis-luis\GestorReunionesProyectoAnalisis\Views\Dashboard\DashboardView.cshtml"
 if (ViewBag.ListaDashboard != null)
{

    DashboardModel temp = ViewBag.ListaDashboard;


#line default
#line hidden
#nullable disable
            WriteLiteral("    <input id=\"TC_Nombre_Meses\" name=\"iptIDReunion\" class=\"form-control\"");
            BeginWriteAttribute("value", " value=\"", 5100, "\"", 5129, 1);
#nullable restore
#line 134 "C:\Users\Usuario\Desktop\GR Presentación Final\GestorReunionesAnalisis-luis\GestorReunionesProyectoAnalisis\Views\Dashboard\DashboardView.cshtml"
WriteAttributeValue("", 5108, temp.TC_Nombre_Meses, 5108, 21, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(" hidden />\n    <input id=\"TC_Asistencia_Meses\" name=\"iptNombreReunion\" class=\"form-control\"");
            BeginWriteAttribute("value", " value=\"", 5221, "\"", 5254, 1);
#nullable restore
#line 135 "C:\Users\Usuario\Desktop\GR Presentación Final\GestorReunionesAnalisis-luis\GestorReunionesProyectoAnalisis\Views\Dashboard\DashboardView.cshtml"
WriteAttributeValue("", 5229, temp.TC_Asistencia_Meses, 5229, 25, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(" hidden />\n    <input id=\"TC_Cantidad_Reunion\" name=\"iptNombreReunion\" class=\"form-control\"");
            BeginWriteAttribute("value", " value=\"", 5346, "\"", 5379, 1);
#nullable restore
#line 136 "C:\Users\Usuario\Desktop\GR Presentación Final\GestorReunionesAnalisis-luis\GestorReunionesProyectoAnalisis\Views\Dashboard\DashboardView.cshtml"
WriteAttributeValue("", 5354, temp.TC_Cantidad_Reunion, 5354, 25, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(" hidden />\n    <input id=\"TC_Tipo_Reunion\" name=\"iptNombreReunion\" class=\"form-control\"");
            BeginWriteAttribute("value", " value=\"", 5467, "\"", 5497, 2);
#nullable restore
#line 137 "C:\Users\Usuario\Desktop\GR Presentación Final\GestorReunionesAnalisis-luis\GestorReunionesProyectoAnalisis\Views\Dashboard\DashboardView.cshtml"
WriteAttributeValue("", 5475, temp.TC_Tipo_Reunion, 5475, 21, false);

#line default
#line hidden
#nullable disable
            WriteAttributeValue(" ", 5496, "", 5497, 1, true);
            EndWriteAttribute();
            WriteLiteral(" hidden />\n    <input id=\"TC_Nombre_Reunion\" name=\"iptNombreReunion\" class=\"form-control\"");
            BeginWriteAttribute("value", " value=\"", 5587, "\"", 5618, 1);
#nullable restore
#line 138 "C:\Users\Usuario\Desktop\GR Presentación Final\GestorReunionesAnalisis-luis\GestorReunionesProyectoAnalisis\Views\Dashboard\DashboardView.cshtml"
WriteAttributeValue("", 5595, temp.TC_Nombre_Reunion, 5595, 23, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(" hidden />\n    <input id=\"TC_Dinero_Invertido\" name=\"iptFechaReunion\" class=\"form-control\"");
            BeginWriteAttribute("value", " value=\"", 5709, "\"", 5742, 1);
#nullable restore
#line 139 "C:\Users\Usuario\Desktop\GR Presentación Final\GestorReunionesAnalisis-luis\GestorReunionesProyectoAnalisis\Views\Dashboard\DashboardView.cshtml"
WriteAttributeValue("", 5717, temp.TC_Dinero_Invertido, 5717, 25, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(" hidden />\n    <input id=\"TC_Tiempo_Invertido\" name=\"iptFechaReunion\" class=\"form-control\"");
            BeginWriteAttribute("value", " value=\"", 5833, "\"", 5866, 1);
#nullable restore
#line 140 "C:\Users\Usuario\Desktop\GR Presentación Final\GestorReunionesAnalisis-luis\GestorReunionesProyectoAnalisis\Views\Dashboard\DashboardView.cshtml"
WriteAttributeValue("", 5841, temp.TC_Tiempo_Invertido, 5841, 25, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(" hidden />\n");
            WriteLiteral(@"    <script>

        //alert(document.getElementById(""TC_Nombre_Meses"").value);
        var nombreMeses = document.getElementById(""TC_Nombre_Meses"").value;
        nombreMesesSplit = nombreMeses.split(',');

        var asistenciaReunion= document.getElementById(""TC_Asistencia_Meses"").value;
        asistenciaReunionSplit = asistenciaReunion.split(',');

        var cantidadReunion = document.getElementById(""TC_Cantidad_Reunion"").value;
        cantidadReunionSplit = cantidadReunion.split(',');

        var TipoReunion = document.getElementById(""TC_Tipo_Reunion"").value;
        TipoReunionSplit = TipoReunion.split(',');

        var NombreReunion = document.getElementById(""TC_Nombre_Reunion"").value;
        NombreReunionSplit = NombreReunion.split(',');

        var DineroReunion = document.getElementById(""TC_Dinero_Invertido"").value;
        DineroReunionSplit = DineroReunion.split(',');

        var TiempoReunion = document.getElementById(""TC_Tiempo_Invertido"").value;
        TiempoReunionSplit = TiempoReu");
            WriteLiteral(@"nion.split(',');

        // Enero,febrero

        var ctx = document.getElementById('myChart');
        var myChart = new Chart(ctx, {
            type: 'line',

            data: {
                
                labels: nombreMesesSplit,
                datasets: [{
                    label: 'Mensual',
                    data: asistenciaReunionSplit,
                    backgroundColor: 'rgba(241, 196, 15 , 0.4)',
                    borderColor: 'rgba(46, 64, 83 , 0.6)',
                    borderWidth: 1
                }]
            },
            options: {
                scales: {
                    yAxes: [{
                        ticks: {
                            beginAtZero: true
                        }
                    }]
                }
            }
        });



        var graficoBarrasCantidadReuniones = document.getElementById('graficoBarrasCantidadReuniones');
        var gfcBarTipo = new Chart(graficoBarrasCantidadReuniones, {
            type: 'bar',

            data: ");
            WriteLiteral(@"{
                labels: nombreMesesSplit,
                datasets: [{
                    label: 'Mensual',
                    data: cantidadReunionSplit,
                    backgroundColor: 'rgba(241, 196, 15  , 0.5)',
                    borderColor: 'rgba(46, 64, 83 , 0.6)',
                    borderWidth: 1
                }]
            },
            options: {
                scales: {
                    yAxes: [{
                        ticks: {
                            beginAtZero: true
                        }
                    }]
                }
            }
        });



        var graficoDonaTipo = document.getElementById('graficoDonaTipo');


        var gfcPieTipo = new Chart(graficoDonaTipo, {
            type: 'doughnut',

            data: {
                labels: NombreReunionSplit,
                datasets: [{


                    label: 'Mensual',
                    data: TipoReunionSplit,
                    borderColor: 'rgba(46, 64, 83 , 0.6)',
                    ");
            WriteLiteral(@"backgroundColor: colorRGB(20),
                    borderWidth: 3
                }]
            }

        });






        var gfclineaDinero = document.getElementById('gfclineaDinero');
        var myChart = new Chart(gfclineaDinero, {
            type: 'line',

            data: {
                labels: nombreMesesSplit,
                datasets: [{
                    label: 'Colones',
                    data: DineroReunionSplit,
                    backgroundColor: 'rgba(241, 196, 15 , 0.4)',
                    borderColor: 'rgba(46, 64, 83 , 0.6)',
                    borderWidth: 1
                }]
            },
            options: {
                scales: {
                    yAxes: [{
                        ticks: {
                            beginAtZero: true
                        }
                    }]
                }
            }
        });




        var gfclineaTiempo = document.getElementById('gfclineaTiempo');
        var myChart = new Chart(gfclineaTiempo, {
            ");
            WriteLiteral(@"type: 'line',

            data: {
                labels: nombreMesesSplit,
                datasets: [{
                    label: 'Horas',
                    data: TiempoReunionSplit,
                    backgroundColor: 'rgba(241, 196, 15 , 0.4)',
                    borderColor: 'rgba(46, 64, 83 , 0.6)',
                    borderWidth: 1
                }]
            },
            options: {
                scales: {
                    yAxes: [{
                        ticks: {
                            beginAtZero: true
                        }
                    }]
                }
            }
        });



        function generarNumero(numero) {

            return (Math.random() * numero).toFixed(0);
        }

        function colorRGB(numero) {
            var coolor = ['rgba(212, 172, 13, 0.9)'];
            for (var i = 0; i < numero; i++) {
                coolor.push(""rgba("" + generarNumero(255) + "","" + generarNumero(255) + "","" + generarNumero(255) + "", 0.5)"");
            }

    ");
            WriteLiteral("        return coolor;\n        }\n\n\n\n\n\n    </script>\n");
#nullable restore
#line 324 "C:\Users\Usuario\Desktop\GR Presentación Final\GestorReunionesAnalisis-luis\GestorReunionesProyectoAnalisis\Views\Dashboard\DashboardView.cshtml"

}

#line default
#line hidden
#nullable disable
            WriteLiteral("\n\n\n\n\n\n\n");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<String> Html { get; private set; }
    }
}
#pragma warning restore 1591
