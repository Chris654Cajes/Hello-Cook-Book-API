/*
 * This class is used as user input when adding or updating data
 */

namespace HelloCookBookAPI.Models
{
    public class IngredientDataInput
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Unit { get; set; }
        public int RecipeId { get; set; }
    }
}
