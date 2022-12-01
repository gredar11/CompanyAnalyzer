using CompanyAnalyzerWpf.Commands;
using CompanyAnalyzerWpf.Tools;
using Domain.Models;
using Persistance;
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
        private readonly RepositoryManager _repositoryManager;
        public EditCompanyDialogViewModel(RepositoryManager repositoryManager)
        {
            _repositoryManager = repositoryManager;
        }
        public CompanyViewModel CompanyViewModel;
        private string _companyName;
        public string CompanyName
        {
            get { return _companyName; }
            set
            {
                SetProperty(ref _companyName, value);
            }
        }
        private DateOnly _establishmentDate;
        public DateOnly EstablishmentDate
        {
            get { return _establishmentDate; }
            set
            {
                SetProperty(ref _establishmentDate, value);
            }
        }
        private string _adress;
        public string Adress
        {
            get { return _adress; }
            set
            {
                SetProperty(ref _adress, value);
            }
        }
        private DelegateCommand<string> _closeDialogCommand;
        public DelegateCommand<string> CloseDialogCommand =>
            _closeDialogCommand ?? (_closeDialogCommand = new DelegateCommand<string>(CloseDialog));
        public string Title => "Edit company";

        public event Action<IDialogResult> RequestClose;
        protected virtual void CloseDialog(string parameter)
        {
            ButtonResult result = ButtonResult.None;

            if (parameter?.ToLower() == "true")
            {
                CompanyViewModel.Company.EstablishmentDate = EstablishmentDate;
                CompanyViewModel.Company.CompanyName = CompanyName;
                CompanyViewModel.CompanyName = CompanyName;
                CompanyViewModel.Company.Adress = Adress;
                _repositoryManager.CompanyRepository.UpdateCompany(CompanyViewModel.Company);
                _repositoryManager.SaveAsync();
                result = ButtonResult.OK;
            }
            else if (parameter?.ToLower() == "false")
                result = ButtonResult.Cancel;

            RaiseRequestClose(new DialogResult(result));
        }

        public virtual void RaiseRequestClose(IDialogResult dialogResult)
        {
            RequestClose?.Invoke(dialogResult);
        }
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
            CompanyName = CompanyViewModel.Company.CompanyName;
            Adress = CompanyViewModel.Company.Adress;
            EstablishmentDate = CompanyViewModel.Company.EstablishmentDate;
        }
    }
}
