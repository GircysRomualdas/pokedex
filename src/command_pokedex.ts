import type { State } from "./state.js";
import { Pokemon } from "./pokeapi.js";

export async function commandPokedex(state: State) {
  console.log("Your Pokedex:");
  const keys = Object.keys(state.pokedex);
  for (let i = 0; i < keys.length; i++) {
    const pokemonName = state.pokedex[keys[i]].name;
    console.log(` - ${pokemonName}`);
  }
}
