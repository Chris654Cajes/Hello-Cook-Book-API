using HelloCookBookAPI.Entities;
using HelloCookBookAPI.Models;
using Microsoft.AspNetCore.Mvc;

/*
 * The user can view, add, update and delete recipes
 */

namespace HelloCookBookAPI.Controllers
{
    [ApiController]
    [Route("recipe")]
    public class RecipeController : ControllerBase
    {
        // The user can view all the recipes that he/she has created
        [HttpGet("user/{userId}")]
        public Result GetAllRecipes(int userId)
        {
            var result = new Result();

            try
            {
                var recipes = new List<Recipe>();

                using (var db = new CookBookContext())
                {
                    recipes = db.Recipes.Where(x => x.UserId == userId).ToList();
                }

                result.JsonResultObject = recipes;
                result.Message = "You can view all your recipes";
                result.IsSuccess = true;

                return result;
            }

            catch 
            {
                result.Message = "Unable to get all your recipes";
                result.IsSuccess = false;

                return result;
            }
        }

        // The user can search the recipes that contains search filter keyword
        [HttpGet("user/{userId}/{recipeKeyword}")]
        public Result GetAllRecipes(int userId, string recipeKeyword)
        {
            var result = new Result();

            try
            {
                var recipes = new List<Recipe>();

                using (var db = new CookBookContext())
                {
                    recipes = db.Recipes.Where(x => x.UserId == userId && x.Name.Contains(recipeKeyword)).ToList();
                }

                result.JsonResultObject = recipes;
                result.Message = "You can view all your recipes";
                result.IsSuccess = true;

                return result;
            }

            catch
            {
                result.Message = "Unable to get all your recipes";
                result.IsSuccess = false;

                return result;
            }
        }

        // The user can view the specific recipe that he/she has created
        [HttpGet("{id}")]
        public Result GetRecipe(int id)
        {
            var result = new Result();

            try
            {
                var recipe = new Recipe();

                using (var db = new CookBookContext())
                {
                    recipe = db.Recipes.Where(x => x.Id == id).FirstOrDefault();
                }

                result.JsonResultObject = recipe;
                result.Message = "You can view your recipe";
                result.IsSuccess = true;

                return result;
            }

            catch
            {
                result.Message = "Unable to get your recipe";
                result.IsSuccess = false;

                return result;
            }

        }

        // The user can add new recipe in his/her cook book
        [HttpPost("add")]
        public Result AddNewRecipe([FromBody]RecipeDataInput recipeDataInput)
        {
            var result = new Result();

            try
            {
                // Validate if all inputs are entered
                if (recipeDataInput.Name.Length == 0 || recipeDataInput.Picture.Length == 0)
                {
                    result.Message = "Please enter the required fields";
                    result.IsSuccess = false;

                    return result;
                }

                // If all fields are entered, add this new recipe to the list
                using (var db = new CookBookContext())
                {
                    Recipe recipe = new Recipe();

                    recipe.Name = recipeDataInput.Name;
                    recipe.Picture = recipeDataInput.Picture;
                    recipe.UserId = recipeDataInput.UserId;

                    db.Add(recipe);
                    db.SaveChanges();
                }

                result.Message = "New recipe added successfully";
                result.IsSuccess = true;

                return result;
            }

            catch
            {
                result.Message = "Unable to add new recipe";
                result.IsSuccess = false;

                return result;
            }
        }

        // The user can edit a specific recipe in his/her cook book
        [HttpPut("edit")]
        public Result UpdateRecipe([FromBody] RecipeDataInput recipeDataInput)
        {
            var result = new Result();

            try
            {
                // Validate if all inputs are entered
                if (recipeDataInput.Name.Length == 0 || recipeDataInput.Picture.Length == 0)
                {
                    result.Message = "Please enter the required fields";
                    result.IsSuccess = false;

                    return result;
                }

                else
                {
                    using (var db = new CookBookContext())
                    {
                        var recipe = db.Recipes.Where(x => x.Id == recipeDataInput.Id).FirstOrDefault();

                        // Check if recipe exists for update
                        if (recipe == null)
                        {
                            result.Message = "No recipe to update";
                            result.IsSuccess = false;

                            return result;
                        }

                        else
                        {
                            recipe.Name = recipeDataInput.Name;
                            recipe.Picture = recipeDataInput.Picture;

                            db.SaveChanges();

                            result.Message = "Recipe is successfully updated";
                            result.IsSuccess = true;

                            return result;
                        }
                    }
                }
            }

            catch
            {
                result.Message = "Unable to edit recipe";
                result.IsSuccess = false;

                return result;
            }
        }

        // The user can delete specific recipe in his/her cook book
        [HttpDelete("delete/{id}")]
        public Result DeleteRecipe(int id)
        {
            var result = new Result();

            try
            {
                using (var db = new CookBookContext())
                {
                    var recipe = db.Recipes.Where(x => x.Id == id).FirstOrDefault();

                    // Check if the specific recipe exists
                    if (recipe == null)
                    {
                        result.Message = "No recipe to delete";
                        result.IsSuccess = false;

                        return result;
                    }

                    else
                    {
                        db.Recipes.Remove(recipe);
                        db.SaveChanges();

                        result.Message = "Recipe and all the ingredients and procedures are successfully deleted";
                        result.IsSuccess = true;

                        return result;
                    }
                }
            }

            catch
            {
                result.Message = "Unable to delete recipe";
                result.IsSuccess = false;

                return result;
            }
        }
    }
}
