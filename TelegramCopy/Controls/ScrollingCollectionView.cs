using System.Runtime.CompilerServices;

namespace TelegramCopy.Controls;

public class ScrollingCollectionView : CollectionView
{
    #region -- Overrides --

    protected override void OnPropertyChanged([CallerMemberName] string propertyName = null)
    {
        base.OnPropertyChanged(propertyName);

    }

    #endregion
}

