var $modal ;
$(function(){
    $modal= $('#my-alert');
})


$(document).on("click", ".btn-login", function () {
   
    //获取登录数据信息
    var loginName = $("#loginname").val();
    var pwd = $("#password").val();
    var isRemember = $(".remeber").is(":checked") ? true : false;
    if (loginName == "") {
        $("#loginname").focus();
        $("#showmsg").text("请输入登陆名");
        $modal.modal();
        return;
    }
    if (pwd == "") {
        $("#showmsg").text("请输入密码");
        $modal.modal();
        $("#passpord").focus();
        return;
    }
    var LoginInfo = {
        "LoginName": loginName,
        "LoginPWD": pwd,
        "VCode": "",
        "IsReMember": isRemember
    }
    $.ajax({
        url: "/admin/login/Login",
        data: LoginInfo,
        type: "post",
        datatype: "json",
        success: function (e) {
            if (e.status == 0) {
                window.location="/admin/home/index"
            }
            else {
                //TODO:提示返回消息
                $("#showmsg").text(e.msg);
                $modal.modal();
            }
        },
        error: function (er) {
            $("#showmsg").text(er);
            $modal.modal();
        }
    })

})
