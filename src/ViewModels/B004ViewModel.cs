using MetaFrm.Management.Razor.Models;
using MetaFrm.MVVM;

namespace MetaFrm.Management.Razor.ViewModels
{
    /// <summary>
    /// B004ViewModel
    /// </summary>
    public partial class B004ViewModel : BaseViewModel
    {
        /// <summary>
        /// SearchModel
        /// </summary>
        public SearchModel SearchModel { get; set; } = new();

        /// <summary>
        /// SelectResultModel
        /// </summary>
        public List<AccountModel> SelectResultModel { get; set; } = new List<AccountModel>();

        /// <summary>
        /// C001ViewModel
        /// </summary>
        public B004ViewModel()
        {

        }
    }
}