using HelloCookBookAPI.Entities;
using HelloCookBookAPI.Models;
using Microsoft.AspNetCore.Mvc;

/*
 * The users can register and login to account
 * The user can update user account, change password, and delete account
 */

namespace HelloCookBookAPI.Controllers
{
    [ApiController]
    [Route("user")]
    public class UserController : ControllerBase
    {
        // API Controller Method for user login. If no data found, the input username and password in incorrect
        [HttpPost("login")]
        public Result Index([FromBody] UserLoginDataInput userLoginDataInput)
        {
            var result = new Result();

            try
            {
                // Validate if all inputs are entered
                if (userLoginDataInput.Username.Length == 0 || userLoginDataInput.Password.Length == 0)
                {
                    result.Message = "Please enter the required fields";
                    result.IsSuccess = false;

                    return result;
                }

                var user = new User();

                using (var db = new CookBookContext())
                {
                    user = db.Users.Where(x => x.Username == userLoginDataInput.Username && x.Password == userLoginDataInput.Password).FirstOrDefault();
                }

                // Check if the specific user exists
                if (user != null)
                {
                    result.JsonResultObject = user;
                    result.Message = "User login successful";
                    result.IsSuccess = true;

                    return result;
                }

                else
                {
                    result.Message = "Incorrect username or password";
                    result.IsSuccess = false;

                    return result;
                }
            }

            catch
            {
                result.Message = "Unable to login user";
                result.IsSuccess = false;

                return result;
            }

        }

        // User sign up registration if the user has no account yet
        [HttpPost("add")]
        public Result InsertUser([FromBody] UserDataInput userDataInput)
        {
            var result = new Result();

            try
            {
                // Check if the account has already existed. if it is, the user will be asked to input different username
                using (var db = new CookBookContext())
                {
                    var user = db.Users.Where(x => x.Username == userDataInput.Username).FirstOrDefault();

                    // Check if the specific user exists
                    if (user != null)
                    {
                        result.Message = "This user account " + userDataInput.Username + " already exists";
                        result.IsSuccess = false;

                        return result;
                    }
                }

                // Validate if all inputs are entered
                if (userDataInput.Username.Length == 0 || userDataInput.Firstname.Length == 0 || userDataInput.Lastname.Length == 0 || userDataInput.Password.Length == 0 || userDataInput.ConfirmPassword.Length == 0)
                {
                    result.Message = "Please enter the required fields";
                    result.IsSuccess = false;

                    return result;
                }

                // Validate that the password and confirmed password should match
                else if (userDataInput.Password != userDataInput.ConfirmPassword)
                {
                    result.Message = "The new password should match with the confirm password";
                    result.IsSuccess = false;

                    return result;
                }

                else
                {
                    using (var db = new CookBookContext())
                    {
                        var user = new User();

                        user.Username = userDataInput.Username;
                        user.Firstname = userDataInput.Firstname;
                        user.Lastname = userDataInput.Lastname;
                        user.Password = userDataInput.Password;

                        db.Add(user);
                        db.SaveChanges();

                        result.Message = "New user record is successfully registered";
                        result.IsSuccess = true;

                        return result;
                    }
                }
            }

            catch
            {
                result.Message = "Unable to register your user account";
                result.IsSuccess = false;

                return result;
            }
        }

        // The user can edit his/her user account information
        [HttpPut("edit")]
        public Result UpdateUser([FromBody] UserDataInput userDataInput)
        {
            var result = new Result();

            try
            {
                // Validate if all inputs are entered
                if (userDataInput.Username.Length == 0 || userDataInput.Firstname.Length == 0 || userDataInput.Lastname.Length == 0)
                {
                    result.Message = "Please enter the required fields";
                    result.IsSuccess = false;

                    return result;
                }

                else
                {
                    using (var db = new CookBookContext())
                    {
                        var user = db.Users.Where(x => x.Id == userDataInput.Id).FirstOrDefault();
                        var usernameExists = db.Users.Where(x => x.Username == userDataInput.Username).FirstOrDefault();

                        // Check if the specific user exists
                        if (user == null)
                        {
                            result.Message = "No user data to update";
                            result.IsSuccess = false;

                            return result;
                        }

                        else
                        {
                            // Check if the specific username exists
                            if (usernameExists != null)
                            {
                                result.Message = "This user account " + userDataInput.Username + " already exists";
                                result.IsSuccess = false;

                                return result;
                            }

                            user.Username = userDataInput.Username;
                            user.Firstname = userDataInput.Firstname;
                            user.Lastname = userDataInput.Lastname;

                            db.SaveChanges();

                            result.Message = "User record is successfully updated";
                            result.IsSuccess = true;

                            return result;
                        }
                    }
                }
            }

            catch
            {
                result.Message = "Unable to edit your user account";
                result.IsSuccess = false;

                return result;
            }
        }

        // The user can change password for their account
        [HttpPut("changePassword")]
        public Result UpdateUserPassword([FromBody] UserDataInput userDataInput)
        {
            var result = new Result();

            try
            {
                // Validate if all inputs are entered
                if (userDataInput.Password.Length == 0 || userDataInput.ConfirmPassword.Length == 0)
                {
                    result.Message = "Please enter the required fields";
                    result.IsSuccess = false;

                    return result;
                }

                // Validate that the password and confirmed password should match
                if (userDataInput.Password != userDataInput.ConfirmPassword)
                {
                    result.Message = "The new password should match with the confirm password";
                    result.IsSuccess = false;

                    return result;
                }

                else
                {
                    using (var db = new CookBookContext())
                    {
                        var user = db.Users.Where(x => x.Id == userDataInput.Id).FirstOrDefault();

                        // Check if the specific user exists
                        if (user == null)
                        {
                            result.Message = "No user data for password update";
                            result.IsSuccess = false;

                            return result;
                        }

                        else
                        {
                            user.Password = userDataInput.Password;

                            db.SaveChanges();

                            result.Message = "User password is successfully updated";
                            result.IsSuccess = true;

                            return result;
                        }
                    }
                }
            }

            catch
            {
                result.Message = "Unable to update your password";
                result.IsSuccess = false;

                return result;
            }
        }

        // The user account will be deleted
        [HttpDelete("delete/{id}")]
        public Result DeleteUser(int id)
        {
            var result = new Result();

            try
            {
                using (var db = new CookBookContext())
                {
                    var user = db.Users.Where(x => x.Id == id).FirstOrDefault();

                    // Check if the specific user exists
                    if (user == null)
                    {
                        result.Message = "No user data to delete";
                        result.IsSuccess = false;

                        return result;
                    }

                    else
                    {
                        db.Users.Remove(user);
                        db.SaveChanges();

                        result.Message = "User is successfully deleted";
                        result.IsSuccess = true;

                        return result;
                    }
                }
            }

            catch
            {
                result.Message = "Unable to delete your account";
                result.IsSuccess = false;

                return result;
            }
        }
    }
}
