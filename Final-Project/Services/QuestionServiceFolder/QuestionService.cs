using Final_Project.Models;
using Final_Project.Services;

namespace Final_Project.Services.QuestionServiceFolder
{
    public class QuestionService : IQuestionService
    {
        private readonly ApplicationDbContext _db;

        public QuestionService(ApplicationDbContext db)
        {
            _db = db;
        }

        public Question Create(Question Entity)
        {
            _db.Questions.Add(Entity);
            _db.SaveChanges();
            return Entity;
        }

        public bool Delete(int id)
        {
            Question question = Find(id);
            if (question != null)
            {
                _db.Questions.Remove(question);
                return true;
            }
            return false;
        }

        public Question Find(int id)
        {
            return _db.Questions.Find(id);
        }

        public List<Question> GetAll()
        {
            return _db.Questions.ToList();
        }

        public Question Update(Question Entity)
        {
            _db.Questions.Update(Entity);
            _db.SaveChanges();
            return Entity;
        }
    }
}
