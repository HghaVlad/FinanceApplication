using FinanceApplication.services;

namespace FinanceApplication.commands
{
    public class LoggingCommandDecorator : ICommand
    {
        private readonly ICommand _command;
        private readonly CommandLogger _logger;

        public LoggingCommandDecorator(ICommand command, CommandLogger logger)
        {
            _command = command;
            _logger = logger;
        }

        public string Name => _command.Name;

        public async Task Execute()
        {
            _logger.LogCommand(_command.Name);
            await _command.Execute();
        }
    }
}