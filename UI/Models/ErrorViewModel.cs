using System;

namespace UI.Models
{
    public class ErrorViewModel : LogViewModel
    {
        public string RequestId { get; set; }

        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
    }
}