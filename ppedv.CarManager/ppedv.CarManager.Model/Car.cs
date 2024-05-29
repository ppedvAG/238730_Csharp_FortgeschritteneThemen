namespace ppedv.CarManager.Model
{
    public class Car : Entity
    {
        public string Manufacturer { get; set; } = string.Empty;
        public string Model { get; set; } = string.Empty;
        public int KW { get; set; }
        public DateTime BuildDate { get; set; }
        public virtual ICollection<Driver> Drivers { get; set; } = new HashSet<Driver>();
    }
}
