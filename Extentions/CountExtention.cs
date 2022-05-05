using DvD_Api.Models;

namespace DvD_Api.Extentions;

public static class CountExtention{
    public static int GetTotalCount(this ICollection<Dvdcopy> copies){
        var totalLoans = 0;

            foreach(var copy in copies){
                totalLoans += copy.Loans.Count();
            }

            return totalLoans;
    }
}