namespace PokemonReviewApp.Models;

public class Owner
{
    public int Id { get; set; }
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string Gym { get; set; } = null!;
    public Country Country { get; set; }
    public ICollection<PokemonOwner> PokemonOwners { get; set; }
    
}

