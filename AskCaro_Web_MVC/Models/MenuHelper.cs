﻿using System;
using System.Web.Mvc;

namespace AskCaro_Web_MVC
{
    /// <summary>
    /// Menu current active item helper class.
    /// </summary>
    public static class MenuHelper
    {
        /// <summary>
        /// Determines whether the specified controller is selected.
        /// </summary>
        /// <param name="html">The HTML.</param>
        /// <param name="controller">The controller.</param>
        /// <param name="action">The action.</param>
        /// <param name="cssClass">The CSS class.</param>
        /// <returns></returns>
        public static string IsSelected(this  HtmlHelper html, string controller = null, string action = null, string cssClass = "selected")
        {
            // const string cssClass = "selected";
            var currentAction = (string)html.ViewContext.RouteData.Values["action"];
            var currentController = (string)html.ViewContext.RouteData.Values["controller"];

            if (String.IsNullOrEmpty(controller))
                controller = currentController;

            if (String.IsNullOrEmpty(action))
                action = currentAction;

            return controller == currentController && action == currentAction ?
                cssClass : String.Empty;
        }

    }
}