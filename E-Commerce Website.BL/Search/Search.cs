using E_Commerce_Website.BL.Pagination;
using E_Commerce_Website.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce_Website.BL.Search
{
    public static class Search<T> where T : BaseModel
    {
        public static Tuple<List<T>, Pager> SearchByName(int pg, string searchName, List<T> dataList)
        {
            var data = dataList.Where(o => o.Name.Contains( searchName )&& o.IsDeleted==false).ToList();
            var dataPagination = Pagination<T>.GetPaginationData(pg, data);
            return dataPagination;
        }
        public static Tuple<List<T>, Pager> SearchByNameDelete(int pg, string searchName, List<T> dataList)
        {
            var data = dataList.Where(o => o.Name.Contains(searchName) && o.IsDeleted==true).ToList();
            var dataPagination = Pagination<T>.GetPaginationData(pg, data);
            return dataPagination;
        }
    }
}
