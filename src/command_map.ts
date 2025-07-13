import type { State } from "./state.js";
import { ShallowLocations } from "./pokeapi.js";

export async function commandMapf(state: State) {
  const shallowlocation: ShallowLocations = await state.pokeAPI.fetchLocations(
    state.nextLocationsURL,
  );
  state.nextLocationsURL = shallowlocation.next;
  state.prevLocationsURL = shallowlocation.previous;

  for (const location of shallowlocation.results) {
    console.log(location.name);
  }
}

export async function commandMapb(state: State) {
  if (!state.prevLocationsURL) {
    console.log("you're on the first page");
    return;
  }
  const shallowlocation: ShallowLocations = await state.pokeAPI.fetchLocations(
    state.prevLocationsURL,
  );
  state.nextLocationsURL = shallowlocation.next;
  state.prevLocationsURL = shallowlocation.previous;

  for (const location of shallowlocation.results) {
    console.log(location.name);
  }
}
