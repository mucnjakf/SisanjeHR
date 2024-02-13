namespace DataAccessLayer.Models
{
    public class MethodOfPayment
    {
        public int Id { get; set; }
        public string Method { get; set; }

        public override string ToString()
        {
            return Method;
        }
    }
}
