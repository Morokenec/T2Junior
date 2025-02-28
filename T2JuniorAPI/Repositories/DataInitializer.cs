using T2JuniorAPI.Data;
using T2JuniorAPI.Entities;

namespace T2JuniorAPI.Repositories
{
    public static class DataInitializer
    {
        public static void Initialize(ApplicationDbContext context)
        {
            if (!context.InitiativeStatuses.Any())
            {
                var statuses = new List<InitiativeStatus>
            {
                new InitiativeStatus { Id = Guid.NewGuid(), Name = "Предложено" },
                new InitiativeStatus { Id = Guid.NewGuid(), Name = "Рассматривается" },
                new InitiativeStatus { Id = Guid.NewGuid(), Name = "Утверждено" },
                new InitiativeStatus { Id = Guid.NewGuid(), Name = "Реализовано" },
                new InitiativeStatus { Id = Guid.NewGuid(), Name = "Отклонено" }
            };

                context.InitiativeStatuses.AddRange(statuses);
                context.SaveChanges();
            }
        }
    }

}
