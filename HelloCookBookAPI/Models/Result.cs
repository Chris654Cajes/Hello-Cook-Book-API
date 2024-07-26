/*
 * This class contains results from the API
 */

namespace HelloCookBookAPI.Models
{
    public class Result
    {
        public object JsonResultObject { get; set; }
        public string Message { get; set; }
        public bool IsSuccess { get; set; }
    }
}
