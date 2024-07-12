using HelloCookBookAPI.Entities;
using HelloCookBookAPI.Models;
using Microsoft.AspNetCore.Mvc;

/*
 * The user can view, add, update and delete ingredients
 */

namespace HelloCookBookAPI.Controllers
{
    [ApiController]
    [Route("ingredient")]
    public class IngredientController : ControllerBase
    {
        // Gets all ingredients in that specific recipe
        [HttpGet("recipe/{recipeId}")]
        public Result GetAllIngredients(int recipeId)
        {
            var result = new Result();

            try
            {
                var ingredients = new List<Ingredient>();

                using (var db = new CookBookContext())
                {
                    ingredients = db.Ingredients.Where(x => x.RecipeId == recipeId).ToList();
                }

                result.JsonResultObject = ingredients;
                result.Message = "You can view all your ingredients";
                result.IsSuccess = true;

                return result;
            }

            catch
            {
                result.Message = "Unable to get all your ingredients";
                result.IsSuccess = false;

                return result;
            }
        }

        // Add new ingredient to a recipe
        [HttpPost("add")]
        public Result AddNewIngredient([FromBody]IngredientDataInput ingredientNewData)
        {
            var result = new Result();

            try
            {
                // Validate if all inputs are entered
                if (ingredientNewData.Name.Length == 0 || ingredientNewData.Unit.Length == 0)
                {
                    result.Message = "Please enter the required fields";
                    result.IsSuccess = false;

                    return result;
                }

                using (var db = new CookBookContext())
                {
                    Ingredient ingredient = new Ingredient();

                    ingredient.Name = ingredientNewData.Name;
                    ingredient.Unit = ingredientNewData.Unit;
                    ingredient.RecipeId = ingredientNewData.RecipeId;

                    db.Add(ingredient);
                    db.SaveChanges();
                }

                result.Message = "New ingredient added successfully";
                result.IsSuccess = true;

                return result;
            }

            catch
            {
                result.Message = "Unable to add new ingredient";
                result.IsSuccess = false;

                return result;
            }
        }

        // The user can edit an existing ingredient in the recipe
        [HttpPut("edit")]
        public Result UpdateIngredient([FromBody] IngredientDataInput ingredientDataInput)
        {
            var result = new Result();

            try
            {
                // Validate if all inputs are entered
                if (ingredientDataInput.Name.Length == 0 || ingredientDataInput.Unit.Length == 0)
                {
                    result.Message = "Please enter the required fields";
                    result.IsSuccess = false;

                    return result;
                }

                else
                {
                    using (var db = new CookBookContext())
                    {
                        var ingredient = db.Ingredients.Where(x => x.Id == ingredientDataInput.Id).FirstOrDefault();

                        if (ingredient == null)
                        {
                            result.Message = "No ingredient to update";
                            result.IsSuccess = false;

                            return result;
                        }

                        else
                        {
                            ingredient.Name = ingredientDataInput.Name;
                            ingredient.Unit = ingredientDataInput.Unit;

                            db.SaveChanges();

                            result.Message = "Ingredient is successfully updated";
                            result.IsSuccess = true;

                            return result;
                        }
                    }
                }
            }

            catch
            {
                result.Message = "Unable to edit an ingredient";
                result.IsSuccess = false;

                return result;
            }
        }

        // The user can remove an ingredient in the recipe
        [HttpDelete("delete/{id}")]
        public Result DeleteIngredient(int id)
        {
            var result = new Result();

            try
            {
                using (var db = new CookBookContext())
                {
                    var ingredient = db.Ingredients.Where(x => x.Id == id).FirstOrDefault();

                    // Check if the specific ingredient exists
                    if (ingredient == null)
                    {
                        result.Message = "No ingredient to delete";
                        result.IsSuccess = false;

                        return result;
                    }

                    else
                    {
                        db.Ingredients.Remove(ingredient);
                        db.SaveChanges();

                        result.Message = "Ingredient is successfully deleted";
                        result.IsSuccess = true;

                        return result;
                    }
                }
            }

            catch
            {
                result.Message = "Unable to delete an ingredient";
                result.IsSuccess = false;

                return result;
            }
        }
    }
}
