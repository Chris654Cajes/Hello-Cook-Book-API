/*
 * This class is used as user input when adding or updating data
 */

namespace HelloCookBookAPI.Models
{
    public class RecipeDataInput
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public byte[] Picture { get; set; }
        public int UserId { get; set; }
    }
}
