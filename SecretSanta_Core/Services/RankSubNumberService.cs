using Microsoft.EntityFrameworkCore;
using SecretSanta_Core.Data;

namespace SecretSanta_Core.BusinessLogic
{
    public class RankSubNumberService
    {
        private static volatile Dictionary<int, int> _rankSubNumber;
        private static object _rankListLock = new object();
        private static DateTime _expireDateTime;
        private static readonly Random _random = new Random(Guid.NewGuid().GetHashCode());
        private readonly IDbContextFactory<ApplicationDbContext> _contextFactory;


        public RankSubNumberService(IDbContextFactory<ApplicationDbContext> contextFactory)
        {
            _contextFactory = contextFactory;
            
        }
        
        public int GetRankSubNumber(int recipientNumber)
        {
            if (BusinessMethods.GetLocalDateTime(DateTime.UtcNow) > _expireDateTime)
            {
                _rankSubNumber = CreateList();
            }

            bool lockWasTaken = false;
            Monitor.Enter(_rankListLock, ref lockWasTaken);
            try
            {
                if(_rankSubNumber.ContainsKey(recipientNumber))
                {
                    return _rankSubNumber[recipientNumber];
                }

                return 0;
            }
            finally
            {
                if (lockWasTaken) Monitor.Exit(_rankListLock);
            }

        }

        public Dictionary<int, int> CreateList()
        {
            bool lockWasTaken = false;
            var rankSubNumber = new Dictionary<int, int>();
            List<int> recipients = new List<int>();
            using (var context = _contextFactory.CreateDbContext())
            {
                recipients = context.TblRecipientParents.Select(r => r.RecipientNum).ToList();
            }

            Monitor.Enter(_rankListLock, ref lockWasTaken);
            try
            {
                foreach (var recipient in recipients)
                {
                    var rnd = _random.Next(1, 200000);
                    rankSubNumber.Add(recipient, rnd);

                }

                _expireDateTime = BusinessMethods.GetLocalDateTime(DateTime.UtcNow).AddHours(23).AddMinutes(59)
                    .AddSeconds(59);
            }
            finally
            {
                if (lockWasTaken) Monitor.Exit(_rankListLock);
            }

            return rankSubNumber;
        }
    }
}
