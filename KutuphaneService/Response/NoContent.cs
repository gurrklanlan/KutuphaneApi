namespace KutuphaneService.Response
{
    public class NoContent : Response
    {
        public NoContent() : base(true, "No Content")
        {

        }

        public static NoContent Success()
        {
            return new NoContent();
        }
    }
}
