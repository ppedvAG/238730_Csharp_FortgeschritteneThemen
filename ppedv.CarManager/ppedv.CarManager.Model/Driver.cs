﻿namespace ppedv.CarManager.Model
{
    public class Driver : Entity
    {
        public string Name { get; set; } = string.Empty;
        public virtual ICollection<Car> Cars { get; set; } = new HashSet<Car>();
    }
}
