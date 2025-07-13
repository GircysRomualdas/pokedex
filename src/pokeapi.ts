export class PokeAPI {
  private static readonly baseURL = "https://pokeapi.co/api/v2";

  constructor() {}

  async fetchLocations(pageURL?: string): Promise<ShallowLocations> {
    if (!pageURL) {
      pageURL = `${PokeAPI.baseURL}/location-area`;
    }
    try {
      const response = await fetch(pageURL, {
        method: "GET",
      });
      if (!response.ok) {
        throw new Error(`${response.status} ${response.statusText}`);
      }
      const location: ShallowLocations = await response.json();
      return location;
    } catch (error) {
      throw new Error(`Failed to fetch locations: ${error}`);
    }
  }

  async fetchLocation(locationName: string): Promise<Location> {
    const fullURL = `${PokeAPI.baseURL}/location-area/${locationName}`;
    try {
      const response = await fetch(fullURL, {
        method: "GET",
      });
      if (!response.ok) {
        throw new Error(`${response.status} ${response.statusText}`);
      }
      const location: Location = await response.json();
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
