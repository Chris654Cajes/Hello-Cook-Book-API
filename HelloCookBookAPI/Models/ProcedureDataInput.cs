/*
 * This class is used as user input when adding or updating data
 */

namespace HelloCookBookAPI.Models
{
    public class ProcedureDataInput
    {
        public int Id { get; set; }
        public string Procedure { get; set; }
        public int RecipeId { get; set; }
    }
}
