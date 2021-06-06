using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RemittanceWebApp.Models
{
    public class AlertModel
    {
        public AlertModel() { }

        public AlertModel(string message,AlertType type)
        {
            switch (type)
            {
                case AlertType.Error:
                    cssClass = "alert-danger";
                    heading = "Error!";
                    break;
                case AlertType.Success:
                    cssClass = "alert-success";
                    heading = "Success!";
                    break;
                case AlertType.Warning:
                    cssClass = "alert-warning";
                    heading = "Warning!";
                    break;
                default:
                    cssClass = "alert-info";
                    heading = "Information!";
                    break;
            }
            this.message = message;
        }

        private string heading;
        public string Heading { get { return heading; } }

        private string message;
        public string Message { get { return message; } }

        private string cssClass;
        public string CSSClass { get { return cssClass; } }

        public enum AlertType
        {
            Success,
            Information,
            Error,
            Warning
        }

    }

    
}