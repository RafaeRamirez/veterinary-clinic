namespace Veterinary.Models;

    public class Pet
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public required string Species { get; set; }
        public int Age { get; set; }
        public string? Symptom { get; set; }     
        
         public override string ToString()
    {
        return $"[PetID: {Id}] {Name} ({Species}), {Age} años - Síntoma: {Symptom ?? "N/A"}";
    }
    }

