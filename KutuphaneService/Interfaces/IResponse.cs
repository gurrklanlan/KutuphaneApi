namespace KutuphaneService.Interfaces
{
    public interface IResponse<T>
    {
        public T Data { get; set; }
        bool IsSuccess { get; }
        string Message { get; }


    }
}
