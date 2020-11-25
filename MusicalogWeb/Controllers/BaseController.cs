using MusicalogWeb.Properties;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MusicalogWeb.Controllers
{
    /// <summary>
    /// Base controller class containing common functionality for controllers.
    /// </summary>
    public class BaseController : Controller
    {
        #region "Error Handling Methods"

        protected override void OnException(ExceptionContext filterContext)
        {
            if (filterContext.ExceptionHandled)
            {
                return;
            }
            else
            {
                // Determine if the action was responding to an Ajax Request.
                bool isAjaxRequest = (filterContext.HttpContext.Request["X-Requested-With"] == "XMLHttpRequest") ||
                    ((filterContext.HttpContext.Request.Headers != null) && (filterContext.HttpContext.Request.Headers["X-Requested-With"] == "XMLHttpRequest"));

                // Get exception error message - including any inner exception message.
                string errorMessage = filterContext.Exception.Message;
                if (filterContext.Exception.InnerException != null)
                {
                    errorMessage = filterContext.Exception.InnerException.Message;
                }

                if (isAjaxRequest)
                {
                    // Return an error in response to Ajax request. Error message will be displayed in a dialog popup in jQuery.
                    //filterContext.HttpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    filterContext.HttpContext.Response.StatusCode = 530;
                    filterContext.HttpContext.Response.StatusDescription = "Internal Ajax Request Error";

                    filterContext.Result = new JsonResult
                    {
                        Data = new { success = false, errorMessage = errorMessage, error = filterContext.Exception.ToString() },
                        JsonRequestBehavior = JsonRequestBehavior.AllowGet
                    };
                }
                else
                {
                    filterContext.HttpContext.Response.StatusCode = 531;
                    filterContext.HttpContext.Response.StatusDescription = "Internal Musicalog Server Error";

                    // Save exception message into TempData to display in Error view.
                    TempDataDictionary tempData = new TempDataDictionary();
                    tempData.Add("ErrorMessage", HttpUtility.HtmlEncode(errorMessage));
                    tempData.Add("AppTitle", Settings.Default.AppTitle);

                    // Set the error view and tempdata to use in view.
                    filterContext.Controller.TempData = tempData;
                    filterContext.Result = new ViewResult
                    {
                        ViewName = "~/Views/Error/Error500.cshtml",
                        MasterName = "~/Views/Shared/_LayoutError.cshtml",
                        TempData = tempData
                    };
                }
            }

            filterContext.ExceptionHandled = true;
        }

        #endregion

        #region "View Helper Methods"

        /// <summary>
        /// Return a Json Result which includes a success flag and message.
        /// </summary>
        /// <param name="success"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        internal JsonResult JsonActionResult(bool success, string message)
        {
            // Return a Json result which includes a success flag and message.
            return Json(new { Success = success, Message = message }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Return a Json Result which includes the view model with any validation errors, partial view and a redirection URL if required
        /// </summary>
        /// <param name="success"></param>
        /// <param name="viewName"></param>
        /// <param name="model"></param>
        /// <param name="clearModelState">Clear the model state which allows a modified view model to be returned in view.</param>
        /// <returns></returns>
        internal JsonResult JsonView(bool success, string viewName, object model, bool clearModelState = false)
        {
            // Return a Json result which includes the validation result, partial view and view model.
            return Json(new { Success = success, View = RenderPartialView(viewName, model, clearModelState) }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Return a Json Result which includes the view model with any validation errors, partial view and a redirection URL if required
        /// </summary>
        /// <param name="success"></param>
        /// <param name="viewName"></param>
        /// <param name="model"></param>
        /// <param name="redirectionURL"></param>
        /// <returns></returns>
        internal JsonResult JsonView(bool success, string viewName, object model, string redirectionURL)
        {
            // Return a Json result which includes the validation result, partial view and a redirection URL if required.
            return Json(new { Success = success, View = RenderPartialView(viewName, model, false), URL = redirectionURL });
        }

        /// <summary>
        /// Generate a partial view from the given partial view name and bind provided model with it.
        /// The rendered partial view will use the current ModelState, so if a new partial view is being rendered
        /// after processing a HttpPost then the ModelState needs to be cleared prior to calling RenderPartialView.
        /// </summary>
        /// <param name="partialViewName"></param>
        /// <param name="model"></param>
        /// <param name="clearModelState">Clear the model state which allows a modified view model to be returned in view.</param>
        /// <returns></returns>
        internal string RenderPartialView(string partialViewName, object model, bool clearModelState)
        {
            if (ControllerContext == null)
                return string.Empty;

            if (model == null)
            {
                throw new ArgumentNullException("model", "The view model for the partial view to render was not provided.");
            }

            if (string.IsNullOrEmpty(partialViewName))
            {
                throw new ArgumentNullException("partialViewName", "The name of the partial view to render was not provided.");
            }

            // Clear the model state if required so that any modified view model is returned in view,
            // else return the same view model that was provided.
            if (clearModelState)
            {
                ModelState.Clear();
            }

            ViewData.Model = model;

            using (var sw = new StringWriter())
            {
                var viewResult = ViewEngines.Engines.FindPartialView(ControllerContext, partialViewName);
                var viewContext = new ViewContext(ControllerContext, viewResult.View, ViewData, TempData, sw);
                viewResult.View.Render(viewContext, sw);
                return sw.GetStringBuilder().ToString();
            }
        }

        #endregion
    }
}