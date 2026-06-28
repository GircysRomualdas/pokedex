
Console.WriteLine("Welcome to the Pokedex!");
Help();

while (true) {
  string? input = Console.ReadLine();
  if (string.IsNullOrWhiteSpace(input)) {
    continue;
  }

  string[] parts = input.Split(' ', StringSplitOptions.RemoveEmptyEntries);

  switch (parts[0]) {
    case "exit":
      Console.WriteLine("Exiting Pokedex!");
      return;
    case "help":
      Console.WriteLine("You can use");
      Help();
      break;
    default:
      Console.WriteLine("Unknown command");
      break;
  }
}

void Help() {
  Console.WriteLine("Commands:");
  Console.WriteLine("\t exit: Exit the Pokedex");
  Console.WriteLine("\t help: Display a help message");
}