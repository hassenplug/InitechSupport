using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitechSupport
{
    public enum tCallType
    {
        [Description("Undefined")]
        unknown,
        [Description("Voice mail")]
        voicemail,
        [Description("Email")]
        email,
    }

    public class CallListCollection : ObservableCollection<CallListClass>
    {

    }

    public class CallListClass
    {
        public CallListClass()
        {
            CallDateTime = DateTime.Now;
            ContactName = "No Contact";
        }

        public tCallType Source { get; set; }
        public DateTime CallDateTime { get; set; }
        public string ContactName { get; set; }
        public bool Responded { get; set; }
    }
}
