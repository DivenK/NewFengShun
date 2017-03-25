<%@ WebHandler Language="C#" Class="UEditorHandler" %>

using System;
using System.Web;
using System.IO;
using System.Collections;
using Newtonsoft.Json;

public class UEditorHandler : IHttpHandler
{
    public void ProcessRequest(HttpContext context)
    {
        Handler action = null;
        switch (context.Request["action"])
        {
            case "config":
                action = new ConfigHandler(context);
                break;
            case "uploadimage":
                action = new UploadHandler(context, new UploadConfig()
                {
                    AllowExtensions = Config.GetStringList("imageAllowFiles"),
                    PathFormat = Config.GetString("imagePathFormat"),//加多一个文件名字，由前台传过来
                    SizeLimit = Config.GetInt("imageMaxSize"),
                    UploadFieldName = Config.GetString("imageFieldName")
                });
                break;
            case "uploadscrawl":
                action = new UploadHandler(context, new UploadConfig()
                {
                    AllowExtensions = new string[] { ".png" },
                    PathFormat = Config.GetString("scrawlPathFormat"),
                    SizeLimit = Config.GetInt("scrawlMaxSize"),
                    UploadFieldName = Config.GetString("scrawlFieldName"),
                    Base64 = true,
                    Base64Filename = "scrawl.png"
                });
                break;
            case "uploadvideo":
                action = new UploadHandler(context, new UploadConfig()
                {
                    AllowExtensions = Config.GetStringList("videoAllowFiles"),
                    PathFormat = Config.GetString("videoPathFormat"),
                    SizeLimit = Config.GetInt("videoMaxSize"),
                    UploadFieldName = Config.GetString("videoFieldName")
                });
                break;
            case "uploadfile":
                action = new UploadHandler(context, new UploadConfig()
                {
                    AllowExtensions = Config.GetStringList("fileAllowFiles"),
                    PathFormat = Config.GetString("filePathFormat"),
                    SizeLimit = Config.GetInt("fileMaxSize"),
                    UploadFieldName = Config.GetString("fileFieldName")
                });
                break;
            case "listimage":
                action = new ListFileManager(context, Config.GetString("imageManagerListPath"), Config.GetStringList("imageManagerAllowFiles"));
                break;
            case "listfile":
                action = new ListFileManager(context, Config.GetString("fileManagerListPath"), Config.GetStringList("fileManagerAllowFiles"));
                break;
            case "catchimage":
                action = new CrawlerHandler(context);
                break;
            case "CreateDirectoryName"://新建相册的方法
                action = new DirectoryHandle(context, context.Request["DirectoryName"]);
                break;
            case "SeachFile"://新建相册的方法
                action = new DirectorySearchHandle(context);
                break;
            case "SaveFileName":
                action = new SaveSessionHandle(context);
                break;
            case "SaveFileNamejpg":
                action = new SeachFileForNameHandle(context);
                break;
            default:
                action = new NotSupportedHandler(context);
                break;
        }
        action.Process();
    }

    public bool IsReusable
    {
        get
        {
            return false;
        }
    }

}
/// <summary>
/// 创建文件夹
/// </summary>
public class DirectoryHandle : Handler
{
    public string DirectoryName;
    public DirectoryHandle(HttpContext context, string directoryName) : base(context)
    {
        this.DirectoryName = directoryName;
    }
    public override void Process()
    {
        var savePath = Config.GetString("imagePathFormat") + DirectoryName;
        var localPath = Server.MapPath(savePath);
        DirectoryInfo dirParent = new DirectoryInfo(localPath);
        if (!dirParent.Exists)
        {
            DirectoryInfo dir = new DirectoryInfo(Server.MapPath(Config.GetString("imagePathFormat")));
            dir.CreateSubdirectory(DirectoryName);
            HttpCookie cookie = Request.Cookies["MWS_User"];
            if (cookie == null)
            {
                cookie = new HttpCookie("MWS_User");
            }
            cookie.Values.Set("DirectoryName", DirectoryName);
            cookie.Expires = DateTime.Now.AddDays(1);
            Response.SetCookie(cookie);
            this.WriteJson(new
            {
                state = "SUCCESS"
            });
        }
        this.WriteJson(new
        {
            state = "FALSE",
            errorMessage = "相册'" + DirectoryName + "'已存在，请重新输入"
        });
    }
}
/// <summary>
/// 查询文件夹
/// </summary>
public class DirectorySearchHandle : Handler
{

    public DirectorySearchHandle(HttpContext context) : base(context)
    {

    }
    public override void Process()
    {
        var savePath = Config.GetString("imagePathFormat");
        var localPath = Server.MapPath(savePath);
        DirectoryInfo dirParent = new DirectoryInfo(localPath);
        var ditItems = dirParent.GetDirectories();
        HttpCookie cookie = Request.Cookies["MWS_User"];
        if (cookie == null)
        {
            cookie = new HttpCookie("MWS_User");
        }
        cookie.Values.Set("DirectoryName", ditItems[0].Name);
        cookie.Expires = DateTime.Now.AddDays(1);
        Response.Cookies.Add(cookie);
        this.WriteJson(new
        {
            list = ditItems,
        });
    }
}
/// <summary>
/// 保存选择的文件夹
/// </summary>
public class SaveSessionHandle : Handler
{
    public HttpContext _context;
    public SaveSessionHandle(HttpContext context) : base(context)
    {
        this._context = context;
    }
    public override void Process()
    {
        HttpCookie cookie = Request.Cookies["MWS_User"];
        if (cookie == null)
        {
            cookie = new HttpCookie("MWS_User");
        }
        cookie.Values.Set("DirectoryName", Request["DirectoryName"]);
        cookie.Expires = DateTime.Now.AddDays(1);
        Response.SetCookie(cookie);
        this.WriteJson(new
        {
            state = "success",
        });
    }
}
/// <summary>
/// 根据文件夹获取相册
/// </summary>
public class SeachFileForNameHandle : Handler
{
    public HttpContext _context;
    public SeachFileForNameHandle(HttpContext context) : base(context)
    {
        this._context = context;
    }
    public override void Process()
    {
        var savePath = Config.GetString("imagePathFormat")+"/"+Request["DirectoryName"];
        var localPath = Server.MapPath(savePath);
        DirectoryInfo folder = new DirectoryInfo(localPath);
        var fileList = folder.GetFiles("*");
        this.WriteJson(new
        {
            state="success",
            date = fileList,
        });
    }
}