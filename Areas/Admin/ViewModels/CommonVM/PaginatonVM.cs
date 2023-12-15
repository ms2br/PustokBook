using System.Collections;

namespace PustokBook.Areas.Admin.ViewModels.CommonVM
{
    public class PaginatonVM<T> where T : IEnumerable
    {
        public int TotalCount { get; }
        public int LastPage { get; } // Oludugum Yer
        public int NextPage { get; } // Novbeti Seyfe
        public bool HasPrev { get; } = false;
        public bool NextPrev { get; } = true;
        public T Item { get; }

        public PaginatonVM(int totalCount, int lastPage, int nextPage, T item)
        {
            TotalCount = totalCount;
            Item = item;
            LastPage = lastPage;
            NextPage = nextPage;
            Item = item;
            if (nextPage >= lastPage)
            {
                if (nextPage == lastPage)
                {
                    NextPrev = false;
                    HasPrev = true;
                }
                if (nextPage == 1)
                {
                    HasPrev = false;
                }
            }
        }
    }
}
