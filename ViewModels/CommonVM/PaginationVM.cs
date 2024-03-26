using System.Collections;

namespace PustokBook.ViewModels.CommonVM
{
    public class PaginationVM<T> where T : IEnumerable
    {
        public int TotalCount { get; set; }
        public int NextPage { get; set; }
        public int LastPage { get; set; }
        public bool HasPrev { get; set; }
        public bool HasNext { get; set; }
        public T Items { get; set; }

        public PaginationVM(int totalCount, int lastPage, int nextPage, T items)
        {
            if (lastPage <= 0)
                throw new ArgumentException();

            if (lastPage <= nextPage)
            {
                if (nextPage != 1 && nextPage == lastPage)
                {
                    HasPrev = true;
                }
                else if (nextPage > lastPage)
                {
                    HasNext = true;
                }
            }

            TotalCount = totalCount;
            Items = items;
            NextPage = nextPage;
            LastPage = lastPage;
        }
    }
}
