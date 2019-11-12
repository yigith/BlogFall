﻿using BlogFall.Areas.Admin.Controllers;
using BlogFall.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;

namespace BlogFall.Helpers
{
    public static class HtmlHelpers
    {
        public static string ControllerName(this HtmlHelper htmlHelper)
        {
            return htmlHelper.ViewContext.RouteData.Values["controller"].ToString();
        }

        public static string ActionName(this HtmlHelper htmlHelper)
        {
            return htmlHelper.ViewContext.RouteData.Values["action"].ToString();
        }

        public static string BreadcrumbControllerName(this HtmlHelper htmlHelper)
        {
            string controller = htmlHelper.ControllerName();

            Type t = Type.GetType("BlogFall.Areas.Admin.Controllers." + controller + "Controller");
            object[] attributes = t.GetCustomAttributes(typeof(BreadcrumbAttribute), true);

            if (attributes.Length == 0)
            {
                return controller;
            }

            var attr = (BreadcrumbAttribute)attributes[0];

            return attr.Name;
        }

        public static string BreadcrumbActionName(this HtmlHelper htmlHelper)
        {
            string controller = htmlHelper.ControllerName();
            string action = htmlHelper.ActionName();

            Type t = Type.GetType("BlogFall.Areas.Admin.Controllers." + controller + "Controller");

            MethodInfo mi = t.GetMethods().FirstOrDefault(x => x.Name == action);

            BreadcrumbAttribute ba = mi.GetCustomAttribute(typeof(BreadcrumbAttribute)) 
                as BreadcrumbAttribute;

            if (ba == null)
                return action;
            
            return ba.Name;
        }

        public static IHtmlString ShowPostIntro(this HtmlHelper htmlHelper, string content)
        {
            int pos = content.IndexOf("<hr>");

            if (pos == -1)
            {
                return htmlHelper.Raw(content);
            }

            return htmlHelper.Raw(content.Substring(0, pos));
        }



        public static IHtmlString ShowPost(this HtmlHelper htmlHelper, string content)
        {
            int pos = content.IndexOf("<hr>");

            if (pos == -1)
            {
                return htmlHelper.Raw(content);
            }

            return htmlHelper.Raw(content.Remove(pos, 4));
        }
    }
}