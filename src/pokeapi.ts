import { Cache } from "./pokecache.js";

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

  async fetchLocation(locationName: string): Promise<LocationDetail> {
    const fullURL = `${PokeAPI.baseURL}/location-area/${locationName}`;
    const cache: LocationDetail | undefined =
      this.#cache.get<LocationDetail>(fullURL);
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
      const location: LocationDetail = await response.json();
      this.#cache.add<LocationDetail>(fullURL, location);
      return location;
    } catch (error) {
      throw new Error(`Failed to fetch location: ${error}`);
    }
  }

  async fetchPokemon(pokemonName: string): Promise<Pokemon> {
    const fullURL = `${PokeAPI.baseURL}/pokemon/${pokemonName}`;
    const cache: Pokemon | undefined = this.#cache.get<Pokemon>(fullURL);
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
      const pokemon: Pokemon = await response.json();
      this.#cache.add<Pokemon>(fullURL, pokemon);
      return pokemon;
    } catch (error) {
      throw new Error(`Failed to fetch pokemon: ${error}`);
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

export type LocationDetail = {
  name: string;
  pokemon_encounters: {
    pokemon: {
      name: string;
    };
  }[];
};

export type Pokemon = {
  name: string;
  base_experience: number;
  height: number;
  weight: number;
  stats: {
    base_stat: number;
    stat: {
      name: string;
    };
  }[];
  types: {
    type: {
      name: string;
    };
  }[];
};
