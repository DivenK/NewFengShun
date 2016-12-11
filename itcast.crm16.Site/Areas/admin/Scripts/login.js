$(document).on("click", ".btn-login", function () {
    $('#your-modal').modal();
    return;
    //获取登录数据信息
    var loginName = $("#loginname").val();
    var pwd = $("#password").val();
    var isRemember = $(".remeber").is(":checked") ? true : false;
    if (loginName == "") {
        $("#loginname").focus();
        alert("请输入登陆名")
        return;
    }
    if (pwd == "") {
        alert("请输入密码");
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
            }
        },
        error: function (er) {

        }
    })

})