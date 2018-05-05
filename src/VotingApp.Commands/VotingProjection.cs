using System;
using System.Collections.Generic;
using System.Linq;
using Marten;
using Marten.Events.Projections;
using Marten.Services;
using VotingApp.Domain;

namespace VotingApp.Commands
{
    public class VotingProjection : ViewProjection<CurrentVotingAggregate, Guid>
    {        
        public VotingProjection()
        {
            ProjectEvent<VotingStartedEvent>
            (
                (s, e) => SelectProjection(s, e.Id),
                (p, e) => p.Id = e.Id
            );

            DeleteEvent<VotingFinishedEvent>(e => e.Id);

            Guid SelectProjection(IDocumentSession session, Guid quizId) =>
                session.Load<CurrentVotingAggregate>(quizId)?.Id ?? quizId;
        }
    }
}   