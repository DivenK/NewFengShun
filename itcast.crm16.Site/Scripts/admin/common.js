var helper = {
    win: null, //1.0 负责存放新增和编辑页面的面板的对象
    //1.0 负责打开新增和编辑页面面板方法
    openPanel: function (title, url, width, height) {
        var h = 450;
        var w = 450;
        if (width) {
            w = width
        }
        if (height) {
            h = height
        }
        //统一打开加载提示面板
        window.top.optloading(true);

        //打开面板,载入指定url的内容
        helper.win = $.ligerDialog.open({
            title: title,
            url: url,
            width: w,
            height: h
             , onLoaded: function () {
                 window.top.optloading(false);
             }
        });
    }
    ,//2.0 将add和edit页面上的begin和success方法封装
    ajaxbegin: function () {
        //打开正在提交中
        window.top.optloading(true);
    }
    , ajaxsuccess: function (ajaxobj) {
        //关闭正在提交中
        window.top.optloading(false);

        helper.checkstatus(ajaxobj, function () {
            //刷新列表页面
            window.parent.grid.reload();
            //关闭修改面板
            window.parent.helper.win.close();
        });
    }
    //负责检查后台传入的ajaxobj中的状态码
    , checkstatus: function (ajaxobj, callbackFun) {
        if (ajaxobj.status == "1") {
            $.ligerDialog.error(ajaxobj.msg);
        } else if (ajaxobj.status == "2") {
            $.ligerDialog.error(ajaxobj.msg);
        }
        else if (ajaxobj.status == "3") {
            $.ligerDialog.error(ajaxobj.msg);
            setTimeout(function () { window.top.location = "/admin/login/login" }, 2000);
        }
        else {
            //执行正常逻辑的回调函数
            callbackFun();
        }
    }
    //获取当前页面所在tab页面的tabid 就是当前页面所在菜单的mid
    , mid: function () {
        return window.top.tab.getSelectedTabItemID();
    }

    ,//统一获取权限按钮方法
    getperfun: function (callbackFun) {
        $.post("/admin/OptPermiss/getfunctions/" + helper.mid(), null, function (ajaxobj) {
            helper.checkstatus(ajaxobj, function () {
                var data = ajaxobj.datas; //[{text:"新增",click:"add",icon:"add"},{line:true}]

                for (var i = 0; i < data.length; i++) {
                    if (data[i].click) {
                        //将click中的字符串 通过eval转换成函数指针
                        data[i].click = eval(data[i].click);
                    }
                }
                //调用creategrid
                callbackFun(data);
            });
        }, "json");
    }
}