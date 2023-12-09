using LibApp.Models;

namespace LibApp.ViewModels
{
    public class NewBookViewModel
    {
        public IEnumerable<Genre> Genre { get; set; }
        public Book Book { get; set; }
    }
}
