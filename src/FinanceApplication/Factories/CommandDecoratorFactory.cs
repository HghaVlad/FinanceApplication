using FinanceApplication.commands;
using FinanceApplication.services;

namespace FinanceApplication.Factories
{
    public class CommandDecoratorFactory
    {
        private readonly CommandLogger _logger;

        public CommandDecoratorFactory(CommandLogger logger)
        {
            _logger = logger;
        }

        public ICommand Decorate<T>(T command) where T : ICommand
        {
            return new LoggingCommandDecorator(command, _logger);
        }
    }
}