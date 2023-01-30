using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WEBSITE_FILM_002.Models
{
    public class _ListComments
    {
        public string comment_content   { get; set; }
        public DateTime date            { get; set; }
        public string userFirstName     { get; set; }
        public string userLastName      { get; set; }
        public string avatar            { get; set; }
    }
}