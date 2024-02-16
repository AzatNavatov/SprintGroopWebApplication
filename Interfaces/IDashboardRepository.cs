using SprintGroopWebApplication.Models;

namespace SprintGroopWebApplication.Interfaces
{
    public interface IDashboardRepository
    {
        public Task<List<Race>> GetAllUserRaces();
        public Task<List<Club>> GetAllUserClubs();
    }
}
