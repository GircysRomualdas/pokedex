import { Cache } from "./pokecache";

export class PokeAPI {
  private static readonly baseURL = "https://pokeapi.co/api/v2";
  #cache: Cache;

  constructor() {
    this.#cache = new Cache(4000);
  }

  async fetchLocations(pageURL?: string): Promise<ShallowLocations> {
    if (!pageURL) {
      pageURL = `${PokeAPI.baseURL}/location-area`;
    }
    const cache: ShallowLocations | undefined =
      this.#cache.get<ShallowLocations>(pageURL);
    if (cache) {
      return cache;
    }
    try {
      const response = await fetch(pageURL, {
        method: "GET",
      });
      if (!response.ok) {
        throw new Error(`${response.status} ${response.statusText}`);
      }
      const location: ShallowLocations = await response.json();
      this.#cache.add<ShallowLocations>(pageURL, location);
      return location;
    } catch (error) {
      throw new Error(`Failed to fetch locations: ${error}`);
    }
  }

  async fetchLocation(locationName: string): Promise<Location> {
    const fullURL = `${PokeAPI.baseURL}/location-area/${locationName}`;
    const cache: Location | undefined = this.#cache.get<Location>(fullURL);
    if (cache) {
      return cache;
    }
    try {
      const response = await fetch(fullURL, {
        method: "GET",
      });
      if (!response.ok) {
        throw new Error(`${response.status} ${response.statusText}`);
      }
      const location: Location = await response.json();
      this.#cache.add<Location>(fullURL, location);
      return location;
    } catch (error) {
      throw new Error(`Failed to fetch location: ${error}`);
    }
  }
}

export type ShallowLocations = {
  next: string;
  previous: string;
  results: {
    name: string;
    url: string;
  }[];
};

export type Location = {
  name: string;
  pokemon_encounters: {
    pokemon: {
      name: string;
    };
  }[];
};
