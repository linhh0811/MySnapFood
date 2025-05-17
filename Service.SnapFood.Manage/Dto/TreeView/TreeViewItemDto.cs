using Microsoft.FluentUI.AspNetCore.Components;

namespace Service.SnapFood.Manage.Dto.TreeView
{
    public class TreeViewItemDto : ITreeViewItem
    {
        public string Id { get; set; } = string.Empty;
        public string Text { get; set; } = string.Empty;
        public int ModerationStatus { get; set; }
        public IEnumerable<ITreeViewItem>? Items { get; set; }
        public Icon? IconCollapsed { get; set; }
        public Icon? IconExpanded { get; set; }
        public bool Disabled { get; set; }
        public bool Expanded { get; set; }
        public Func<TreeViewItemExpandedEventArgs, Task>? OnExpandedAsync { get; set; }
    }
}
