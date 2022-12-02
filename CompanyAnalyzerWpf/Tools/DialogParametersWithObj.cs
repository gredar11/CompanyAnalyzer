using Prism.Services.Dialogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyAnalyzerWpf.Tools
{
    public class DialogParametersWithObj: DialogParameters
    {
        public object RequestParameter;
        public bool CreateNew;
        public DialogParametersWithObj(object requestParameter, bool createNew)
        {
            CreateNew = createNew;
            RequestParameter = requestParameter;
        }
    }
}
