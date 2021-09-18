using System;

namespace opg_5_tcp_server
{
    public class FootballPlayer
    {
        private int _id;
        private string _name;
        private double _price;
        private int _shirt_number;
        public FootballPlayer(int id, string name, double price, int shirt_number)
        {
            Id = id;
            Name = name;
            Price = price;
            Shirt_number = shirt_number;
        }
        public int Id { get => _id; set => _id = value; }
        public string Name
        {
            get => _name;
            set
            {
                if (value.Length < 4)
                {
                    throw new ArgumentOutOfRangeException("Name must be atleast 4 charcters long");
                }
                _name = value;
            }
        }
        public double Price
        {
            get => _price;
            set
            {
                if (value <= 0)
                {
                    throw new ArgumentOutOfRangeException("Price must be higher than 0");
                }
                _price = value;

            }
        }
        public int Shirt_number
        {
            get => _shirt_number;
            set
            {
                if (value > 100 || value < 1)
                {
                    throw new ArgumentOutOfRangeException("Shirt number must be between 1 - 100");
                }
                _shirt_number = value;
            }
        }
    }
}
