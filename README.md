# Pokedex

Command-line Pokemon exploration tool built in C# that allows users to explore the Pokemon world through an interactive REPL interface. The application integrates with the [PokeAPI](https://pokeapi.co) to fetch real-time Pokemon data.

## Requirements

- .NET SDK 10
- Docker

## Installation

1. Clone the repository.

## Usage

### Run the application

```bash
dotnet run
```

## Example Session

### Display help:

```
> help
```

Output:

```
 Commands:
   - exit: Exit the Pokedex REPL.
   - help: Display a help message with all available commands.
   - map: Display the next 20 location areas.
   - mapb Display the previous 20 location areas.
   - explore <location-area>: List all Pokemon found in a specific location.
   - catch <pokemon>: Attempt to catch a wild Pokemon.
   - pokedex: Display all Pokemon you've caught.

```

### View map:

```
> map
```

Output:

```
 canalave-city-area
 eterna-city-area
 pastoria-city-area
 sunyshore-city-area
 sinnoh-pokemon-league-area
 oreburgh-mine-1f
 oreburgh-mine-b1f
 valley-windworks-area
 eterna-forest-area
 fuego-ironworks-area
 mt-coronet-1f-route-207
 mt-coronet-2f
 mt-coronet-3f
 mt-coronet-exterior-snowfall
 mt-coronet-exterior-blizzard
 mt-coronet-4f
 mt-coronet-4f-small-room
 mt-coronet-5f
 mt-coronet-6f
 mt-coronet-1f-from-exterior
```

### Explore a location:

```
> explore canalave-city-area
```

Output:

```
 Found Pokemon:
 - tentacool
 - tentacruel
 - staryu
 - magikarp
 - gyarados
 - wingull
 - pelipper
 - shellos
 - gastrodon
 - finneon
 - lumineon
```

### Catch a Pokemon:

```
> catch tentacool
```

Output:

```
Throwing a Pokeball at tentacool...
tentacool was caught!
```

### View your Pokedex:

```
> pokedex
```

Output:

```
 Your Pokedex:
 - tentacool water
```

### Exit the application:

```
> exit
```

Output:

```
 Exiting Pokedex!
```

## Commands

| Command              | Description                                        |
| -------------------- | -------------------------------------------------- |
| `help`               | Display a help message with all available commands |
| `exit`               | Exit the Pokedex REPL                              |
| `map`                | Display the next 20 location areas                 |
| `mapb`               | Display the previous 20 location areas             |
| `explore <location>` | List all Pokemon found in a specific location      |
| `catch <pokemon>`    | Attempt to catch a wild Pokemon                    |
| `inspect <pokemon>`  | View detailed information about a caught Pokemon   |
| `pokedex`            | Display all Pokemon you've caught                  |

---

Temp Notes
To Do:

- Add DB (PostgreSQL)
- Add Dokeker (To run DB and Unit test in GitHub actions)
- Add Unit test
- Add CI/CD
