namespace Capstone.Api.Common.ResponseApi.Model
{
    public class ResponseSuccess<T>
    {
        public int StatusCode { get; set; }
        public T? Data {  get; set; }
    }
}
