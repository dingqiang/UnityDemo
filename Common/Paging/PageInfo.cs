using System;

namespace Common.Paging
{
    public class PageInfo
    {
        #region Property

        /// <summary>
        /// 每页记录数
        /// </summary>
        public int Size { get; set; }

        /// <summary>
        /// 当前页码
        /// </summary>
        public int Current { get; set; }

        /// <summary>
        /// 总页数
        /// </summary>
        public int Count
        {
            get { return (int)Math.Ceiling(Amount / (double)Size); }
        }

        /// <summary>
        /// 总记录数
        /// </summary>
        public int Amount { get; set; }

        /// <summary>
        /// 当前页起始记录索引
        /// </summary>
        public int CurrentRecordBegin
        {
            get
            {
                if (Amount == 0) return 0;
                return (Current - 1) * Size + 1;
            }
        }

        /// <summary>
        /// 当前页结束记录索引
        /// </summary>
        public int CurrentRecordEnd
        {
            get
            {
                if (Current * Size > Amount)
                {
                    return (Current - 1) * Size + Amount % Size;
                }
                return Current * Size;
            }
        }

        public int Previous
        {
            get { return Current - 1 <= 0 ? 1 : Current - 1; }
        }

        public int Next
        {
            get { return Current + 1 >= Count ? Count : Current + 1; }
        }

        #endregion

        public PageInfo()
            : this(10)
        {
        }

        public PageInfo(int pageSize)
        {
            Size = pageSize;
        }
    }
}