using Prism.Services.Dialogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CompanyAnalyzerWpf.Extensions
{
    public static class DialogExtension
    {
        public static Task<IDialogResult> ShowDialogAsync(this IDialogService dialogService, string name, IDialogParameters parameters)
        {
            var tcs = new TaskCompletionSource<IDialogResult>();

            try
            {
                dialogService.ShowDialog(name, parameters, (result) =>
                {

                    tcs.SetResult(result);
                });
            }
            catch (Exception ex)
            {
                tcs.SetException(ex);
            }
            return tcs.Task;
        }
    }
}
