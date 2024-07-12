using System;
using System.Collections.Generic;

namespace HelloCookBookAPI.Entities;

public partial class Procedure
{
    public int Id { get; set; }

    public string Procedure1 { get; set; } = null!;

    public int RecipeId { get; set; }

    public DateTime CreatedAt { get; set; }

    public virtual Recipe Recipe { get; set; } = null!;
}
