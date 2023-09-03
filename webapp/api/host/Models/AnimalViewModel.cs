using domain;

namespace host.Models
{
    public class AnimalViewModel
    {
        public int? Id { get; set; }

        public string? Name { get; set; }

        public AnimalViewModel(Animal? animal)
        {
            if (animal == null) { return; }
            Id = animal.Id;
            Name = animal.Name;
        }

        public static IEnumerable<AnimalViewModel> Map(Animal[] animals)
        {
            return animals.Select(a => new AnimalViewModel(a));
        }
    }
}

