namespace Core.Models
{
    public class Medicament
    {
        public string Id { get; private set; }
        public string Name { get; private set; }
        
        public int FrequencyPerDay { get; private set; }
        public int Days { get; private set; }

        public Medicament(string id, string name, int frequencyPerDay, int days)
        {
            Id = id;
            Name = name;

            FrequencyPerDay = frequencyPerDay;
            Days = days;
        }
    }
}
