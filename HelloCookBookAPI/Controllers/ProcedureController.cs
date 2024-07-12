using HelloCookBookAPI.Entities;
using HelloCookBookAPI.Models;
using Microsoft.AspNetCore.Mvc;

/*
 * The user can view, add, update and delete procedures
 */

namespace HelloCookBookAPI.Controllers
{
    [ApiController]
    [Route("procedure")]
    public class ProcedureController : ControllerBase
    {
        // Gets all procedures in that specific recipe
        [HttpGet("recipe/{recipeId}")]
        public Result GetAllProcedures(int recipeId)
        {
            var result = new Result();

            try
            {
                var procedures = new List<Procedure>();

                using (var db = new CookBookContext())
                {
                    procedures = db.Procedures.Where(x => x.RecipeId == recipeId).ToList();
                }

                result.JsonResultObject = procedures;
                result.Message = "You can view all your procedures";
                result.IsSuccess = true;

                return result;
            }

            catch
            {
                result.Message = "Unable to get all your procedures";
                result.IsSuccess = false;

                return result;
            }
        }

        // Add new procedure to a recipe
        [HttpPost("add")]
        public Result AddNewProcedure([FromBody]ProcedureDataInput procedureDataInput)
        {
            var result = new Result();

            try
            {
                // Validate if all inputs are entered
                if (procedureDataInput.Procedure.Length == 0)
                {
                    result.Message = "Please enter the required fields";
                    result.IsSuccess = false;

                    return result;
                }

                using (var db = new CookBookContext())
                {
                    Procedure procedure = new Procedure();

                    procedure.Procedure1 = procedureDataInput.Procedure;
                    procedure.RecipeId = procedureDataInput.RecipeId;

                    db.Add(procedure);
                    db.SaveChanges();
                }

                result.Message = "New procedure added successfully";
                result.IsSuccess = true;

                return result;
            }

            catch
            {
                result.Message = "Unable to add new procedure";
                result.IsSuccess = false;

                return result;
            }
        }

        // The user can edit an existing procedure in the recipe
        [HttpPut("edit")]
        public Result UpdateProcedure([FromBody] ProcedureDataInput procedureDataInput)
        {
            var result = new Result();

            try
            {
                // Validate if all inputs are entered
                if (procedureDataInput.Procedure.Length == 0)
                {
                    result.Message = "Please enter the required fields";
                    result.IsSuccess = false;

                    return result;
                }

                else
                {
                    using (var db = new CookBookContext())
                    {
                        var procedure = db.Procedures.Where(x => x.Id == procedureDataInput.Id).FirstOrDefault();

                        if (procedure == null)
                        {
                            result.Message = "No procedure to update";
                            result.IsSuccess = false;

                            return result;
                        }

                        else
                        {
                            procedure.Procedure1 = procedureDataInput.Procedure;

                            db.SaveChanges();

                            result.Message = "Procedure is successfully updated";
                            result.IsSuccess = true;

                            return result;
                        }
                    }
                }
            }

            catch
            {
                result.Message = "Unable to edit a procedure";
                result.IsSuccess = false;

                return result;
            }
        }

        // The user can remove a procedure in the recipe
        [HttpDelete("delete/{id}")]
        public Result DeleteProcedure(int id)
        {
            var result = new Result();

            try
            {
                using (var db = new CookBookContext())
                {
                    var procedure = db.Procedures.Where(x => x.Id == id).FirstOrDefault();

                    // Check if the specific ingredient exists
                    if (procedure == null)
                    {
                        result.Message = "No procedure to delete";
                        result.IsSuccess = false;

                        return result;
                    }

                    else
                    {
                        db.Procedures.Remove(procedure);
                        db.SaveChanges();

                        result.Message = "Procedure is successfully deleted";
                        result.IsSuccess = true;

                        return result;
                    }
                }
            }

            catch
            {
                result.Message = "Unable to delete a procedure";
                result.IsSuccess = false;

                return result;
            }
        }
    }
}
