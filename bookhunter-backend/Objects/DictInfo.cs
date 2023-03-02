using BookHunter_Backend.Domain.Models;

namespace BookHunter_Backend.Objects
{
    public class DictInfo
    {
        public int Id { get; set; }

        public string Name { get; set; } = String.Empty;

        public string Message { get; set; } = "ok";

        public static DictInfo Success(int id, string name)
        {
            return new DictInfo()
            {
                Id = id,
                Name = name
            };
        }

        public static DictInfo Fail(string message)
        {
            return new DictInfo()
            {
                Message = message
            };
        }
    }
}