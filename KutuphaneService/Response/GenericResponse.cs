using KutuphaneService.Interfaces;

namespace KutuphaneService.Response
{
    public class GenericResponse<T> : Response,IResponse<T>
    {
        public T Data { get; set; }

        public GenericResponse(bool isSuccess, string message, T data) : base(isSuccess, message)
        {
            Data = data;
        }

        public static GenericResponse<T>Success(string message="", T data=default)
        {
            return new GenericResponse<T>(true, message, data);
        }

        public static GenericResponse<T> Error(string message="", T data = default)
        {
            return new GenericResponse<T>(false, message, default(T));
        }
    }
}
