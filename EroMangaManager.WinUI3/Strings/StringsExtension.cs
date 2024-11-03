namespace EroMangaManager.WinUI3.Strings;
///<summary> </summary>
public class StringsExtension : MarkupExtension
{
    ///<summary> </summary>
    public StringsEnum Uid { get; set; }

    ///<summary> </summary>
    protected override object ProvideValue ()
    {
        return ResourceLoader.GetForViewIndependentUse().GetString(Uid.ToString());
    }
}