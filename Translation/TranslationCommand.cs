using System;
using System.Windows;
using System.Windows.Input;

namespace Translation
{
    internal class TranslationCommand : ICommand
    {
        public bool CanExecute(object? parameter)
        {
            return true;
        }

        public async void Execute(object? parameter)
        {
            if (!Clipboard.ContainsText()) return;
            var result = await Translator.Translate(Clipboard.GetText());
            if (result != null) Clipboard.SetText(result);
        }

        public event EventHandler? CanExecuteChanged;
    }
}

