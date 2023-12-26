using System.ComponentModel;

namespace EroMangaManager.Core.Setting
{
    /// <summary>
    /// 设置项总和
    /// </summary>
    public interface IAppConfig
    {
        /// <summary>
        /// 通用设置
        /// </summary>
        General General { get; set; }

        /// <summary>
        /// 漫画打开相关设置
        /// </summary>
        MangaOpenWaySetting MangaOpenWaySetting { get; set; }
    }

    /// <summary>
    /// 一般设置项
    /// </summary>
    public interface General
    {
        /// <summary>
        /// 是否启用过滤图片功能
        /// </summary>
        [DefaultValue(false)]
        bool IsFilterImageOn { set; get; }

        /// <summary>
        /// 是否在删除前显示对话框
        /// </summary>
        [DefaultValue(true)]
        bool WhetherShowDialogBeforeDelete { set; get; }

        /// <summary>
        /// 文件删除设置
        /// </summary>
        [DefaultValue(false)]
        bool StorageFileDeleteOption { set; get; }

        /// <summary>
        /// 默认图书书架
        /// </summary>
        string DefaultBookcaseFolder { set; get; }

        /// <summary>
        /// 项删除
        /// </summary>
        [DefaultValue(false)]
        bool StorageDeleteOption { set; get; }

        /// <summary>
        /// 是否显示空文件夹
        /// </summary>
        [DefaultValue(false)]
        bool IsEmptyFolderShow { set; get; }

        /// <summary>
        /// app默认UI语言
        /// </summary>
        [DefaultValue(DefaultAppUILanguageStrings.zhCN)]
        string DefaultAppUILanguage { set; get; }

        /// <summary>
        /// 系统主题
        /// </summary>
        [DefaultValue(ThemeValue.Auto)]
        int Theme { set; get; }
        /// <summary>
        /// 默认书架界面
        /// </summary>
        [DefaultValue(0)]
        int BookcaseTemplateKey { set; get; }
        /// <summary>
        /// 是否加载子文件夹
        /// </summary>
        [DefaultValue(false)]
        bool WhetherPickSubFolder { set; get; }
    }

    /// <summary>
    /// app默认UI语言
    /// </summary>
    public class DefaultAppUILanguageStrings
    {
        /// <summary>
        /// 简中
        /// </summary>
        public const string zhCN = "zh-CN";

        /// <summary>
        /// 英文
        /// </summary>
        public const string en = "en";
    }

    /// <summary>
    /// 漫画打开方式相关设置
    /// </summary>
    public interface MangaOpenWaySetting
    {
        /// <summary>
        /// 打开漫画方式
        /// </summary>
        [DefaultValue(ReadMangaWayStrings.InternalReadPage)]
        string ReadMangaWay { set; get; }

        /// <summary>
        /// 用户选择的打开漫画方式
        /// </summary>
        string UserSelectedReadMangaExePath { set; get; }
    }

    /// <summary>
    ///
    /// </summary>
    public static class ReadMangaWayStrings
    {
        /// <summary>
        /// 内部ReadPage，
        /// </summary>
        public const string InternalReadPage = "InternalReadPage";

        /// <summary>
        /// 系统决定的关联打开方式
        /// </summary>
        public const string OSRelated = "OSRelated";

        /// <summary>
        /// 用户选定的exe
        /// </summary>
        public const string UserSelected = "UserSelected";
    }

    /// <summary>
    /// 主题设置值，这个值对应ComboBox的索引值
    /// </summary>
    public static class ThemeValue
    {
        /// <summary>
        /// 深色
        /// </summary>
        public const int Dark = 2;

        /// <summary>
        /// 浅色
        /// </summary>
        public const int Light = 1;

        /// <summary>
        /// 自动
        /// </summary>
        public const int Auto = 0;
    }
}