
namespace Zora.Web.Models.Tabs.ViewModels
{

    public class PostsTabViewModel
    {
        public Tab ActiveTab { get; set; }
    }

    public enum Tab
    {
        PostsWaitingForApproved,
        PostsApproved,
        PostsNotApproved,
        PostsAll

    }

}
