using System.Threading.Tasks;
using System.Windows.Input;

namespace CompanyAnalyzerWpf.Commands.GenericCommand
{
    public interface IAsyncCommand<T> : ICommand
    {
        Task ExecuteAsync(T parameter);
        bool CanExecute(T parameter);
    }
}
