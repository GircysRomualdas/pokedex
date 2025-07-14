import type { State } from "./state.js";
import { LocationDetail } from "./pokeapi.js";

export async function commandExplore(state: State, ...args: string[]) {
  if (!args.length) {
    console.log("explore requires an location name");
    return;
  }
  const areaName = args[0];
  try {
    const location: LocationDetail =
      await state.pokeAPI.fetchLocation(areaName);

    console.log(`Exploring ${areaName}...`);
    console.log("Found Pokemon:");
    for (const encounter of location.pokemon_encounters) {
      console.log(` - ${encounter.pokemon.name}`);
    }
  } catch (error) {
    console.error(`Error exploring ${areaName}:`, error);
  }
}
