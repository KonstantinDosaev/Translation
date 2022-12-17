using System.Windows.Input;

namespace Translation
{
    internal class MyDataContext
    {
        public ICommand TranslationCommand { get; } = new TranslationCommand();
    }
}
