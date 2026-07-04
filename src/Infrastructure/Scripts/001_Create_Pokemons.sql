CREATE TABLE pokemons (
  id SERIAL PRIMARY KEY,
  name TEXT NOT NULL,
  types TEXT NOT NULL,
  height INT NOT NULL,
  weight INT NOT NULL,
  base_experience INT NOT NULL
);