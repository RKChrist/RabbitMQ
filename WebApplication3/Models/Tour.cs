namespace Rab
{
    public enum Tours
    {
        France,
        Denmark,
        Sweden,
        Spain
    }
    
    public class Tour
    {

        public Guid Id { get; set; } = Guid.NewGuid();
        public string? Name { get; set; }

        public string? Email { get; set; } 

        public Tours Tours { get; set; }

        private bool _book;
        public bool Book
        {
            get => _book; 
            set
            {
                if (_cancel && value) Cancel = false;
                _book = value;
            }
        }
        private bool _cancel;
        public bool Cancel
        {
            get => _cancel;
            set
            {
                if (_book && value)
                {
                    Book = false;
                }
                _cancel = value;
            }
        }


        public override string ToString()
        {
            return Name + "," + Email + "," + Tours + "," + Book;
        }
    }
}
