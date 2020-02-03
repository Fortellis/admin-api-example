using System;
using System.Collections.Generic;

namespace AdminAPIImplementation.Models
{
    public class ActivateResponse
    {
        public List<ActionLink> links { get; set; }
    }

    public class ActionLink
    {
        public string href { get; set; }
        public string rel { get; set; }
        public string method { get; set; }
        public string title { get; set; }
    }
}
