using FinanceApplication.commands;

namespace FinanceApplication
{
    public class Menu
    {
        public string Name;
        private readonly List<ICommand> _commands;

        public Menu(string name, List<ICommand> commands)
        {
            Name = name;
            _commands = commands;
        }

        public void Run()
        {
            Console.WriteLine("==========================");
            while (true)
            {
                ShowMenu();
                Console.Write("Enter your choice: ");
                string? input = Console.ReadLine();
                int choice;
                while (string.IsNullOrEmpty(input) || !int.TryParse(input, out choice) || choice < 1 ||
                       choice > _commands.Count + 1)
                {
                    Console.WriteLine("The choice input is incorrect. Please try again");
                    Console.Write("Enter your choice: ");
                    input = Console.ReadLine();
                }

                if (choice <= _commands.Count)
                {
                    _commands[choice - 1].Execute();
                }
                else
                {
                    Console.WriteLine("==========================");
                    return;
                }
            }
        }


        private void ShowMenu()
        {
            Console.WriteLine($"Welcome to {Name} menu");
            Console.WriteLine("Please select the option by writing command number:");
            for (int i = 0; i < _commands.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {_commands[i].Name}");
            }

            Console.WriteLine($"{_commands.Count + 1}. Exit");
        }
    }
}