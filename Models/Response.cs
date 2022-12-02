namespace DotNetAssignment.Models
{
    public class Response<T>
    {

        public Response()
        {
            this.Success = true;
            this.Message = string.Empty;
        }

        public T Data { get; set; }
        public bool Success { get; set; }
        public string Message { get; set; }
       
    }
}
