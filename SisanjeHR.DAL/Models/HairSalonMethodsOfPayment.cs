namespace DataAccessLayer.Models
{
    public class HairSalonMethodsOfPayment
    {
        public int Id { get; set; }

        public int HairSalonId { get; set; }
        public HairSalon HairSalon { get; set; }

        public int MethodOfPaymentId { get; set; }
        public MethodOfPayment MethodOfPayment { get; set; }

        public override string ToString()
        {
            return $"{MethodOfPayment.Method}";
        }
    }
}
