namespace Services
{
    public interface IConsoleViewManager
    {
        void ToggleConsole();
        void OpenConsole();
        void CloseConsole();
        void WriteToOutput(string message);
        bool IsConsoleOpen { get; }
    }
}