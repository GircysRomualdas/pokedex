namespace Pokedex.Infrastructure;

public class PokemonRow {
  public int id { get; set; }
  public string name { get; set; }
  public string types { get; set; }
  public int height { get; set; }
  public int weight { get; set; }
  public int base_experience { get; set; }
}