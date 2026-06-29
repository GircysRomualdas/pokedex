# Pokedex

Command-line Pokemon exploration tool built in C# that allows users to explore the Pokemon world through an interactive REPL interface. The application integrates with the [PokeAPI](https://pokeapi.co) to fetch real-time Pokemon data.

## Requirements

- .NET SDK 10

## Installation

1. Clone the repository.

## Usage

### Run the application

```bash
dotnet run
```

#### Example Session

##### Display help:

```bash
> help
```

Output:

```bash
 Commands:
   - exit: Exit the Pokedex REPL.
   - help: Display a help message with all available commands.
   - map: Display the next 20 location areas.
   - mapb Display the previous 20 location areas.
   - explore <location-area>: List all Pokemon found in a specific location.
   - catch <pokemon>: Attempt to catch a wild Pokemon.
   - pokedex: Display all Pokemon you've caught.

```

##### View map:

```bash
> map
```

Output:

```bash
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

##### Explore a location:

```bash
> explore canalave-city-area
```

Output:

```bash
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

##### Catch a Pokemon:

```bash
> catch tentacool
```

Output:

```bash
Throwing a Pokeball at tentacool...
tentacool was caught!
```

##### View your Pokedex:

```bash
> pokedex
```

Output:

```bash
 Your Pokedex:
 - tentacool water
```

##### Exit the application:

```bash
> exit
```

Output:

```bash
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

# Temp Notes

## important URL:

Required endpoints only:
📍 Location browsing (map/mapb)
GET /location-area?limit=20&offset=0

🌿 Explore a location
GET /location-area/{location-name}

🧬 Pokémon data (catch + inspect)
GET /pokemon/{name}

📖 Extra lore (inspect enhancement)
GET /pokemon-species/{name}

🧩 OPTIONAL (nice upgrades)
Evolution chains
GET /evolution-chain/{id}

(found inside species endpoint)

Type info (for better inspect UI)
GET /type/{name}
Ability details
GET /ability/{name}

## emojis to add (maybe?)

Pokémon type → emoji mapping
🔥 Fire
🔥
💧 Water
💧
🌿 Grass
🌿
⚡ Electric
⚡
❄️ Ice
❄️
🪨 Rock
🪨
🧱 Ground
🟫 or 🌍
🟫 (brown block) = more consistent
🌍 = more thematic but less precise
🐦 Flying
🕊️ or 🪶
🐛 Bug
🐛
👻 Ghost
👻
🧠 Psychic
🔮
🐉 Dragon
🐉
🥊 Fighting
🥊
🗡️ Dark
🌑 or 🖤
⚙️ Steel
⚙️
🧚 Fairy
🧚 or ✨
☠️ Poison
☠️ or 🧪
🐾 Normal
⚪ or 🐾
