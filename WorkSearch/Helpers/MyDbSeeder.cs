using WorkSearch.DBContext;
using WorkSearch.Models;

namespace WorkSearch.Helpers
{
    public class MyDbSeeder
    {
        private readonly MyDBContext _dbContext;

        private string[] Genders { get; set; } = {"Мужской", "Женский"};
        private string[] Languages { get; set; } = {"Русский", "Английский", "Немецкий", "Фрпанцузский", "Испанский" };

        public MyDbSeeder(MyDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public MyDbSeeder SeedLanguages()
        {
            foreach(var e in Languages)
            {
                _dbContext.Languages.Add(new Language {Name = e });
            }
            _dbContext.SaveChanges();

            return this;
        }

        public MyDbSeeder SeedGenders()
        {
            foreach (var e in Genders)
            {
                _dbContext.Genders.Add(new Gender { Name = e });
            }
            _dbContext.SaveChanges();

            return this;
        }
    }
}
