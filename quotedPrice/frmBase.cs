/*
 * 在Program.cs文件的Main()方法第一行添加如下三句
 * DevExpress.UserSkins.BonusSkins.Register();//皮肤注册 
 * DevExpress.UserSkins.OfficeSkins.Register();
 * DevExpress.Skins.SkinManager.EnableFormSkins();//激活窗体皮肤，否则窗体还是操作系统默认主题风格
*/
public class frmBase : DevExpress.XtraEditors.XtraForm
{
    public static DevExpress.LookAndFeel.DefaultLookAndFeel defaultLookAndFeel = new DevExpress.LookAndFeel.DefaultLookAndFeel();
    public frmBase()
    {
        //设置默认皮肤
        defaultLookAndFeel.LookAndFeel.SkinName = "Coffee";
    }
}