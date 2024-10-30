namespace EroMangaManager.WinUI3.LocalizationWords
{
    ///<summary> </summary>
    public enum Appxmanifest
    {
        ///<summary></summary>
        AppDisplayName,

        ///<summary></summary>
        Description,
    }

    ///<summary> </summary>
    public enum Strings
    {
        Author,
        Translator,

        ///<summary></summary>
        ContainInvalaidChar,

        ///<summary></summary>
        DontUseEmptyString,

        ///<summary></summary>
        ErrorString1,

        ///<summary></summary>
        ExportDone,
    }

    ///<summary> </summary>
    public enum UI
    {
        ChineseTranslateTeam,
        Import,
        Rename,
        UnCategoryed,
        Save,
        NameWithoutBrackets,
        AddCategory,
        IrregularName,
        SeriesMangas,
        NeedRestart,
        Library,
        AddExe,
        InternalReadPage,
        CustomExe,
        PasswordManagement,
        LoadSubDirectory,
        AppUILanguage,
        zhCN,
        en,
        ThumanailViewMode,
        TagsViewMode,

        /// <summary>
        ///
        /// </summary>
        Remove,

        SwitchViewMode,
        WhetherPickSubFolder,
        SelectExe,
        OSRelated,
        DefaultWay,
        MangaOpenWay,

        /// <summary>
        ///
        /// </summary>
        SearchByTag,

        /// <summary>
        ///
        /// </summary>
        SearchByMangaName,

        ///<summary></summary>
        AddAFolder,

        ///<summary></summary>
        Bookcase,

        ///<summary></summary>
        BookcaseItemToolTip,

        ///<summary></summary>
        CancleEdit,

        ///<summary></summary>
        ChangeSortMethod,

        ///<summary></summary>
        Close,

        ///<summary></summary>
        CommonSetting,

        ///<summary></summary>
        ConfirmEdit,

        ///<summary></summary>
        DeleteFile,

        ///<summary></summary>
        DeleteIt,

        ///<summary></summary>
        DeleteMode,

        ///<summary></summary>
        DeleteMode_JustDelete,

        ///<summary></summary>
        DeleteMode_MoveTo,

        ///<summary></summary>
        DeleteSourceFile,

        ///<summary></summary>
        DontShowThisImage,

        ///<summary></summary>
        Edit,

        ///<summary></summary>
        ErrorZips,

        ///<summary></summary>
        ExportToPDF,

        ///<summary></summary>
        FilteredImages,

        ///<summary></summary>
        FindSameMangaByName,

        ///<summary></summary>
        Folders,

        ///<summary></summary>
        FullScreen,

        ///<summary></summary>
        FunctionOff,

        ///<summary></summary>
        FunctionOn,

        ///<summary></summary>
        GitHubFeedBack,

        ///<summary></summary>
        IsEmptyFolderShow,

        ///<summary></summary>
        MailFeedbackToDeveloper,

        ///<summary></summary>
        MangaTags,

        ///<summary></summary>
        MangaTagsCategorys,

        ///<summary></summary>
        MangaTagsManage,

        ///<summary></summary>
        No,

        ///<summary></summary>
        NoFoldersPleaseAddFirst,

        ///<summary></summary>
        NoSelectedFolder,

        ///<summary></summary>
        NowInitializingMaybeError,

        ///<summary></summary>
        NowReading,

        ///<summary></summary>
        Open,

        ///<summary></summary>
        OpenFile,

        ///<summary></summary>
        OpenFolder,

        ///<summary></summary>
        OpenInMicrosoftStore,

        ///<summary></summary>
        OpenItsPlacedFolder,

        ///<summary></summary>
        OverviewInformation,

        ///<summary></summary>
        PinnedFolders,

        ///<summary></summary>
        Refresh,

        ///<summary></summary>
        RemoveFolder,

        ///<summary></summary>
        RemoveRepeatTags,

        ///<summary></summary>
        RenameFile,

        ///<summary></summary>
        SaveAs,

        ///<summary></summary>
        Search,

        ///<summary></summary>
        SetAsDefaultBookcaseFolder,

        ///<summary></summary>
        SetBack,

        ///<summary></summary>
        ShowFolderBookcase,

        ///<summary></summary>
        Tags,

        ///<summary></summary>
        ToolBox,

        ///<summary></summary>
        Translate,

        ///<summary></summary>
        UpdateRecords,

        ///<summary></summary>
        Usage,

        ///<summary></summary>
        WhetherOpenFilterImageFunction,

        ///<summary></summary>
        WhetherShowDialogBeforeDelete,

        ///<summary></summary>
        Yes,
    }

    ///<summary> </summary>
    public class AppxmanifestExtension : MarkupExtension
    {
        ///<summary> </summary>
        public Appxmanifest Uid { get; set; }

        ///<summary> </summary>
        protected override object ProvideValue ()
        {
            return ResourceLoader
                .GetForViewIndependentUse("Appxmanifest")
                .GetString(Uid.ToString());
        }
    }

    ///<summary> </summary>
    public class StringsExtension : MarkupExtension
    {
        ///<summary> </summary>
        public Strings Uid { get; set; }

        ///<summary> </summary>
        protected override object ProvideValue ()
        {
            return ResourceLoader.GetForViewIndependentUse("Strings").GetString(Uid.ToString());
        }
    }

    ///<summary> </summary>
    public class UIExtension : MarkupExtension
    {
        ///<summary> </summary>
        public UI Uid { get; set; }

        ///<summary> </summary>
        protected override object ProvideValue ()
        {
            return ResourceLoader.GetForViewIndependentUse("UI").GetString(Uid.ToString());
        }
    }
}

namespace EroMangaManager.WinUI3.LocalizationWords
{
    ///<summary> </summary>
    public enum QuestionAndAnswers
    {
        ///<summary></summary>
        Answer_SupportedType,

        ///<summary></summary>
        Answer_WhatISTheApp,

        ///<summary></summary>
        Answer_WhyZipRatherFolder,

        ///<summary></summary>
        Description_20221219,

        ///<summary></summary>
        Description_20221226,

        ///<summary></summary>
        Description_20230109,

        ///<summary></summary>
        Description_20230125,

        ///<summary></summary>
        Description_20230205,

        ///<summary></summary>
        Description_before,

        ///<summary></summary>
        Question_SupportedType,

        ///<summary></summary>
        Question_WhatISTheApp,

        ///<summary></summary>
        Question_WhyZipRatherFolder,

        ///<summary></summary>
        Version_20221219,

        ///<summary></summary>
        Version_20221226,

        ///<summary></summary>
        Version_20230109,

        ///<summary></summary>
        Version_20230125,

        ///<summary></summary>
        Version_20230205,

        ///<summary></summary>
        Version_before,

        Question_MangaLibrary,
        Question_Tag,
        Answer_Tag,
        Answer_MangaLibrary
    }

    ///<summary> </summary>
    public class QuestionAndAnswersExtension : MarkupExtension
    {
        ///<summary> </summary>
        public QuestionAndAnswers Uid { get; set; }

        ///<summary> </summary>
        protected override object ProvideValue ()
        {
            return ResourceLoader
                .GetForViewIndependentUse("QuestionAndAnswers")
                .GetString(Uid.ToString());
        }
    }
}