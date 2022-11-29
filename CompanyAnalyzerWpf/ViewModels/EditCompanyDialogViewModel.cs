using CompanyAnalyzerWpf.Tools;
using Domain.Models;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Services.Dialogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace CompanyAnalyzerWpf.ViewModels
{
    public class EditCompanyDialogViewModel : BindableBase, IDialogAware
    {
        public EditCompanyDialogViewModel()
        {

        }
        private CompanyViewModel companyViewModel;
        public CompanyViewModel CompanyViewModel
        {
            get { return companyViewModel; }
            set { SetProperty(ref companyViewModel, value); }
        }

        public string Title => "Edit company";

        public event Action<IDialogResult> RequestClose;

        public bool CanCloseDialog()
        {
            return true;
        }

        public void OnDialogClosed()
        {

        }

        public void OnDialogOpened(IDialogParameters parameters)
        {
            var repuestObject = (parameters as DialogParametersWithObj).RequestParameter;
            CompanyViewModel = (CompanyViewModel)repuestObject;
        }
    }
}
