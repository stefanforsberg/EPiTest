using System;
using System.Collections.Generic;
using EPiServer.ChangeLog;

namespace EPiTest.DB
{
    public class NullChangeLog : IChangeLog
    {
        public void Save(IChangeLogItem item)
        {
            return;
        }

        public IList<IChangeLogItem> GetChanges(ChangeLogQueryInfo queryInfo, ReadDirection direction, SortOrder order)
        {
            return new List<IChangeLogItem>();
        }

        public long GetChangeCount(ChangeLogQueryInfo queryInfo)
        {
            return 0;
        }

        public long GetChangeCountBackwards(ChangeLogQueryInfo queryInfo)
        {
            return 0;
        }

        public long GetChangeCountForward(ChangeLogQueryInfo queryInfo)
        {
            return 0;
        }

        public long GetHighestSequenceNumber()
        {
            return 0;
        }

        public void Truncate(long rows, DateTime olderThan)
        {
            return;
        }

        public void TruncateByDependency(DateTime defaultOldestChangeDate)
        {
            return;
        }

        public void Save(IEnumerable<IChangeLogItem> items)
        {
            return;
        }
    }
}