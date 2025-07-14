import type { State } from "./state.js";
import { Pokemon } from "./pokeapi.js";

export async function commandInspect(state: State, ...args: string[]) {
  if (!args.length) {
    console.log("inspect requires an Pokemon name");
    return;
  }
  const pokemonName = args[0];
  const pokemon: Pokemon = state.pokedex[pokemonName];
  if (!pokemon) {
    console.log("you have not caught that pokemon");
    return;
  }
  console.log(`Name: ${pokemon.name}`);
  console.log(`Height: ${pokemon.height}`);
  console.log(`Weight: ${pokemon.weight}`);
  console.log(`Stats:`);
  pokemon.stats.forEach((stat) => {
    console.log(`  -${stat.stat.name}: ${stat.base_stat}`);
  });
  console.log(`Types:`);
  pokemon.types.forEach((type) => {
    console.log(`  - ${type.type.name}`);
  });
}
