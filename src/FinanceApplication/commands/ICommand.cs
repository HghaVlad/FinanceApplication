namespace FinanceApplication.commands
{
    /// <summary>
    /// Our own simple interface ICommand
    /// We need only the Name of the command and the void function that runs int
    /// </summary>
    public interface ICommand
    {
        public string Name { get; }
        public Task Execute();
    }
}