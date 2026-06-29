# Pokedex

Command-line Pokemon exploration tool built in C# that allows users to explore the Pokemon world through an interactive REPL interface. The application integrates with the [PokeAPI](https://pokeapi.co) to fetch real-time Pokemon data.

## Requirements

- .NET SDK 10

## Installation

1. Clone the repository.

## Usage

```bash
dotnet run
```

### map command

```bash
map
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

### explore command

```bash
explore canalave-city-area
```

Output:

```


```

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
