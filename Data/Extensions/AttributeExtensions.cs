
using BTS.Web.Infrastructure.Helpers;
using Data.Common;
using Data.DataModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Linq.Dynamic;
using System.Linq.Expressions;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using System.Web.Security;
using System.Web.SessionState;

namespace Data.Extensions
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, Inherited = true, AllowMultiple = true)]
    public class AuthorizeRolesAttribute : AuthorizeAttribute
    {
        public AuthorizeRolesAttribute(params string[] roles)
        {
            Roles = String.Join(",", roles);
        }

        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            if (filterContext.HttpContext.Request.IsAjaxRequest())
            {
                if (filterContext.HttpContext.Request.IsAuthenticated)
                {
                    filterContext.Result = new JsonResult
                    {
                        Data = new
                        {
                            // put whatever data you want which will be sent
                            // to the client
                            status = CommonConstants.Status_Error,
                            message = "Bạn Không được cấp quyền để thực hiện chức năng này"
                        },
                        JsonRequestBehavior = JsonRequestBehavior.AllowGet
                    };
                }
                else
                {
                    filterContext.Result = new JsonResult
                    {
                        Data = new
                        {
                            // put whatever data you want which will be sent
                            // to the client
                            status = CommonConstants.Status_TimeOut,
                            message = "Xin lỗi! Đã quá thời gian chờ. Bạn đã bị đăng xuất ra khỏi hệ thống."
                        },
                        JsonRequestBehavior = JsonRequestBehavior.AllowGet
                    };
                }
            }
            else
            {
                if (filterContext.HttpContext.Request.IsAuthenticated)
                {
                    filterContext.Controller.TempData["error"] = "Bạn Không được cấp quyền để thực hiện chức năng này";
                    if (HttpContext.Current.Request.UrlReferrer != null)
                        filterContext.Result = new RedirectResult(HttpContext.Current.Request.UrlReferrer.ToString());
                    else
                    {
                        filterContext.Result = new RedirectResult("/");
                    }
                }
                else
                {
                    base.HandleUnauthorizedRequest(filterContext);
                }
            }
        }
    }

    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class Unique: ValidationAttribute
    {
        public Type TargetModelType { get; set; }
        public string TargetPropertyName { get; set; }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            return (TargetModelType == null || string.IsNullOrEmpty(TargetPropertyName)) ? DirectlyValid(value, validationContext) : ViewModelValid(value, validationContext);
        }



        private ValidationResult DirectlyValid(object value, ValidationContext validationContext)
        {
            using (CFContext db = new CFContext())
            {
                string Name = GetName(validationContext);

                PropertyInfo IdProp = validationContext.ObjectInstance.GetType().GetProperties().FirstOrDefault(x => x.CustomAttributes.Count(a => a.AttributeType == typeof(KeyAttribute)) > 0);

                var Id = IdProp.GetValue(validationContext.ObjectInstance, null);

                Type entityType = validationContext.ObjectType;


                IQueryable result = db.Set(entityType).Where(Name + "==@0", value);
                int count = 0;

                if (Id != null || Name != IdProp.Name)
                {
                    result = result.Where(IdProp.Name + "<>@0", Id);
                }

                count = result.Count();

                if (count == 0 || value == null)
                {
                    return ValidationResult.Success;
                }
                else
                {
                    return new ValidationResult(ErrorMessageString);
                }
            }
        }
        private string GetName(ValidationContext validationContext)
        {
            string Name = validationContext.MemberName;

            if (string.IsNullOrEmpty(Name))
            {
                string displayName = validationContext.DisplayName;

                PropertyInfo prop = validationContext.ObjectInstance.GetType().GetProperty(displayName);

                if (prop != null)
                {
                    Name = prop.Name;
                }
                else
                {
                    List<PropertyInfo> props = validationContext.ObjectInstance.GetType().GetProperties().Where(x => x.CustomAttributes.Count(a => a.AttributeType == typeof(DisplayAttribute)) > 0).ToList();

                    foreach (PropertyInfo prp in props)
                    {
                        CustomAttributeData attr = prp.CustomAttributes.FirstOrDefault(p => p.AttributeType == typeof(DisplayAttribute));

                        object val = attr.NamedArguments.FirstOrDefault(p => p.MemberName == "Name").TypedValue.Value;

                        if (val.Equals(displayName))
                        {
                            Name = prp.Name;
                            break;
                        }
                    }
                }
            }

            return Name;


        }
        
        private ValidationResult ViewModelValid(object value, ValidationContext validationContext)
        {
            using (CFContext db = new CFContext())
            {
                string Name = TargetPropertyName;

                PropertyInfo IdProp = TargetModelType.GetProperties().FirstOrDefault(x => x.CustomAttributes.Count(a => a.AttributeType == typeof(KeyAttribute)) > 0) ?? TargetModelType.GetProperties().FirstOrDefault();



                var Id = validationContext.ObjectInstance.GetType().GetProperty(IdProp.Name).GetValue(validationContext.ObjectInstance, null);

                //int Id1 = (int)IdProp.GetValue(validationContext.ObjectInstance, null);

                Type entityType = TargetModelType;


                IQueryable result = db.Set(entityType).Where(Name + "==@0", value);
                int count = 0;

                if (Id != null || Name != IdProp.Name)
                {
                    result = result.Where(IdProp.Name + "<>@0", Id);
                }

                count = result.Count();

                if (count == 0 || value == null)
                {
                    return ValidationResult.Success;
                }
                else
                {
                    return new ValidationResult(ErrorMessageString);
                }
            }

        }
    }

    public class UserNameFilter : ActionFilterAttribute
    {
        private const string parameterName = "userName";
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (filterContext.ActionParameters.ContainsKey(parameterName))
            {
                if (filterContext.HttpContext.User.Identity.IsAuthenticated)
                {
                    filterContext.ActionParameters[parameterName] = filterContext.HttpContext.User.Identity.Name;
                }
            }
        }
    }

    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, Inherited = true, AllowMultiple = false)]
    public class NoSundayAccessAttribute : FilterAttribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationContext filterContext)
        {
            if (DateTime.Now.DayOfWeek == DayOfWeek.Sunday)
            {
                filterContext.Result = new ContentResult
                { Content = "It's Sunday, get some rest" };
            }
        }
    }

    public static class HtmlHelperExtension
    {
        public static MvcHtmlString AntiForgeryTokenForAjaxPost(this HtmlHelper helper)
        {
            var antiForgeryInputTag = helper.AntiForgeryToken().ToString();
            // Above gets the following: <input name="__RequestVerificationToken" type="hidden" value="PnQE7R0MIBBAzC7SqtVvwrJpGbRvPgzWHo5dSyoSaZoabRjf9pCyzjujYBU_qKDJmwIOiPRDwBV1TNVdXFVgzAvN9_l2yt9-nf4Owif0qIDz7WRAmydVPIm6_pmJAI--wvvFQO7g0VvoFArFtAR2v6Ch1wmXCZ89v0-lNOGZLZc1" />
            var removedStart = antiForgeryInputTag.Replace(@"<input name=""__RequestVerificationToken"" type=""hidden"" value=""", "");
            var tokenValue = removedStart.Replace(@""" />", "");
            if (antiForgeryInputTag == removedStart || removedStart == tokenValue)
                throw new InvalidOperationException("Oops! The Html.AntiForgeryToken() method seems to return something I did not expect.");
            return new MvcHtmlString(string.Format(@"{0}:""{1}""", "__RequestVerificationToken", tokenValue));
        }

        // How to use [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)] for @Html.TextBox
        // @Html.TextBoxWithFormatFor(m => m.CustomDate, new Dictionary<string, object> { { "class", "datepicker" } })

        public static MvcHtmlString TextBoxWithFormatFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, IDictionary<string, object> htmlAttributes)
        {
            ModelMetadata metadata = ModelMetadata.FromLambdaExpression(expression, htmlHelper.ViewData);
            return htmlHelper.TextBox(htmlHelper.ViewData.TemplateInfo.GetFullHtmlFieldName(metadata.PropertyName), string.Format(metadata.DisplayFormatString, metadata.Model), htmlAttributes);
        }

        public static IHtmlString MetaAcceptLanguage<TModel>(this HtmlHelper<TModel> htmlHelper)
        {
            string acceptLanguage = HttpUtility.HtmlAttributeEncode(Thread.CurrentThread.CurrentUICulture.ToString());
            return new HtmlString(string.Format("<meta name=\"accept-language\" content=\"{0}\">", acceptLanguage));
        }

        //public static MvcHtmlString ListArrayItems(this HtmlHelper html, string[] list)
        //{
        //    TagBuilder tag = new TagBuilder("ul");
        //    foreach (string str in list)
        //    {
        //        TagBuilder itemTag = new TagBuilder("li");
        //        itemTag.SetInnerText(str);
        //        tag.InnerHtml += itemTag.ToString();
        //    }
        //    return new MvcHtmlString(tag.ToString());
        //}
    }

    public class AuditAttribute : ActionFilterAttribute, IExceptionFilter
    {
        //Our value to handle our AuditingLevel
        public int AuditingLevel { get; set; }

        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            base.OnActionExecuted(filterContext);
        }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            AuditHelpers.Log(AuditingLevel);
            base.OnActionExecuting(filterContext);
        }

        void IExceptionFilter.OnException(ExceptionContext filterContext)
        {
            if (!filterContext.ExceptionHandled)
            {
                Exception exception = filterContext.Exception;

                // To do Log Exception
                

                filterContext.Result = new RedirectResult("/ErrorViews");
                filterContext.ExceptionHandled = true;
            }            
        }


        
    }


}