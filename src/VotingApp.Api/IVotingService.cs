using System.Threading.Tasks;
using VotingApp.Domain;

namespace VotingApp.Api
{
    public interface IVotingService
    {
        Task Save(Voting voting);

        Task<Voting> Get();
    }
}