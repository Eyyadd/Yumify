namespace Yumify.API.Helper.Sorting
{
    public class Sorting
    {
        public bool IsAscending { get; set; }=true;
        public SortingBy SortingBy { get; set; }= SortingBy.Id;
    }
}
