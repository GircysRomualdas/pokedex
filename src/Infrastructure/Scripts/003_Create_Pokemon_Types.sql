CREATE TABLE pokemon_types (
  pokemon_id INT NOT NULL REFERENCES pokemons(id),
  type_id INT NOT NULL REFERENCES types(id),
  PRIMARY KEY (pokemon_id, type_id)
);