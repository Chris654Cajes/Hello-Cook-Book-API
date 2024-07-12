using System;
using System.Collections.Generic;

namespace HelloCookBookAPI.Entities;

public partial class Ingredient
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string Unit { get; set; } = null!;

    public int RecipeId { get; set; }

    public DateTime CreatedAt { get; set; }

    public virtual Recipe Recipe { get; set; } = null!;
}
