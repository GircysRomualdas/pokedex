import type { State } from "./state.js";
import { Pokemon } from "./pokeapi.js";

export async function commandCatch(state: State, ...args: string[]) {
  if (!args.length) {
    console.log("catching requires an Pokemon name");
    return;
  }
  const pokemonName = args[0];
  console.log(`Throwing a Pokeball at ${pokemonName}...`);
  try {
    const pokemon: Pokemon = await state.pokeAPI.fetchPokemon(pokemonName);

    const baseExp = pokemon.base_experience;
    const maxExp = 200;
    const catchProbability = 1 - baseExp / maxExp;

    if (Math.random() < catchProbability) {
      console.log(`${pokemon.name} was caught!`);
      state.pokedex[pokemonName] = pokemon;
      console.log("You may now inspect it with the inspect command.");
    } else {
      console.log(`${pokemon.name} escaped!`);
    }
  } catch (error) {
    console.error(`Error catching ${pokemonName}:`, error);
  }
}
