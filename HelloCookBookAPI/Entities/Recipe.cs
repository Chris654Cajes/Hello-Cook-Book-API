using System;
using System.Collections.Generic;

namespace HelloCookBookAPI.Entities;

public partial class Recipe
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public byte[] Picture { get; set; } = null!;

    public int UserId { get; set; }

    public DateTime CreatedAt { get; set; }

    public virtual ICollection<Ingredient> Ingredients { get; set; } = new List<Ingredient>();

    public virtual ICollection<Procedure> Procedures { get; set; } = new List<Procedure>();

    public virtual User User { get; set; } = null!;
}
